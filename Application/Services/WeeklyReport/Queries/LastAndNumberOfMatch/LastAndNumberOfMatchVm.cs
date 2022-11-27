using Domain.Entities.Matches;

namespace Application.Services.WeeklyReport.Queries.LastAndNumberOfMatch;

public class LastAndNumberOfMatchVm
{
    public Match? Match { get; set; }
    
    public int NumberOfWeeklyMatch { get; set; }
}