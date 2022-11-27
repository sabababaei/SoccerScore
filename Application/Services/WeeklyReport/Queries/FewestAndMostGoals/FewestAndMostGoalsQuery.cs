using Application.Interfaces;
using Domain.Enumarations;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.WeeklyReport.Queries.FewestAndMostGoals;

public class FewestAndMostGoalsQuery:IRequest<FewestAndMostGoalsVm>
{
    public class Handler:IRequestHandler<FewestAndMostGoalsQuery , FewestAndMostGoalsVm>
    {
        private readonly IDataBaseContext _context;

        public Handler( IDataBaseContext context)
        {
            
            _context = context;
        }
        public async Task<FewestAndMostGoalsVm> Handle(FewestAndMostGoalsQuery request, CancellationToken cancellationToken)
        {
            var result = _context.Matches
                .Where(m => m.StartDate >= (DateTime.Now.AddDays(-7)) && m.Status==MatchStatus.Done)
                .Include(m => m.MatchTeams)
                .ThenInclude(t=>t.Team)
                .SelectMany(m =>m.MatchTeams)
                .Select(m=>new
                {
                    teamId =m.Team.Id , teamName=m.Team.TeamName,rank=m.Team.Rank,currentRate=m.Team.CurrentRate ,numberOfGoal= m.NumberOfGoal
                })
                .GroupBy(t =>new { t.teamId , t.teamName ,t.rank , t.currentRate})
                .Select(t => new
                    {
                        TeamId = t.Key.teamId,
                        SumofGoals = t.Sum(p => p.numberOfGoal),
                        Rank=t.Key.rank,
                        CurrentRate=t.Key.currentRate,
                        TeamName=t.Key.teamName
                    });
            

            
            var listMin = await result
                .Where(t=>t.SumofGoals== result.Min(p => p.SumofGoals))
                .Select(t=>new FewestGoalsVm()
                {
                    TeamName =  t.TeamName,
                    Rank = t.Rank,
                    CurrentRate = t.CurrentRate,
                    WeeklyGoals = t.SumofGoals
                })
                .ToListAsync(cancellationToken);
            
            var listMax =await result
                .Where(t=>t.SumofGoals== result.Max(p => p.SumofGoals))
                .Select(t=>new MostGoalsVm()
                {
                    TeamName =  t.TeamName,
                    Rank = t.Rank,
                    CurrentRate = t.CurrentRate,
                    WeeklyGoals = t.SumofGoals
                })
                .ToListAsync(cancellationToken);
                
            return new FewestAndMostGoalsVm()
            {
                FewestGoalsTeams = listMin,
                MostGoalsTeam = listMax
            };
           
            
        }
    }
}