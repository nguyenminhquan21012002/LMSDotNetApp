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
        }
    }
}
