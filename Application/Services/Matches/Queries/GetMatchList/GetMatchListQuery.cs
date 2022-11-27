using Application.Interfaces;
using Application.Services.Matches.Queries.GetMatchDetail;
using Domain.Entities.Matches;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Matches.Queries.GetMatchList;

public class GetMatchListQuery:IRequest<IList<GetMatchListVm>>
{
    public class Handler:IRequestHandler<GetMatchListQuery,IList<GetMatchListVm>>
    {
    
        private readonly IDataBaseContext _context;

        public Handler(IDataBaseContext context)
        {
           
            _context = context;
        }
        public async Task<IList<GetMatchListVm>> Handle(GetMatchListQuery request, CancellationToken cancellationToken)
        {
            var list = await _context.Matches
                .Include(m=>m.MatchTeams)
                .ThenInclude(t=>t.Team)
                .Select(a=>new GetMatchListVm()
                {
                    Id = a.Id,
                    Status = a.Status,
                    EndDate = a.EndDate,
                    StartDate = a.StartDate,
                    MatchType = a.MatchType,
                    Importance = a.Importance,
                    MatchTeamsName = string.Join("," ,a.MatchTeams.Select(m=>m.Team).Select(t=>t.TeamName).ToList())
                }).ToListAsync(cancellationToken);
            return list;
         
        }
    }

}