namespace Roadwise.Application.Modules.Auth.Commands.Register;

public sealed class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.Firstname)
            .NotEmpty().WithMessage("Ime je obavezno.")
            .MaximumLength(RoadwiseUserEntity.Constraints.FirstnameMaxLength)
            .WithMessage($"Ime može imati najviše {RoadwiseUserEntity.Constraints.FirstnameMaxLength} znakova.");

        RuleFor(x => x.Lastname)
            .NotEmpty().WithMessage("Prezime je obavezno.")
            .MaximumLength(RoadwiseUserEntity.Constraints.LastnameMaxLength)
            .WithMessage($"Prezime može imati najviše {RoadwiseUserEntity.Constraints.LastnameMaxLength} znakova.");

        // Only the request itself is checked here (no database access, see ValidationBehavior).
        // An email that is already registered needs the database, so it is checked by RegisterCommandHandler (409).
        // Cascade.Stop: an empty email is reported only as "obavezan", not also as "nije ispravan".
        RuleFor(x => x.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Email je obavezan.")
            .MaximumLength(RoadwiseUserEntity.Constraints.EmailMaxLength)
            .WithMessage($"Email može imati najviše {RoadwiseUserEntity.Constraints.EmailMaxLength} znakova.")
            .EmailAddress().WithMessage("Email nije ispravan.");

        RuleFor(x => x.Password)
            .ValidPassword();

        RuleFor(x => x.Address)
            .MaximumLength(RoadwiseUserEntity.Constraints.AddressMaxLength)
            .WithMessage($"Adresa može imati najviše {RoadwiseUserEntity.Constraints.AddressMaxLength} znakova.");

        RuleFor(x => x.City)
            .MaximumLength(RoadwiseUserEntity.Constraints.CityMaxLength)
            .WithMessage($"Grad može imati najviše {RoadwiseUserEntity.Constraints.CityMaxLength} znakova.");
    }
}
