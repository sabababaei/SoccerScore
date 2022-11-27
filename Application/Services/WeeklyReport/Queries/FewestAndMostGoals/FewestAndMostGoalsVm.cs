namespace Application.Services.WeeklyReport.Queries.FewestAndMostGoals;

public class FewestAndMostGoalsVm
{
    public List<FewestGoalsVm> FewestGoalsTeams;
    public List<MostGoalsVm> MostGoalsTeam;
}

public class FewestGoalsVm
{
    public string TeamName { get; set; }
    public Double Rank { get; set; }
    public int CurrentRate { get; set; }
    public int WeeklyGoals { get; set; }
}

public class MostGoalsVm
{
    public string TeamName { get; set; }
    public Double Rank { get; set; }
    public int CurrentRate { get; set; }
    public int WeeklyGoals { get; set; }
}