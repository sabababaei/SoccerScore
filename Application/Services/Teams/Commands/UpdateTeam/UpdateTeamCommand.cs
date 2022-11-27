using Application.Common.Exceptions;
using Application.Common.Models;
using Application.Interfaces;
using Domain.Entities.Players;
using Domain.Entities.Teams;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Teams.Commands.UpdateTeam;

public class UpdateTeamCommand:IRequest<Result<Team>>
{
    public int Id { get; set; }
    public string TeamName { get; set; }
    public ICollection<Player> Players { get; set; }
    public class Handler:IRequestHandler<UpdateTeamCommand , Result<Team>>
    {
        private readonly IDataBaseContext _context;

        public Handler(IDataBaseContext context)
        {
            _context = context;
        }
        public async Task<Result<Team>> Handle(UpdateTeamCommand request, CancellationToken cancellationToken)
        {
            var entity = _context.Teams.SingleOrDefaultAsync(p=>p.Id==request.Id).Result;
            if (entity==null)
            {
                throw new NotFoundException(nameof(Team) ,request.Id);
            }

            entity.Id = request.Id;
            entity.Players = request.Players;
            entity.TeamName = request.TeamName;
            try
            {
                await _context.SaveChangesAsync(cancellationToken);
                return new Result<Team>(true, new []{""} , entity);
             
            }
            catch (Exception e)
            {
                return new Result<Team>(false, new[] { e.Message } , entity);
            }

        }
    }
}