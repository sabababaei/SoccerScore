using Domain.Entities.Teams;
using Domain.Enumarations;

namespace Soccer.EndPoint.Models.Matches;

public class MatchListVm
{
    public int Id { get; set; }
    
    public string MatchTeamsName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public MatchStatus Status { get; set; }
    public MatchTypes MatchType { get; set; }
    public MatchTypeImportance Importance { get; set; }

}