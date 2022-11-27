using Domain.Entities.Teams;
using Domain.Enumarations;

namespace Domain.Entities.Matches;

public class MatchTeam
{
    public int Id { get; set; }
    public Team Team { get; set; }
    public Match Match { get; set; }
    public MatchResult Result { get; set; }
    public Int16 NumberOfGoal { get; set; }
    public int Score { get; set; }
}