namespace Application.Services.Teams.Queries.GetTeamSummery.Queries.GetTeamStatisticalInfoHistory;

public class GetTeamStatisticalInfoHistoryChartVm
{
    public int Id { get; set; }
    public string TeamsName { get; set; }
    public string LastRate { get; set; }
    public string LastRank { get; set; }
    public string LastPower { get; set; }
    public string LastDate { get; set; }
}