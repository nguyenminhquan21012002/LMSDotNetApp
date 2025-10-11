using FluentValidation;
using Quiz.Core.Application.DTOs.Request;

namespace Quiz.Core.Application.Validators
{
    public class UpdateQuizValidator : AbstractValidator<UpdateQuizDTO>
    {
        public UpdateQuizValidator()
        {

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title cannot be empty")
                .MaximumLength(200).WithMessage("Title cannot exceed 200 characters");

            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters");

            RuleFor(x => x.TimeLimit)
                .GreaterThan(0).When(x => x.TimeLimit.HasValue)
                .WithMessage("Time limit must be greater than 0 seconds");

            RuleFor(x => x.TotalPoints)
                .GreaterThan(0).When(x => x.TotalPoints.HasValue)
                .WithMessage("Total points must be greater than 0");
        }
    }
}

