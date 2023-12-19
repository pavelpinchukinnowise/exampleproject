using AutoFixture;
using FluentValidation.TestHelper;
using Forwarding.Management.Application.Quotation.Commands.RequestQuotation;
using Forwarding.Management.Application.Quotation.Commands.RequestQuotation.Validators;
using Forwarding.Management.Domain.Enums;
using Forwarding.Management.Domain.Location.Model;
using Forwarding.Management.Domain.Quotation.Model;

namespace Forwarding.Management.Application.Quotation.UnitTests.Commands.RequestQuotation;

public class RequestQuotationCommandValidatorTests
{
    private readonly RequestQuotationCommandValidator validator;
    private readonly Fixture fixture;

    public RequestQuotationCommandValidatorTests()
    {
        validator = new RequestQuotationCommandValidator();
        fixture = new Fixture();
    }

    [Fact]
    public async Task InvokeValidation_IncorrectFromLocation_Error()
    {
        var command = fixture.Create<RequestQuotationCommand>();
        command.StartingLocation.PortId = null;
        command.StartingLocation.Port = null;
        command.StartingLocation.Type = LocationType.Port;

        var result = await validator.TestValidateAsync(command);
        Assert.False(result.IsValid);
        result.ShouldHaveValidationErrorFor(x => x.StartingLocation.Port);
    }

    [Fact]
    public async Task InvokeValidation_IncorrectToLocation_Error()
    {
        var command = fixture.Create<RequestQuotationCommand>();
        command.FinalLocation.PortId = null;
        command.FinalLocation.Port = null;
        command.FinalLocation.Type = LocationType.Port;

        var result = await validator.TestValidateAsync(command);
        Assert.False(result.IsValid);
        result.ShouldHaveValidationErrorFor(x => x.FinalLocation.Port);
    }

    [Fact]
    public async Task InvokeValidation_IncorrectShippingContainer_Error()
    {
        var command = fixture.Create<RequestQuotationCommand>();
        command.Cargo.Containers = null;
        command.Cargo.Type = CargoType.Containerized;

        var result = await validator.TestValidateAsync(command);
        Assert.False(result.IsValid);
        result.ShouldHaveValidationErrorFor(x => x.Cargo.Containers);
    }

    [Fact]
    public async Task InvokeValidation_IncorrectShippingBulk_Error()
    {
        var command = fixture.Create<RequestQuotationCommand>();
        command.Cargo.BulkItems = null;
        command.Cargo.Type = CargoType.InBulk;

        var result = await validator.TestValidateAsync(command);
        Assert.False(result.IsValid);
        result.ShouldHaveValidationErrorFor(x => x.Cargo.BulkItems);
    }

    [Fact]
    public async Task InvokeValidation_IncorrectShippingLooseGoodsItem_Error()
    {
        var command = fixture.Create<RequestQuotationCommand>();
        command.Cargo.LooseGoodsItems = null;
        command.Cargo.Type = CargoType.AsLooseGoods;

        var result = await validator.TestValidateAsync(command);
        Assert.False(result.IsValid);
        result.ShouldHaveValidationErrorFor(x => x.Cargo.LooseGoodsItems);
    }

    [Theory]
    [InlineData(LocationType.Address)]
    [InlineData(LocationType.Port)]
    public async Task InvokeValidation_CorrectLocations_ValidationPassed(LocationType containerLoading)
    {
        var model = fixture.Build<RequestQuotationCommand>().Create();

        model.StartingLocation.Type = containerLoading;
        model.FinalLocation.Type = containerLoading;
        model.Cargo.Type = CargoType.Containerized;
        model.Cargo.LooseGoodsItems = null;
        model.Cargo.BulkItems = null;
        model.Cargo.Specifications = null;

        if (model.Cargo.Containers is not null)
        {
            foreach (var container in model.Cargo.Containers)
            {
                container.Specifications = new CargoSpecifications
                {
                    Weight = 10,
                    WeightUnit = WeightUnit.Kg
                };
            }
        }

        model.StartingLocation.PortId = containerLoading == LocationType.Port ? 1 : null;
        model.FinalLocation.PortId = containerLoading == LocationType.Port ? 2 : null;

        model.FinalLocation.GeoLocation = containerLoading == LocationType.Address
            ? fixture.Create<GeoLocation>()
            : null;
        model.FinalLocation.GeoLocation = containerLoading == LocationType.Address
            ? fixture.Create<GeoLocation>()
            : null;

        var result = await validator.TestValidateAsync(model);
        Assert.True(result.IsValid);
        result.ShouldNotHaveValidationErrorFor(x => x.StartingLocation.PortId);
        result.ShouldNotHaveValidationErrorFor(x => x.FinalLocation.PortId);
        result.ShouldNotHaveValidationErrorFor(x => x.FinalLocation.GeoLocation);
        result.ShouldNotHaveValidationErrorFor(x => x.FinalLocation.GeoLocation);
    }

    [Theory]
    [InlineData(CargoType.AsLooseGoods)]
    [InlineData(CargoType.Containerized)]
    [InlineData(CargoType.InBulk)]
    public async Task InvokeValidation_CorrectShipment_ValidationPassed(CargoType cargoType)
    {
        var model = fixture.Build<RequestQuotationCommand>().Create();
        if (cargoType == CargoType.AsLooseGoods)
        {
            model.Cargo.Containers = null;
            model.Cargo.BulkItems = null;

            if (model.Cargo.LooseGoodsItems is not null)
            {
                foreach (var looseGoodsItem in model.Cargo.LooseGoodsItems)
                {
                    looseGoodsItem.Specifications = new CargoSpecifications
                    {
                        Weight = 10,
                        WeightUnit = WeightUnit.Kg
                    };
                }
            }
        }
        else if (cargoType == CargoType.Containerized)
        {
            model.Cargo.LooseGoodsItems = null;
            model.Cargo.BulkItems = null;

            if (model.Cargo.Containers is not null)
            {
                foreach (var container in model.Cargo.Containers)
                {
                    container.Specifications = new CargoSpecifications
                    {
                        Weight = 10,
                        WeightUnit = WeightUnit.Kg
                    };
                }
            }
        }
        else if (cargoType == CargoType.InBulk)
        {
            model.Cargo.LooseGoodsItems = null;
            model.Cargo.Containers = null;
        }

        model.Cargo.Type = cargoType;
        model.Cargo.Specifications = null;

        var result = await validator.TestValidateAsync(model);
        Assert.True(result.IsValid);
        result.ShouldNotHaveValidationErrorFor(x => x.Cargo.LooseGoodsItems);
        result.ShouldNotHaveValidationErrorFor(x => x.Cargo.BulkItems);
        result.ShouldNotHaveValidationErrorFor(x => x.Cargo.Containers);
        result.ShouldNotHaveValidationErrorFor(x => x.Cargo.Type);
    }

    [Fact]
    public void Validate_ContainerizedCargoTypeAndTotalSpecificationsArePassed_ValidationFails()
    {
        var command = new RequestQuotationCommand
        {
            Cargo = new Cargo
            {
                Type = CargoType.Containerized,
                Containers = new List<Container>
                {
                    new()
                    {
                        Specifications = new CargoSpecifications
                        {
                            Weight = 10,
                            WeightUnit = WeightUnit.Kg
                        }
                    }
                },
                Specifications = new CargoSpecifications
                {
                    Weight = 10,
                    WeightUnit = WeightUnit.Kg
                }
            },
            TransportationMode = fixture.Create<TransportationMode>(),
            StartingLocation = fixture.Create<Location>(),
            FinalLocation = fixture.Create<Location>()
        };

        var result = validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.Cargo.Specifications);
    }

    [Fact]
    public void Validate_LooseGoodsSpecificationsAndTotalSpecificationsArePassed_ValidationFails()
    {
        var command = new RequestQuotationCommand
        {
            Cargo = new Cargo
            {
                Type = CargoType.AsLooseGoods,
                LooseGoodsItems = new List<LooseGoodsItem>
                {
                    new()
                    {
                        Name = fixture.Create<string>(),
                        Specifications = new CargoSpecifications
                        {
                            Weight = 10,
                            WeightUnit = WeightUnit.Kg
                        }
                    }
                },
                Specifications = new CargoSpecifications
                {
                    Weight = 10,
                    WeightUnit = WeightUnit.Kg
                }
            },
            TransportationMode = fixture.Create<TransportationMode>(),
            StartingLocation = fixture.Create<Location>(),
            FinalLocation = fixture.Create<Location>()
        };

        var result = validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.Cargo);
    }
}
