using Application.Common.Models;
using Application.Interfaces;
using Domain.Entities.Players;
using MediatR;

namespace Application.Services.Players.Commands.UpdatePlayer;

public class UpdatePlayerCommand:IRequest<Result<Player>>
{
    public int Id { get; set; }
    public string PlayerName { get; set; }

    public class Handler : IRequestHandler<UpdatePlayerCommand, Result<Player>>
    {
        private readonly IDataBaseContext _context;

        public Handler(IDataBaseContext context)
        {
            _context = context;
        }
        public async Task<Result<Player>> Handle(UpdatePlayerCommand request, CancellationToken cancellationToken)
        {
            var player = _context.Players.FirstOrDefault(p => p.Id == request.Id);
            player.Id = request.Id;
            player.PlayerName = request.PlayerName;

            try
            {
                await _context.SaveChangesAsync(CancellationToken.None);
                return new Result<Player>(true, null , player);
            }
            catch (Exception e)
            {
                return new Result<Player>(false, new[] { e.Message } , player);
            }
        }
    }
}