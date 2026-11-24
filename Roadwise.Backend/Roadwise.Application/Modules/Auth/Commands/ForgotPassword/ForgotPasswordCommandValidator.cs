namespace Roadwise.Application.Modules.Auth.Commands.ForgotPassword;

public sealed class ForgotPasswordCommandValidator : AbstractValidator<ForgotPasswordCommand>
{
    public ForgotPasswordCommandValidator()
    {
        // Only the format is checked: whether the account exists must not change the response.
        RuleFor(x => x.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Email je obavezan.")
            .MaximumLength(RoadwiseUserEntity.Constraints.EmailMaxLength)
            .WithMessage($"Email može imati najviše {RoadwiseUserEntity.Constraints.EmailMaxLength} znakova.")
            .EmailAddress().WithMessage("Email nije ispravan.");
    }
}
