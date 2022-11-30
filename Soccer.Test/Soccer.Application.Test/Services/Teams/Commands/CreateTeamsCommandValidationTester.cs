using Application.Services.Teams.Commands.CreateTeam;
using Domain.Entities.Players;
using FluentValidation.TestHelper;

namespace Soccer.Test.Soccer.Application.Test.Services.Teams.Commands;

public class CreateTeamsCommandValidationTester
{
    private readonly CreateTeamCommandValidator _validator;

    public CreateTeamsCommandValidationTester()
    {
        _validator = new CreateTeamCommandValidator();
    }
   
    [Theory]
    [Trait("Application.Services" ,"Validators")]
    [InlineData("")]
    [InlineData(null)]
    public void Should_have_error_when_name_is_null(string name)
    {
        //Arrange
        var command = new CreateTeamCommand();
        command.TeamName =name;
      //Act
        var validationResult = _validator.TestValidate(command);
        
        //assert
        validationResult.ShouldHaveValidationErrorFor(command => command.TeamName) 
            ;
        
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
        
        var model = new CreateTeamCommand() { TeamName = name };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(team=>team.TeamName);
    }

    
    [Theory]
    [Trait("Application.Services" ,"Validators")]
    [InlineData("test name")]
   
    public void Should_not_have_error_when_name_is_not_null(string name)
    {
        //Arrange
        var command = new CreateTeamCommand();
        command.TeamName =name;
        //Act
        var validationResult = _validator.TestValidate(command);
        
        //assert
        validationResult.ShouldNotHaveValidationErrorFor(command => command.TeamName) 
            ;
        
    }
    
     
   [Fact]
   [Trait("Application.Services" ,"Validators")]
    public void Should_not_have_error_when_name_is_not_null_and_players_is_not_null()
    {
       
        var command = new CreateTeamCommand();
        command.TeamName ="test name";
        command.Players = new List<Player>()
        {
            new Player()
            {
                PlayerName = "test player"
            }
        };
       
        var validationResult = _validator.TestValidate(command);
      
        validationResult.ShouldNotHaveAnyValidationErrors();
        
    }
    
    [Fact]
    [Trait("Application.Services" ,"Validators")]
    public void Should_have_error_when_player_is_null()
    {
        //Arrange
        var command = new CreateTeamCommand();
        command.Players =null;
        //Act
        var validationResult = _validator.TestValidate(command);
        
        //assert
        validationResult.ShouldHaveValidationErrorFor(command => command.Players);
        
    }
}