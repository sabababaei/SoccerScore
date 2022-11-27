namespace Soccer.EndPoint.Models.Teams;

public class TeamOfSameStrengthVm
{
    public int Id { get; set; }
    public string TeamName { get; set; }
    public int MatchesCount { get; set; }
    public int Rate { get; set; }
    public decimal Rank { get; set; }
    public decimal Power { get; set; }
    public string TeamPlayers { get; set; }
}