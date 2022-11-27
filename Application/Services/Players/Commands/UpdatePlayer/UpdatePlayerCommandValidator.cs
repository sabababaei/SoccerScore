using FluentValidation;

namespace Application.Services.Players.Commands.UpdatePlayer;

public class UpdatePlayerCommandValidator:AbstractValidator<UpdatePlayerCommand>
{
    public UpdatePlayerCommandValidator()
    {
        RuleFor(p => p.PlayerName)
            .NotEmpty().WithMessage("Enter Player Name.")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");
    }   
}