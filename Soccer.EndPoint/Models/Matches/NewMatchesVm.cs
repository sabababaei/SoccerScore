using Domain.Entities.Teams;
using Domain.Enumarations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Soccer.EndPoint.Models.Teams;

namespace Soccer.EndPoint.Models.Matches;

public class NewMatchesVm
{
    public int Id { get; set; }
    
    public ICollection<NewTeamVm> Teams { get; set; }
    public DateTime StartDate { get; set; }
    public MatchStatus Status { get; set; }
   
    public MatchTypes MatchTypes { get; set; }
    
    public MatchTypeImportance Importance { get; set; }
    public SelectList TeamsList { get; set; }
    public SelectList MatchType { get; set; }
    
}