using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Transactions;
using Application.Common.Constants;
using Application.Common.Exceptions;
using Application.Common.Models;
using Application.Interfaces;
using Domain.Entities.Matches;
using Domain.Entities.Teams;
using Domain.Enumarations;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Matches.Commands.GameOver;

public class GameOverCommand : IRequest<Result>
{
    public int Id { get; set; }
    public DateTime? EndDate { get; set; }
    public ICollection<MatchTeam> MatchTeams { get; set; }

    public class Handler : IRequestHandler<GameOverCommand, Result>
    {
        private readonly IDataBaseContext _context;

        public Handler(IDataBaseContext context)
        {

            _context = context;
        }

        public async Task<Result> Handle(GameOverCommand request, CancellationToken cancellationToken)
        {
            var transaction = _context.DbTransaction().BeginTransaction();
            var match = _context.Matches
                .Include(m => m.MatchTeams)
                .ThenInclude(m => m.Team)
                .FirstOrDefault(m => m.Id == request.Id);
            if (match == null)
            {
                throw new NotFoundException(nameof(Match), request.Id);
            }

            try
            {

                var matchTeams = match.MatchTeams;
                match.EndDate = request.EndDate;
                match.Status = MatchStatus.Done;
                foreach (var matchTeam in matchTeams)
                {
                    matchTeam.NumberOfGoal =
                        request.MatchTeams.FirstOrDefault(m => m.Id == matchTeam.Id)!.NumberOfGoal;
                    matchTeam.Result = request.MatchTeams.FirstOrDefault(m => m.Id == matchTeam.Id)!.Result;

                    var score = MatchTeamScore((int)(Enum.Parse<MatchTypeImportance>(match.Importance.ToString())),
                        (int)(Enum.Parse<MatchResult>(matchTeam.Result.ToString())));
                    matchTeam.Score = score;

                    var team = matchTeam.Team;
                    team.CurrentRate += score;

                }
                await _context.SaveChangesAsync(cancellationToken);
                var teamList = GetTeamsListAndDataForUpdate();

                foreach (var team in _context.Teams.Include(t => t.TeamRates)
                                 .Where(t=> _context.Matches.Include(m=>m.MatchTeams)
                                 .SelectMany(m=>m.MatchTeams).Any(m=>m.Team.Id == t.Id && m.Match.Status==MatchStatus.Done)))
                {
                    var entity = teamList.FirstOrDefault(t => t.Id == team.Id);
                    team.Power = entity.Power;
                    team.Rank = entity.Rank;
                    team.TeamRates?.Add(new TeamStatisticalInfoHistory()
                    {
                        LastDate = DateTime.Now,
                        LastPower = entity.Power,
                        LastRank = entity.Rank ,
                        LastRate = team.CurrentRate
                    });

                }

                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
                return (new Result(true, new[] { "" }));
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync(cancellationToken);
                return new Result(false, new[] { e.Message });
            }


        }

        private int MatchTeamScore(int matchImportance, int teamResult)
        {
            // I * (W -We)
            // I :Importance of Match
            //W : Result
            //We : Expected Result ; we suppose it 0
            return matchImportance * teamResult;
        }

        private IEnumerable<Team> GetTeamsListAndDataForUpdate()
        {
            try
            {
                var result = _context.Teams
                    .Include(t => t.MatchTeams)
                    .GroupBy(t => new { t.Id, t.CurrentRate })
                    .Select(t =>
                        new
                        {
                            TeamId = t.Key.Id,
                            TotalGoal =
                                _context.Matches.Include(m => m.MatchTeams).SelectMany(m => m.MatchTeams)
                                    .Where(m => m.Team.Id == t.Key.Id).Sum(m => m.NumberOfGoal),
                            TotalScore =
                                _context.Matches.Include(m => m.MatchTeams).SelectMany(m => m.MatchTeams)
                                    .Where(m => m.Team.Id == t.Key.Id).Sum(m => m.Score),
                            TotalCountGame = _context.Matches.Include(m => m.MatchTeams)
                                .SelectMany(m => m.MatchTeams).Count(m => m.Team.Id == t.Key.Id),
                            Rate = t.Key.CurrentRate
                        })
                    .OrderByDescending(t => t.Rate)
                    .ThenByDescending(t => t.TotalScore)
                    .ThenBy(t => t.TotalCountGame)
                    .ToList();
                ;

                var teamList = result.Select((r, index) => new Team()
                {
                    Id = r.TeamId,
                    Rank = index + 1,
                    Power = (decimal)(200 - (index + 1)) / 100,
                 
                }).ToList();

                return teamList;

            }
            catch (Exception e)
            {
                return null;
            }
        }


    }

}