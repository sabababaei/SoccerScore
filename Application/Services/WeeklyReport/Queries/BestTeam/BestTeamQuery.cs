using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.WeeklyReport.Queries.BestTeam;

public class BestTeamQuery:IRequest<BestTeamVm>
{
    public class Handler:IRequestHandler<BestTeamQuery , BestTeamVm>
    {
        private readonly IDataBaseContext _context;

        public Handler( IDataBaseContext context)
        {
            
            _context = context;
        }
        public async Task<BestTeamVm> Handle(BestTeamQuery request, CancellationToken cancellationToken)
        {
            var result=await _context.Teams
                .Where(t=> t.Rank == (_context.Teams.Min((m => m.Rank))) && t.Rank != 0)
                .Select(t=>new BestTeamInfo()
                {
                    Rank = t.Rank,
                    CurrentRate = t.CurrentRate,
                    TeamName = t.TeamName
                }).ToListAsync();

            return new BestTeamVm()
            {
                BestTeams = result
            };

        }
    }
}