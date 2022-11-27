using Domain.Entities.Matches;
using FluentValidation;

namespace Application.Services.Matches.Commands.GameOver;

public class GameOverCommandValidator:AbstractValidator<GameOverCommand>
{
    public GameOverCommandValidator()
    {
        RuleFor(m => m.MatchTeams)
            .NotNull().WithMessage("Select Teams.");
        
        RuleFor(m => m.EndDate)
            .NotEmpty().WithMessage("Enter End Date.");

        RuleForEach(m=>m.MatchTeams)
            .SetValidator(new InlineValidator<MatchTeam>()
            {
                v=>v.RuleFor(t=>t.NumberOfGoal).NotNull(),
                v=>v.RuleFor(t=>t.Score).NotNull(),
                v=>v.RuleFor(t=>t.Result).NotNull()
            });

    }
}