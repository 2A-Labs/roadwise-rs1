namespace Roadwise.Application.Modules.Auth.Commands.UpdateMe;

/// <summary>
/// Same rules for these fields as <see cref="Register.RegisterCommandValidator"/>.
/// </summary>
public sealed class UpdateMeCommandValidator : AbstractValidator<UpdateMeCommand>
{
    public UpdateMeCommandValidator()
    {
        RuleFor(x => x.Firstname)
            .NotEmpty().WithMessage("Ime je obavezno.")
            .MaximumLength(RoadwiseUserEntity.Constraints.FirstnameMaxLength)
            .WithMessage($"Ime može imati najviše {RoadwiseUserEntity.Constraints.FirstnameMaxLength} znakova.");

        RuleFor(x => x.Lastname)
            .NotEmpty().WithMessage("Prezime je obavezno.")
            .MaximumLength(RoadwiseUserEntity.Constraints.LastnameMaxLength)
            .WithMessage($"Prezime može imati najviše {RoadwiseUserEntity.Constraints.LastnameMaxLength} znakova.");

        RuleFor(x => x.Address)
            .MaximumLength(RoadwiseUserEntity.Constraints.AddressMaxLength)
            .WithMessage($"Adresa može imati najviše {RoadwiseUserEntity.Constraints.AddressMaxLength} znakova.");

        RuleFor(x => x.City)
            .MaximumLength(RoadwiseUserEntity.Constraints.CityMaxLength)
            .WithMessage($"Grad može imati najviše {RoadwiseUserEntity.Constraints.CityMaxLength} znakova.");
    }
}
