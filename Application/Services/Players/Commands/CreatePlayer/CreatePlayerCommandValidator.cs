using Domain.Entities.Players;
using FluentValidation;

namespace Application.Services.Players.Commands.CreatePlayer;

public class CreatePlayerCommandValidator:AbstractValidator<CreatePlayerCommand>
{

    public CreatePlayerCommandValidator()
    {
        RuleFor(p => p.PlayerName)
            .NotEmpty().WithMessage("Enter Player Name.")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");
    }
}