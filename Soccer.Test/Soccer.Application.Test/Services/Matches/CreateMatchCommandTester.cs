using Application.Common.Models;
using Application.Services.Matches.Commands.CreateMatch;
using Domain.Entities.Matches;
using Domain.Entities.Teams;
using Domain.Enumarations;
using Soccer.Test.Soccer.Application.Test.Common;

namespace Soccer.Test.Soccer.Application.Test.Services.Matches;

public class CreateMatchCommandTester:CommandTestBase
{
    
    [Theory]
    [Trait("Application.Services" ,"Commands")]
    [MemberData(nameof(CreateMatchCommandData_ShoulBeTrue))]
    public async void CreateMatchCommandHandlerTester(CreateMatchCommand command )
    {
        //arrange
         
        var handler = new CreateMatchCommand.Handler(_context);
        //act
        var res =await handler.Handle(command, CancellationToken.None);
        //assert
        Assert.True(res.Succeeded);
        Assert.IsType<Result<Match>>(res);
        
    }


    public static IEnumerable<object[]> CreateMatchCommandData_ShoulBeTrue()
    {
        yield return new object[]
        {
            new CreateMatchCommand()
            {
                Id = 1,
                Importance = MatchTypeImportance.Confederation,
                MatchType = MatchTypes.Confederation,
                Status = MatchStatus.Doing,
                MatchTeams = new List<MatchTeam>()
                {
                    new MatchTeam()
                    {
                        Id = 1,
                        Team = new Team()
                        {
                            Id = 1
                        }
                    },
                    new MatchTeam()
                    {
                        Id = 2,
                        Team = new Team()
                        {
                            Id = 3
                        }
                    }
                },
                StartDate = DateTime.Now,

            }
        };

        yield return new object[]
        {
            new CreateMatchCommand()
            {
                Id = 2,
                Importance = MatchTypeImportance.Friendly,
                MatchType = MatchTypes.Friendly,
                Status = MatchStatus.UnDone,
                MatchTeams = new List<MatchTeam>()
                {
                    new MatchTeam()
                    {
                        Id = 3,
                        Team = new Team()
                        {
                            Id = 1
                        }
                    },
                    new MatchTeam()
                    {
                        Id = 4,
                        Team = new Team()
                        {
                            Id = 2
                        }
                    }
                },
                StartDate = DateTime.Now.AddDays(1),

            }
        };

    }
}