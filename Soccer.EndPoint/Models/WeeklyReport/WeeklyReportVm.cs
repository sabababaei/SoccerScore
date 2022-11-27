using Application.Services.WeeklyReport.Queries.BestTeam;
using Application.Services.WeeklyReport.Queries.FewestAndMostGoals;
using Application.Services.WeeklyReport.Queries.LastAndNumberOfMatch;
using Application.Services.WeeklyReport.Queries.MatchesInProgress;
using Application.Services.WeeklyReport.Queries.RankingBarChart;

namespace Soccer.EndPoint.Models.WeeklyReport;

public class WeeklyReportVm
{
    public LastAndNumberOfMatchVm LastAndNumberOfMatchVm { get; set; }
    public BestTeamVm BestTeamVm { get; set; }
    public FewestAndMostGoalsVm FewestAndMostGoalsVm { get; set; }
    public MatchesInProgressVm MatchesInProgressVm { get; set; }
    public RankingBarChartVm RankingBarChartVm{ get; set; }
}