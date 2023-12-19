using FluentValidation.TestHelper;
using Forwarding.Management.Application.Quotation.Commands.RequestQuotation.Validators;
using Forwarding.Management.Domain.Enums;
using Forwarding.Management.Domain.Quotation.Model;

namespace Forwarding.Management.Application.Quotation.UnitTests.Commands.RequestQuotation;

public class DimensionsValidatorTests
{
    [Fact]
    public void Validate_PassOnlyLength_ValidationFails()
    {
        var dimensions = new Dimensions { Length = 10 };
        var validator = new DimensionsValidator();

        var result = validator.TestValidate(dimensions);

        result.ShouldHaveValidationErrorFor(x => x.Height);
        result.ShouldHaveValidationErrorFor(x => x.Width);
        result.ShouldHaveValidationErrorFor(x => x.Unit);
    }

    [Fact]
    public void Validate_PassOnlyWidth_ValidationFails()
    {
        var dimensions = new Dimensions { Width = 10 };
        var validator = new DimensionsValidator();

        var result = validator.TestValidate(dimensions);

        result.ShouldHaveValidationErrorFor(x => x.Height);
        result.ShouldHaveValidationErrorFor(x => x.Length);
        result.ShouldHaveValidationErrorFor(x => x.Unit);
    }

    [Fact]
    public void Validate_PassOnlyHeight_ValidationFails()
    {
        var dimensions = new Dimensions { Height = 10 };
        var validator = new DimensionsValidator();

        var result = validator.TestValidate(dimensions);

        result.ShouldHaveValidationErrorFor(x => x.Length);
        result.ShouldHaveValidationErrorFor(x => x.Width);
        result.ShouldHaveValidationErrorFor(x => x.Unit);
    }

    [Fact]
    public void Validate_PassOnlyUnit_ValidationFails()
    {
        var dimensions = new Dimensions { Unit = LengthUnit.Cm };
        var validator = new DimensionsValidator();

        var result = validator.TestValidate(dimensions);

        result.ShouldHaveValidationErrorFor(x => x.Height);
        result.ShouldHaveValidationErrorFor(x => x.Width);
        result.ShouldHaveValidationErrorFor(x => x.Length);
    }
}
