using Application.Common.Models;
using Application.Interfaces;
using Domain.Entities.Players;
using MediatR;

namespace Application.Services.Players.Commands.CreatePlayer;

public class CreatePlayerCommand:IRequest<Result<Player>>
{
    public int Id { get; set; }
    public string PlayerName { get; set; }

    public class Handler : IRequestHandler<CreatePlayerCommand, Result<Player>>
    {
        private readonly IDataBaseContext _context;

        public Handler(IDataBaseContext context)
        {
            _context = context;
        }
        public async Task<Result<Player>> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
        {
            var player = new Player()
            {
                Id = request.Id,
                PlayerName = request.PlayerName
            };
            try
            {
                 await _context.Players.AddAsync(player);
                await _context.SaveChangesAsync(cancellationToken);
                return new Result<Player>(true, null, player);
            }
            catch (Exception e)
            {
                return new Result<Player>(false, new[] { e.Message }, player);
            }



        }
    }
}