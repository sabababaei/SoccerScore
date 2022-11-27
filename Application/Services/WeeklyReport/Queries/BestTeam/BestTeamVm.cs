namespace Application.Services.WeeklyReport.Queries.BestTeam;

public class BestTeamVm
{
    public List<BestTeamInfo> BestTeams { get; set; }
}
public class BestTeamInfo
{
    public string TeamName { get; set; }
    public Double Rank { get; set; }
    public int CurrentRate { get; set; }
}