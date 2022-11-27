using Domain.Entities.Matches;
using Domain.Entities.Teams;
using Domain.Enumarations;

namespace Application.Services.Matches.Queries.GetMatchDetail;

public class GetMatchDetailVm
{
    public int Id { get; set; }
    public ICollection<MatchTeam> MatchTeams { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public MatchStatus Status { get; set; }
    public MatchTypes MatchType { get; set; }
    public MatchTypeImportance Importance { get; set; }
}