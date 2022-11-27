using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using Domain.Enumarations;
 
namespace Domain.Entities.Matches;

public class Match
{
    public int Id { get; set; }
    
    public ICollection<MatchTeam> MatchTeams { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public MatchStatus Status { get; set; }
    public MatchTypes MatchType { get; set; }
    public MatchTypeImportance Importance { get; set; }
}