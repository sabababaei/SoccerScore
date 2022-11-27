using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.Matches;
using Domain.Entities.Players;

namespace Domain.Entities.Teams;

public class Team
{
    public int Id { get; set; }
    public string TeamName { get; set; }
    public int CurrentRate { get; set; }
    public int Rank { get; set; }
    public decimal Power { get; set; }
     
    public ICollection<Player> Players { get; set; }
   
    public ICollection<MatchTeam> MatchTeams { get; set; }
    [DefaultValue(0)]
   
     
    public ICollection<TeamStatisticalInfoHistory> TeamRates { get; set; }

}