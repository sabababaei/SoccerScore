using Domain.Entities.Matches;

namespace Soccer.EndPoint.Models.Matches;

public class GameOverVm
{
    public int Id { get; set; }
    public DateTime? EndDate { get; set; }
    public ICollection<MatchTeam> MatchTeams { get; set; }
}