using Application.Services.Players.Queries.GetPlayerList;
using Domain.Entities.Players;
using Microsoft.AspNetCore.Mvc.Rendering;
using Soccer.EndPoint.Models.Players;

namespace Soccer.EndPoint.Models.Teams;

public class NewTeamVm
{
    public int Id { get; set; }
    public string TeamName { get; set; }
    public ICollection<NewPlayerVm> Players { get; set; }
    public SelectList PlayersList { get; set; }
}