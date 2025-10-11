using FluentValidation;
using Quiz.Core.Application.DTOs.Request;

namespace Quiz.Core.Application.Validators
{
    public class CreateQuizValidator : AbstractValidator<CreateQuizDTO>
    {
        public CreateQuizValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required")
                .MaximumLength(200).WithMessage("Title cannot exceed 200 characters");

            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters");

            RuleFor(x => x.Type)
                .IsInEnum().WithMessage("Invalid quiz type");

            RuleFor(x => x.TimeLimit)
                .GreaterThan(0).When(x => x.TimeLimit.HasValue)
                .WithMessage("Time limit must be greater than 0 seconds");

            RuleFor(x => x.TotalPoints)
                .GreaterThan(0).When(x => x.TotalPoints.HasValue)
                .WithMessage("Total points must be greater than 0");

            RuleFor(x => x.CourseId)
                .NotEmpty().WithMessage("CourseId is required");

            RuleFor(x => x.CreatedBy)
                .NotEmpty().WithMessage("CreatedBy is required");
        }
    }
}

