using Application.Interfaces;
using Application.Services.Matches.Queries.GetMatchList;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Teams.Queries.GetTeamList;

public class GetTeamListQuery:IRequest<IList<GetTeamListVm>>
{
    public class Handler:IRequestHandler<GetTeamListQuery,IList<GetTeamListVm>>
    {
    
        private readonly IDataBaseContext _context;

        public Handler(IDataBaseContext context)
        {
           
            _context = context;
        }
        public async Task<IList<GetTeamListVm>> Handle(GetTeamListQuery request, CancellationToken cancellationToken)
        {
            var list = await _context.Teams
                .Include(p=>p.Players)
               
                .Select(a=>new GetTeamListVm()
                {
                    Id = a.Id,
                    TeamPlayers = a.Players,
                    Rank = a.Rank,
                    TeamName = a.TeamName
                }).ToListAsync(cancellationToken);
            return list;
         
        }
    }
}