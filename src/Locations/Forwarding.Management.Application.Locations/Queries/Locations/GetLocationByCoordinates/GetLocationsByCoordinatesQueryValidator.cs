using System.Globalization;
using FluentValidation;
using Forwarding.Management.Application.Resources;

namespace Forwarding.Management.Application.Locations.Queries.Locations.GetLocationByCoordinates;

public class GetLocationsByCoordinatesQueryValidator : AbstractValidator<GetLocationsByCoordinatesQuery>
{
    public GetLocationsByCoordinatesQueryValidator()
    {
        RuleFor(x => x.Latitude)
            .NotNull()
            .WithMessage(string.Format(CultureInfo.CurrentCulture, Messages.Required,
                nameof(GetLocationsByCoordinatesQuery.Latitude)));

        RuleFor(x => x.Longitude)
            .NotNull()
            .WithMessage(string.Format(CultureInfo.CurrentCulture, Messages.Required,
                nameof(GetLocationsByCoordinatesQuery.Longitude)));
    }
}