using Application.Common.Exceptions;
using Application.Common.Models;
using Application.Interfaces;
using Application.Services.Players.Commands.DeletePlayer;
using MediatR;

namespace Application.Services.Teams.Commands.DeleteTeam;

public class DeleteTeamCommand:IRequest<Result>
{
    public int Id { get; set; }
    public class Handler:IRequestHandler<DeletePlayerCommand , Result>
    {
        private readonly IDataBaseContext _context;

        public Handler( IDataBaseContext context)
        {
          
            _context = context;
        }

        public async Task<Result> Handle(DeletePlayerCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Teams.FindAsync(request.Id);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Teams), request.Id);
            }

            try
            {
                _context.Teams.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);

                return (new Result(true, new []{""}));
            }
            catch (Exception e)
            {
                return new Result(false, new[] { e.Message });
            }
        }
    }
}