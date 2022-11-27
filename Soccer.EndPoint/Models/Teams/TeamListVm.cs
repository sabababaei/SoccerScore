using Soccer.EndPoint.Models.Players;

namespace Soccer.EndPoint.Models.Teams;

public class TeamListVm
{
    public int Id { get; set; }
    public string TeamName { get; set; }
    public Double Rank { get; set; }
    public ICollection<PlayerListVm> Players { get; set; }
}