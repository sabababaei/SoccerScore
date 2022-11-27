using Application.Common.Exceptions;
using Application.Common.Models;
using Application.Interfaces;
using Domain.Entities.Players;
using MediatR;

namespace Application.Services.Players.Commands.DeletePlayer;

public class DeletePlayerCommand : IRequest<Result>
{
    public int Id { get; set; }

    public class Handler : IRequestHandler<DeletePlayerCommand, Result>
    {
        private readonly IDataBaseContext _context;

        public Handler(IDataBaseContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(DeletePlayerCommand request, CancellationToken cancellationToken)
        {
            var player = await _context.Players.FindAsync(request.Id);
            if (player == null)
            {
                throw new NotFoundException(nameof(Player), request.Id);
            }

            try
            {
                _context.Players.Remove(player);
                await _context.SaveChangesAsync(cancellationToken);
                return new Result(true, new []{""});

            }
            catch (Exception e)
            {
                return new Result(false, new[] { e.Message });
            }

        }
    }
}