using Domain.Entities.Players;
using Domain.Entities.Teams;

namespace Application.Services.Teams.Queries.GetTeamDetail;

public class GetTeamDetailVm
{
    public int Id { get; set; }
    public string TeamName { get; set; }
    public int CurrentRate { get; set; }
    public int Rank { get; set; }
    public decimal Power { get; set; }
   public string TeamPlayers { get; set; }
}