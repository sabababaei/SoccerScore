using FluentValidation;

namespace Application.Services.Teams.Commands.UpdateTeam;

public class UpdateTeamCommandValidator:AbstractValidator<UpdateTeamCommand>
{
    public UpdateTeamCommandValidator()
    {
        RuleFor(t => t.TeamName)
            .NotEmpty().WithMessage("Enter Team Name.")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");
        RuleFor(t => t.Players)
            .NotNull().WithMessage("Select Team Players.");
    }
}