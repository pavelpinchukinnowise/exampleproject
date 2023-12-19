using System.Globalization;
using FluentValidation;
using Forwarding.Management.Application.Locations.Queries.Locations.GetLocationByTerm;
using Forwarding.Management.Application.Resources;

namespace Forwarding.Management.Application.Locations.Queries.Ports.GetPortsByFilters;
public class GetPortsByFiltersQueryValidator : AbstractValidator<GetPortsByFiltersQuery>
{
    public GetPortsByFiltersQueryValidator()
    {
        RuleFor(x => x.Query)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(string.Format(CultureInfo.CurrentCulture, Messages.Required, nameof(GetLocationsByTermQuery.Query)))
            .MaximumLength(512)
            .WithMessage(string.Format(CultureInfo.CurrentCulture, Messages.MaxLength, 512));
    }
}
