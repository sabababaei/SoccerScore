 
using Application.Common.Exceptions;
using Application.Common.Models;
using Application.Interfaces;
using Domain.Entities.Matches;
using MediatR;

namespace Application.Services.Matches.Commands.DeleteMatch;

public class DeleteMatchCommand : IRequest<Result>
{
    public int Id { get; set; }

    public class Handler : IRequestHandler<DeleteMatchCommand , Result>
    {
        
        private readonly IDataBaseContext _context;

        public Handler( IDataBaseContext context)
        {
          
            _context = context;
        }

        public async Task<Result> Handle(DeleteMatchCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Matches.FindAsync(request.Id);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Match), request.Id);
            }

            try
            {
                _context.Matches.Remove(entity);
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