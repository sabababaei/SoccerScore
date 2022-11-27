using Application.Common.Exceptions;
using Application.Interfaces;
using Application.Services.Matches.Queries.GetMatchDetail;
using Domain.Entities.Players;
using MediatR;

namespace Application.Services.Players.Queries.GetPlayerDetail;

public class GetPlayerDetailQuery:IRequest<GetPlayerDetailVm>
{
    public int Id { get; set; }
    public class Handler:IRequestHandler<GetPlayerDetailQuery,GetPlayerDetailVm>
    {
        private readonly IDataBaseContext _context;
        
        public Handler(IDataBaseContext context)
        {
            _context = context;
        }
        public async Task<GetPlayerDetailVm> Handle(GetPlayerDetailQuery request, CancellationToken cancellationToken)
        {

            var entity = _context.Players.FirstOrDefault(p => p.Id == request.Id);
            if (entity==null)
            {
                throw new NotFoundException(nameof(Player), request.Id);
            }
            var result= new GetPlayerDetailVm()
            {
                Id = entity.Id,
                PlayerName = entity.PlayerName
            };
            
            return result;
        }
    }
}