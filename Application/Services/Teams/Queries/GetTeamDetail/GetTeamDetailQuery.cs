using Application.Common.Exceptions;
using Application.Interfaces;
using Application.Services.Matches.Queries.GetMatchDetail;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Teams.Queries.GetTeamDetail;

public class GetTeamDetailQuery:IRequest<GetTeamDetailVm>
{
    public int Id { get; set; }   
    public class Handler:IRequestHandler<GetTeamDetailQuery , GetTeamDetailVm>
    {
      
        private readonly IDataBaseContext _context;

        public Handler( IDataBaseContext context)
        {
            
            _context = context;
        }
        public async Task<GetTeamDetailVm> Handle(GetTeamDetailQuery request, CancellationToken cancellationToken)
        {
            var entity = _context.Teams.SingleOrDefault(p => p.Id == request.Id);
            if (entity==null)
            {
                throw new NotFoundException(nameof(Teams), request.Id);
            }
            var result= new GetTeamDetailVm()
            {
                Id = entity.Id,
                TeamPlayers =  string.Join(",", _context.Players.Include(p => p.Teams)
                    .Where(p => p.Teams.Any(a => a.Id ==entity.Id)).Select(p => p.PlayerName).ToList()),
                Rank = entity.Rank,
                TeamName = entity.TeamName,
                Power = entity.Power,
                CurrentRate = entity.CurrentRate
            };
            
            return result;


        }
    }
}