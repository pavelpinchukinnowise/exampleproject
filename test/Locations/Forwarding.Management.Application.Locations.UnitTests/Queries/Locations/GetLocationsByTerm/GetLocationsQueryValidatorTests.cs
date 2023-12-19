using FluentValidation.TestHelper;
using Forwarding.Management.Application.Locations.Queries.Locations.GetLocationByTerm;

namespace Forwarding.Management.Application.Locations.UnitTests.Queries.Locations.GetLocationsByTerm;

public class GetLocationsQueryValidatorTests
{
    private readonly GetLocationsByTermQueryValidator validator;

    public GetLocationsQueryValidatorTests()
    {
        validator = new GetLocationsByTermQueryValidator();
    }

    [Fact]
    public void InvokeValidation_EmptySearchString_Error()
    {
        var query = new GetLocationsByTermQuery
        {
            Query = ""
        };
        var result = validator.TestValidate(query);
        result.ShouldHaveValidationErrorFor(c => c.Query);
    }

    [Fact]
    public void InvokeValidation_CorrectRequest_ValidationPassed()
    {
        var query = new GetLocationsByTermQuery
        {
            Query = "123"
        };
        var result = validator.TestValidate(query);
        result.ShouldNotHaveValidationErrorFor(c => c.Query);
    }

    [Fact]
    public void InvokeValidation_TooLongString_Error()
    {
        var longString = new string(Enumerable.Repeat('1', 512).Select(x => x).ToArray());
        var query = new GetLocationsByTermQuery
        {
            Query = longString
        };
        var result = validator.TestValidate(query);
        result.ShouldNotHaveValidationErrorFor(c => c.Query);
    }
}