using Forwarding.Management.Application.Locations.Queries.Ports.DTOs;
using Forwarding.Management.Domain.Location.Contracts;
using MediatR;

namespace Forwarding.Management.Application.Locations.Queries.Ports.GetPortsByFilters;

public class
    GetPortsByFiltersHandler : IRequestHandler<GetPortsByFiltersQuery, IReadOnlyCollection<PortDto>>
{
    private readonly IPortsService portsService;

    public GetPortsByFiltersHandler(IPortsService portsService)
    {
        this.portsService = portsService;
    }

    public async Task<IReadOnlyCollection<PortDto>> Handle(GetPortsByFiltersQuery request,
        CancellationToken cancellationToken)
    {
        return (await portsService.GetPortsByFiltersAsync(request.Query, request.TransportationMode, cancellationToken)).Select(x =>
            new PortDto
            {
                Id = x.Id,
                Address = x.Address,
                Country = x.Country,
                CountryCode = x.CountryCode,
                Latitude = x.Latitude,
                Longitude = x.Longitude,
                PostalCode = x.PostalCode,
                InternationalCode = x.InternationalCode,
                Name = x.Name,
                Type = x.Type

            }).ToList();
    }
}