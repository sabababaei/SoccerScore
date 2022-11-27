using FluentValidation;

namespace Application.Services.Matches.Commands.UpdateMatch;

public class UpdateMatchCommandValidator:AbstractValidator<UpdateMatchCommand>
{
    public UpdateMatchCommandValidator()
    {
        RuleFor(m => m.MatchTeams)
            .NotNull().WithMessage("Select Teams.");
        RuleFor(m => m.MatchType)
            .NotNull().WithMessage("Select Match Type.");
        RuleFor(m => m.StartDate)
            .NotEmpty().WithMessage("Enter Start Date.");
         

    }
}