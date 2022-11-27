using Application.Interfaces;
using Domain.Enumarations;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.WeeklyReport.Queries.MatchesInProgress;

public class MatchesInProgressQuery:IRequest<MatchesInProgressVm>
{
    public class Handler:IRequestHandler<MatchesInProgressQuery ,MatchesInProgressVm>
    {
        private readonly IDataBaseContext _context;

        public Handler( IDataBaseContext context)
        {
            
            _context = context;
        }
        public async Task<MatchesInProgressVm> Handle(MatchesInProgressQuery request, CancellationToken cancellationToken)
        {
            var result =await _context.Matches
                .Include(m => m.MatchTeams)
                .ThenInclude(m => m.Team)
                .Where(m => m.StartDate >= (DateTime.Now.AddDays(-7)) && m.Status != MatchStatus.Done)
                .ToListAsync(cancellationToken);
            return new MatchesInProgressVm()
            {
                Match = result
            };
        }
    }
}