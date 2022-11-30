using Application.Services.Players.Commands.CreatePlayer;
using FluentValidation.TestHelper;

namespace Soccer.Test.Soccer.Application.Test.Services.Players.Commands;


public class CreatePlayerCommandValidationTester
{
 
    private CreatePlayerCommandValidator _validator;

    public CreatePlayerCommandValidationTester()
    {
        _validator =new CreatePlayerCommandValidator();
    }

    [Theory]
    [Trait("Application.Services" ,"Validators")]
    [InlineData("")]
    [InlineData(null)]
    public void Should_have_error_when_name_is_null(string name)
    {
        var model = new CreatePlayerCommand() { PlayerName =name };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(player => player.PlayerName);
        
    }

    [Theory]
    [Trait("Application.Services" ,"Validators")]
    [InlineData(105)]
    [InlineData(101)]
    public void Should_have_error_when_name_exceed_100characters(int length)
    {
        var random = new Random();
        const string pool = "abcdefghijklmnopqrstuvwxyz0123456789";
        var chars = Enumerable.Range(0, length)
            .Select(x => pool[random.Next(0, pool.Length)]);
        var name = new string(chars.ToArray());
        
        var model = new CreatePlayerCommand() { PlayerName = name };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(player => player.PlayerName);

    }
}