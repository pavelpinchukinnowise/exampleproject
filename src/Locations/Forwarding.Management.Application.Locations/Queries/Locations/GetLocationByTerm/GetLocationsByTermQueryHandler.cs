using Forwarding.Management.Application.Locations.Contracts;
using Forwarding.Management.Domain.Location.Model;
using MediatR;

namespace Forwarding.Management.Application.Locations.Queries.Locations.GetLocationByTerm;

public class
    GetLocationsByTermQueryHandler : IRequestHandler<GetLocationsByTermQuery, IReadOnlyCollection<GeoLocation>>
{
    private readonly IGeoLocationService geoLocationService;

    public GetLocationsByTermQueryHandler(IGeoLocationService geoLocationService)
    {
        this.geoLocationService = geoLocationService;
    }

    public async Task<IReadOnlyCollection<GeoLocation>> Handle(GetLocationsByTermQuery request,
        CancellationToken cancellationToken)
        => await geoLocationService.GetLocationsByTermAsync(request.Query, cancellationToken);
}