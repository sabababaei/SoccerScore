using Domain.Entities.Players;
using Domain.Entities.Teams;

namespace Application.Services.Teams.Queries.GetTeamList;

public class GetTeamListVm
{
    public int Id { get; set; }
    public string TeamName { get; set; }
    public Double Rank { get; set; }
    public ICollection<Player> TeamPlayers { get; set; }
}