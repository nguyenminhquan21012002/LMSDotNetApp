using Course.Core.Application.DTOs;
using FluentValidation;

namespace Course.Core.Application.Validators
{
    public class CreateCourseValidator : AbstractValidator<CreateCourseDTO>
    {
        public CreateCourseValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required")
                .MaximumLength(200).WithMessage("Title must not exceed 200 characters");
                
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required")
                .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters");
                
            RuleFor(x => x.Instructor)
                .NotEmpty().WithMessage("Instructor is required")
                .MaximumLength(100).WithMessage("Instructor name must not exceed 100 characters");
                
            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0).WithMessage("Price must be greater than or equal to 0");
                
            RuleFor(x => x.Duration)
                .GreaterThan(0).WithMessage("Duration must be greater than 0");
        }
    }
}
