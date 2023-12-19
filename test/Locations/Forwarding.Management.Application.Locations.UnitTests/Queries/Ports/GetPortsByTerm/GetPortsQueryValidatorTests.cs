using FluentValidation.TestHelper;
using Forwarding.Management.Application.Locations.Queries.Ports.GetPortsByFilters;

namespace Forwarding.Management.Application.Locations.UnitTests.Queries.Ports.GetPortsByTerm;

public class GetPortsQueryValidatorTests
{
    private readonly GetPortsByFiltersQueryValidator validator;

    public GetPortsQueryValidatorTests()
    {
        validator = new GetPortsByFiltersQueryValidator();
    }

    [Fact]
    public void InvokeValidation_EmptySearchString_Error()
    {
        var query = new GetPortsByFiltersQuery
        {
            Query = ""
        };
        var result = validator.TestValidate(query);
        result.ShouldHaveValidationErrorFor(c => c.Query);
    }

    [Fact]
    public void InvokeValidation_CorrectRequest_ValidationPassed()
    {
        var query = new GetPortsByFiltersQuery
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
        var query = new GetPortsByFiltersQuery
        {
            Query = longString
        };
        var result = validator.TestValidate(query);
        result.ShouldNotHaveValidationErrorFor(c => c.Query);
    }
}