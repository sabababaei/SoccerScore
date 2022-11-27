using Application.Common.Exceptions;
using Application.Common.Models;
using Application.Interfaces;
using Domain.Entities.Matches;
using Domain.Entities.Teams;
using Domain.Enumarations;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Application.Services.Matches.Commands.UpdateMatch;

public class UpdateMatchCommand:IRequest<Result<Match>>
{
    public int Id { get; set; }
    public ICollection<MatchTeam> MatchTeams { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public MatchStatus Status { get; set; }
    public MatchTypes MatchType { get; set; }
    public MatchTypeImportance Importance { get; set; }
    
    public class Handler : IRequestHandler<UpdateMatchCommand ,Result<Match>>
    {
        
        private readonly IDataBaseContext _context;

        public Handler(  IDataBaseContext context)
        {
             
            _context = context;
        }
        public async Task<Result<Match>> Handle(UpdateMatchCommand request, CancellationToken cancellationToken)
        {
            var entity = _context.Matches.SingleOrDefaultAsync(p=>p.Id==request.Id).Result;
            if (entity==null)
            {
                throw new NotFoundException(nameof(Match) ,request.Id);
            }

            entity.Status = request.Status;
            entity.EndDate = request.EndDate;
            entity.StartDate = request.StartDate;
            entity.MatchTeams = request.MatchTeams;
            try
            {
                await _context.SaveChangesAsync(cancellationToken);
                return new Result<Match>(true, new []{""} , entity);
             
            }
            catch (Exception e)
            {
                return new Result<Match>(false, new[] { e.Message } , entity);
            }
           

        }
    }
}