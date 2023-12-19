using FluentValidation.TestHelper;
using Forwarding.Management.Application.Quotation.Commands.RequestQuotation.Validators;
using Forwarding.Management.Domain.Enums;
using Forwarding.Management.Domain.Quotation.Model;

namespace Forwarding.Management.Application.Quotation.UnitTests.Commands.RequestQuotation;

public class CargoSpecificationsValidatorTests
{
    [Fact]
    public void Validate_PassBothDimensionsAndVolume_ValidationFails()
    {
        var specifications = new CargoSpecifications
        {
            Dimensions = new Dimensions(),
            Volume = 10,
            VolumeUnit = VolumeUnit.Cbm,
            Weight = 10,
            WeightUnit = WeightUnit.Kg
        };

        var validator = new CargoSpecificationsValidator();

        var result = validator.TestValidate(specifications);

        result.ShouldHaveValidationErrorFor(x => x);
    }

    [Fact]
    public void Validate_NoDimensionsAndVolume_ValidationPasses()
    {
        var specifications = new CargoSpecifications
        {
            Weight = 10,
            WeightUnit = WeightUnit.Kg
        };

        var validator = new CargoSpecificationsValidator();

        var result = validator.TestValidate(specifications);

        Assert.True(result.IsValid);
    }

    [Fact]
    public void Validate_NoVolumeIsPassedButUnitIsPassed_ValidationFails()
    {
        var specifications = new CargoSpecifications
        {
            VolumeUnit = VolumeUnit.Cbm,
            Weight = 10,
            WeightUnit = WeightUnit.Kg
        };

        var validator = new CargoSpecificationsValidator();

        var result = validator.TestValidate(specifications);

        result.ShouldHaveValidationErrorFor(x => x.Volume);
    }

    [Fact]
    public void Validate_NoUnitIsPassedButVolumeIsPassed_ValidationFails()
    {
        var specifications = new CargoSpecifications
        {
            Volume = 10,
            Weight = 10,
            WeightUnit = WeightUnit.Kg
        };

        var validator = new CargoSpecificationsValidator();

        var result = validator.TestValidate(specifications);

        result.ShouldHaveValidationErrorFor(x => x.VolumeUnit);
    }

    [Fact]
    public void Validate_WeightIsLessThenZero_ValidationFails()
    {
        var specifications = new CargoSpecifications
        {
            Weight = -10,
            WeightUnit = WeightUnit.Kg
        };

        var validator = new CargoSpecificationsValidator();

        var result = validator.TestValidate(specifications);

        result.ShouldHaveValidationErrorFor(x => x.Weight);
    }

    [Fact]
    public void Validate_VolumeIsLessThenZero_ValidationFails()
    {
        var specifications = new CargoSpecifications
        {
            Volume = -10,
            Weight = 10,
            WeightUnit = WeightUnit.Kg
        };

        var validator = new CargoSpecificationsValidator();

        var result = validator.TestValidate(specifications);

        result.ShouldHaveValidationErrorFor(x => x.Volume);
    }
}
