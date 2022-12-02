using Domain.Enumarations;
using FluentValidation;

namespace Application.Services.Matches.Commands.CreateMatch;

public class CreateMatchCommandValidator:AbstractValidator<CreateMatchCommand>
{
    public CreateMatchCommandValidator()
    {
        RuleFor(m => m.MatchTeams)
            .NotNull().WithMessage("Select Teams.");
        
         
        RuleFor(m => m.MatchType)
            .NotNull().WithMessage("Select Match Type.")
            .IsInEnum();
            
            RuleFor(m=>m.MatchType.ToString())
            .Equal(  m=>m.Importance.ToString());
           
        
        RuleFor(m => m.Importance)
            .NotNull().WithMessage("Select Match Importance.")
            .IsInEnum();
        
        RuleFor(m => m.StartDate)
            .NotEmpty().WithMessage("Enter Start Date.");
        
        RuleFor(m => m.Status)
            .NotNull().WithMessage("Select Match Status.")
            .NotEqual(MatchStatus.Done)
            .IsInEnum();
        
        RuleFor(m => m.MatchTeams.Count)
            .Equal(2);

    }
}