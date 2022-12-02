using Application.Services.Matches.Commands.CreateMatch;
using Domain.Entities.Matches;
using Domain.Entities.Teams;
using Domain.Enumarations;
using FluentValidation.TestHelper;

namespace Soccer.Test.Soccer.Application.Test.Services.Matches;

public class CreateMatchCommandValidationTester
{
    private readonly CreateMatchCommandValidator _validator;

    public CreateMatchCommandValidationTester()
    {
        _validator = new CreateMatchCommandValidator();
    }

    [Theory]
    [Trait("Application.Services", "Validators")]
    [MemberData(nameof(CreateMatchCommandData_ShoulBeTrue))]
    public void Should_Be_True(CreateMatchCommand command)
    {
        //Arrange

        //Act
        var validationResult = _validator.TestValidate(command);

        //assert
        validationResult.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [Trait("Application.Services", "Validators")]
    [MemberData(nameof(CreateMatchCommandData_ShoulNotBeTrue))]
    public void Should_Not_Be_True(CreateMatchCommand command)
    {
        //Arrange

        //Act
        var validationResult = _validator.TestValidate(command);

        //assert
        validationResult.ShouldHaveAnyValidationError();
    }

    public static IEnumerable<object[]> CreateMatchCommandData_ShoulNotBeTrue()
    {
        yield return new object[]
        {
            new CreateMatchCommand()
            {
                Id = 2,
                Importance = MatchTypeImportance.Confederation,
                MatchType = MatchTypes.Friendly,
                Status = MatchStatus.UnDone,
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
                Id = 3,
                Importance = MatchTypeImportance.Confederation,
                MatchType = MatchTypes.Confederation,
                Status = MatchStatus.UnDone,
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

                },
                StartDate = DateTime.Now,

            }
        };

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