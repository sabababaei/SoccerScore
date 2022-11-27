using Application.Common.Models;
using Application.Interfaces;
using Domain.Entities.Matches;
using Domain.Enumarations;
using MediatR;
 

namespace Application.Services.Matches.Commands.CreateMatch;

public class CreateMatchCommand : IRequest<Result<Match>>
{
    public int Id { get; set; }
    public ICollection<MatchTeam> MatchTeams { get; set; }
    public DateTime StartDate { get; set; }
    public MatchStatus Status { get; set; }
    public MatchTypes MatchType { get; set; }
    public MatchTypeImportance Importance { get; set; }

    public class Handler : IRequestHandler<CreateMatchCommand, Result<Match>>
    {
        private readonly IDataBaseContext _context;

        public Handler(IDataBaseContext context)
        {

            _context = context;
        }

        public async Task<Result<Match>> Handle(CreateMatchCommand request, CancellationToken cancellationToken)
        {
            var match = new Match()
            {
                Id = request.Id,
                Status = request.Status,
                StartDate = request.StartDate,
                MatchTeams = _context.Teams.Where(t => request.MatchTeams.Select(m => m.Team)
                    .Select(t => t.Id).Contains(t.Id)).Select(m => new MatchTeam() { Team = m }).ToList(),
                MatchType = request.MatchType,
                Importance = request.Importance
            };
            try
            {

                _context.Matches.Add(match);
                await _context.SaveChangesAsync(cancellationToken);

                return new Result<Match>(true, new []{""}, match);
            }
            catch (Exception e)
            {
                return new Result<Match>(false, new[] { e.Message }, match);
            }

        }
    }

}