using MediatR;

namespace Application.Services.Players.Queries.GetPlayerList;

public class GetPlayerListVm
{
    public int Id { get; set; }
    public string PlayerName { get; set; }
}