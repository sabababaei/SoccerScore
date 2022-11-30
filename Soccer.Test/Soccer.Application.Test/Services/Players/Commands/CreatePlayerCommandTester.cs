using Application.Common.Models;
using Application.Services.Players.Commands.CreatePlayer;
using Domain.Entities.Players;
 
using Soccer.Test.Soccer.Application.Test.Common;
using Xunit.Abstractions;

namespace Soccer.Test.Soccer.Application.Test.Services.Players.Commands;

public class CreatePlayerCommandTester:CommandTestBase
{
    private readonly ITestOutputHelper _helper;

    public CreatePlayerCommandTester(ITestOutputHelper helper )
    {
        _helper = helper;
    }
    [Fact]
    [Trait("Application.Services" ,"Commands")]
    public async void CreatePlayerCommandHandlerTester()
    {
        //arange
       
        var command = new CreatePlayerCommand()
        {
            Id=1 , 
            PlayerName = "test"
        };
        var handler = new CreatePlayerCommand.Handler(_context );
        //act
        var res = await handler.Handle(command, CancellationToken.None);
     
        //assert
        Assert.True(res.Succeeded);
        Assert.Equal("test" ,res.Data.PlayerName);
        Assert.IsType<Result<Player>>(res);

    }

}