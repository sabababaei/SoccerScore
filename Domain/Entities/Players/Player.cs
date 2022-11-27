using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using Domain.Entities.Teams;

namespace Domain.Entities.Players;

public class Player
{
    public int Id { get; set; }
    public string PlayerName { get; set; }
    
    public ICollection<Team> Teams { get; set; }
}