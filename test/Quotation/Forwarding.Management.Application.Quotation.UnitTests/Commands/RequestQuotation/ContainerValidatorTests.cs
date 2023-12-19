using FluentValidation.TestHelper;
using Forwarding.Management.Application.Quotation.Commands.RequestQuotation.Validators;
using Forwarding.Management.Domain.Enums;
using Forwarding.Management.Domain.Quotation.Model;

namespace Forwarding.Management.Application.Quotation.UnitTests.Commands.RequestQuotation;

public class ContainerValidatorTests
{
    [Fact]
    public void Validate_PassFclContainerAndDimensions_ValidationFails()
    {
        var container = new Container
        {
            LoadingMode = ContainerLoadingMode.Fcl,
            Specifications = new CargoSpecifications
            {
                Dimensions = new Dimensions(),
                Weight = 10,
                WeightUnit = WeightUnit.Kg
            }
        };

        var validator = new ContainerValidator();

        var result = validator.TestValidate(container);

        result.ShouldHaveValidationErrorFor(x => x.Specifications);
    }
}
