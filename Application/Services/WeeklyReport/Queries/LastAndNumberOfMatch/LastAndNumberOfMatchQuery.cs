using Application.Interfaces;
using Domain.Enumarations;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.WeeklyReport.Queries.LastAndNumberOfMatch;

public class LastAndNumberOfMatchQuery : IRequest<LastAndNumberOfMatchVm>
{
 public class Handler : IRequestHandler<LastAndNumberOfMatchQuery, LastAndNumberOfMatchVm>
 {
  private readonly IDataBaseContext _context;

  public Handler(IDataBaseContext context)
  {

   _context = context;
  }

  public async Task<LastAndNumberOfMatchVm> Handle(LastAndNumberOfMatchQuery request,
   CancellationToken cancellationToken)
  {
   var result = _context.Matches
    .Include(m => m.MatchTeams)
    .ThenInclude(m => m.Team)
    .Where(m => m.StartDate >= (DateTime.Now.AddDays(-7)));


   var lastMatch = await result
    .Where(m => m.Status == MatchStatus.Done)
    .OrderByDescending(m => m.StartDate)
    .FirstOrDefaultAsync(cancellationToken);

   var numberOfMatch = await result
    .Where(m => m.Status == MatchStatus.Done || m.Status == MatchStatus.Doing)
    .CountAsync();

   return new LastAndNumberOfMatchVm()
   {
    Match = lastMatch,
    NumberOfWeeklyMatch = numberOfMatch
   };

  }
 }
}