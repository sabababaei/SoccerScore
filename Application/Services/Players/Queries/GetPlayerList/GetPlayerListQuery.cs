using Application.Interfaces;
using Application.Services.Matches.Queries.GetMatchList;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Players.Queries.GetPlayerList;

public class GetPlayerListQuery:IRequest<IList<GetPlayerListVm>>
{
    public class Handler:IRequestHandler<GetPlayerListQuery , IList<GetPlayerListVm>>
    {
        private readonly IDataBaseContext _context;

        public Handler(IDataBaseContext context)
        {
           
            _context = context;
        }
        public async Task<IList<GetPlayerListVm>> Handle(GetPlayerListQuery request, CancellationToken cancellationToken)
        {
            var list = await _context.Players
                .Select(a=>new GetPlayerListVm()
                {
                    Id = a.Id,
                    PlayerName = a.PlayerName
                }).ToListAsync(cancellationToken);
            return list;
        }
    }
}
