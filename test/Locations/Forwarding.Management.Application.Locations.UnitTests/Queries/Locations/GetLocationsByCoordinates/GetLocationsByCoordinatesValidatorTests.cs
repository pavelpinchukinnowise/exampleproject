using FluentValidation.TestHelper;
using Forwarding.Management.Application.Locations.Queries.Locations.GetLocationByCoordinates;

namespace Forwarding.Management.Application.Locations.UnitTests.Queries.Locations.GetLocationsByCoordinates;

public class GetLocationsByCoordinatesValidatorTests
{
    private readonly GetLocationsByCoordinatesQueryValidator validator;

    public GetLocationsByCoordinatesValidatorTests()
    {
        validator = new GetLocationsByCoordinatesQueryValidator();
    }

    [Theory]
    [InlineData(1, 1)]
    [InlineData(0, 0)]
    [InlineData(-1, -2)]
    public void GetLocationsQueryValidator_CorrectRequest_ValidationPassed(double lat, double lon)
    {
        var query = new GetLocationsByCoordinatesQuery
        {
            Longitude = lon,
            Latitude = lat
        };
        var result = validator.TestValidate(query);
        result.ShouldNotHaveValidationErrorFor(c => c.Latitude);
    }
}