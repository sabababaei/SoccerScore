using Application.Common.Exceptions;
using Application.Common.Models;
using Application.Interfaces;
using Domain.Entities.Teams;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Teams.Queries.GetTeamOfSameStrength;

public class GetTeamOfSameStrengthQuery:IRequest<Result<ICollection<GetTeamOfSameStrengthVm>>>
{
    public int Id { get; set; }
    public class Handler:IRequestHandler<GetTeamOfSameStrengthQuery ,Result<ICollection<GetTeamOfSameStrengthVm>>>
    {
        private readonly IDataBaseContext _context;

        public Handler( IDataBaseContext context)
        {
          
            _context = context;
        }
        public async Task<Result<ICollection<GetTeamOfSameStrengthVm>>> Handle(GetTeamOfSameStrengthQuery request, CancellationToken cancellationToken)
        {
            var team = _context.Teams
                .Include(t=>t.MatchTeams)
     .FirstOrDefault(t => t.Id == request.Id);
            if (team == null)
            {
                throw new NotFoundException(nameof(Team), request.Id);
            }

            try
            {
                var list = await _context.Teams
                    .Include(t => t.MatchTeams)
                    .ThenInclude(t => t.Team)
                    .ThenInclude(t => t.Players)
                    .Where(t => t.Power >= team.Power * (decimal)0.99 && t.Power <= team.Power * (decimal)1.01 &&
                                t.Id != request.Id)
                    .Select(t => new GetTeamOfSameStrengthVm()
                    {
                        Id = t.Id,
                        Rank = t.Rank,
                        MatchesCount = t.MatchTeams.Count,
                        TeamName = t.TeamName,
                        TeamPlayers = string.Join(",", _context.Players.Include(p => p.Teams)
                            .Where(p => p.Teams.Any(a => a.Id == t.Id)).Select(p => p.PlayerName).ToList()),
                        Rate = t.CurrentRate,
                        Power = t.Power

                    }).ToListAsync();
                return new Result<ICollection<GetTeamOfSameStrengthVm>>(true,new []{""} ,list);

            }
            catch (Exception e)
            {
                return new Result<ICollection<GetTeamOfSameStrengthVm>>(false, new[] { e.Message }
                    ,new List<GetTeamOfSameStrengthVm>() );
            }
        }
    }
}