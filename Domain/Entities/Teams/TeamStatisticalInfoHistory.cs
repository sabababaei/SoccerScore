namespace Domain.Entities.Teams;

public class TeamStatisticalInfoHistory
{
    public int Id { get; set; }
    public Team Team { get; set; }
    public int LastRate { get; set; }
    public int LastRank { get; set; }
    public decimal LastPower { get; set; }
    public DateTime? LastDate { get; set; }
}