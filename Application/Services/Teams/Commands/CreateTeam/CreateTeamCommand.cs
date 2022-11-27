using Application.Common.Models;
using Application.Interfaces;
using Domain.Entities.Players;
using Domain.Entities.Teams;
using MediatR;

namespace Application.Services.Teams.Commands.CreateTeam;

public class CreateTeamCommand:IRequest<Result<Team>>
{
    public int Id { get; set; }
    public string TeamName { get; set; }
    public ICollection<Player> Players { get; set; }
    
    public class Handler:IRequestHandler<CreateTeamCommand ,Result<Team>>
    {
        private readonly IDataBaseContext _context;

        public Handler(IDataBaseContext context)
        {
            _context = context;
        }

        public async Task<Result<Team>> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
        {
            var team = new Team()
            {
                Id = request.Id,
                TeamName = request.TeamName,
                Players = _context.Players
                    .Where(p => request.Players.Select(p => p.Id).Contains(p.Id)).ToList(),
            };
            try
            {

                _context.Teams.Add(team);
                await _context.SaveChangesAsync(cancellationToken);

                return new Result<Team>(true, new[] { "" }, team);
            }
            catch (Exception e)
            {
                return new Result<Team>(false, new[] { e.Message }, team);
            }
        }
    }
}