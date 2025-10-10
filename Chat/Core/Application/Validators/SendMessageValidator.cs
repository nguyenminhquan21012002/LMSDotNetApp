using Chat.Core.Application.Commands.Messages;
using FluentValidation;

namespace Chat.Core.Application.Validators
{
    public class SendMessageValidator : AbstractValidator<SendMessageCommand>
    {
        public SendMessageValidator()
        {
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Message content cannot be empty.")
                .MaximumLength(1000).WithMessage("Message content cannot exceed 1000 characters.");
        }
    }
}
