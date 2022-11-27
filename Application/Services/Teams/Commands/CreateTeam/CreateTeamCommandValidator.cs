using FluentValidation;

namespace Application.Services.Teams.Commands.CreateTeam;

public class CreateTeamCommandValidator:AbstractValidator<CreateTeamCommand>
{
    public CreateTeamCommandValidator()
    {
        RuleFor(t => t.TeamName)
            .NotEmpty().NotNull().WithMessage("Enter Team Name.")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");
        RuleFor(t => t.Players)
            .NotNull().WithMessage("Select Team Players.");
    }
    
}