using Domain.Entities.Matches;

namespace Application.Services.WeeklyReport.Queries.MatchesInProgress;

public class MatchesInProgressVm
{
    public List<Match> Match { get; set; }
}