using Application.Common.Models;
using Application.Services.Teams.Commands.CreateTeam;
using Domain.Entities.Players;
using Domain.Entities.Teams;
using MediatR;
using Moq;
using Soccer.Test.Soccer.Application.Test.Common;

namespace Soccer.Test.Soccer.Application.Test.Services.Teams.Commands;

public class CreateTeamsCommandTester:CommandTestBase
{
    [Fact]
    [Trait("Application.Services" ,"Commands")]
    public async void CreateTeamsCommandHandlerTester()
    {
        //arrange
       var command = new CreateTeamCommand()
        {
            Id = 1,
            Players = new List<Player>()
            {
                new Player()
                {
                    Id = 1,
                    PlayerName = "p1"
                },
                new Player()
                {
                    Id = 2,
                    PlayerName = "p2"
                }
            },
            TeamName = "team1"
        };
        
        var handler = new CreateTeamCommand.Handler(_context);
        //act
        var res = handler.Handle(command, CancellationToken.None).Result;
        //assert
        Assert.True(res.Succeeded);
        Assert.IsType<Result<Team>>(res);
        Assert.Equal("team1" , res.Data.TeamName);
    }

}