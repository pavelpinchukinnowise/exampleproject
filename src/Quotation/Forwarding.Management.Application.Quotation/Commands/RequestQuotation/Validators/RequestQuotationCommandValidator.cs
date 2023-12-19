using System.Globalization;
using FluentValidation;
using Forwarding.Management.Application.Common.FluentValidation;
using Forwarding.Management.Application.Resources;
using Forwarding.Management.Domain.Enums;

namespace Forwarding.Management.Application.Quotation.Commands.RequestQuotation.Validators;

public class RequestQuotationCommandValidator : AbstractValidator<RequestQuotationCommand>
{
    public RequestQuotationCommandValidator()
    {
        RuleFor(x => x.StartingLocation)
            .NotNull()
            .DependentRules(() =>
            {
                RuleFor(x => x.StartingLocation.Type)
                    .IsInEnum()
                    .DependentRules(() =>
                    {
                        When(x => x.StartingLocation.Type == LocationType.Port,
                            () =>
                            {
                                RuleFor(x => x.StartingLocation.Port).NotNull()
                                    .WithMessage(string.Format(CultureInfo.CurrentCulture,
                                        Messages.PortWithSpecifiedIdDoesNotExist));
                            });

                        When(x => x.StartingLocation.Type == LocationType.Address,
                            () =>
                            {
                                RuleFor(x => x.StartingLocation.GeoLocation)
                                    .NotNull()
                                    .WithMessage(string.Format(CultureInfo.CurrentCulture,
                                        Messages.Required,
                                        nameof(RequestQuotationCommand.StartingLocation.GeoLocation)));
                            });
                    })
                    .WithMessage(string.Format(CultureInfo.CurrentCulture, Messages.IncorrectValue,
                        nameof(RequestQuotationCommand.StartingLocation.Type)));
            })
            .WithMessage(string.Format(CultureInfo.CurrentCulture, Messages.Required,
                nameof(RequestQuotationCommand.StartingLocation)));

        RuleFor(x => x.FinalLocation)
            .NotNull()
            .DependentRules(() =>
            {
                RuleFor(x => x.FinalLocation.Type)
                    .IsInEnum()
                    .DependentRules(() =>
                    {
                        When(x => x.FinalLocation.Type == LocationType.Port,
                            () =>
                            {
                                RuleFor(x => x.FinalLocation.Port).NotNull()
                                    .WithMessage(string.Format(CultureInfo.CurrentCulture,
                                        Messages.PortWithSpecifiedIdDoesNotExist));
                            });

                        When(x => x.FinalLocation.Type == LocationType.Address,
                            () =>
                            {
                                RuleFor(x => x.FinalLocation.GeoLocation)
                                    .NotNull()
                                    .WithMessage(string.Format(CultureInfo.CurrentCulture,
                                        Messages.Required,
                                        nameof(RequestQuotationCommand.FinalLocation.GeoLocation)));
                            });
                    })
                    .WithMessage(string.Format(CultureInfo.CurrentCulture, Messages.IncorrectValue,
                        nameof(RequestQuotationCommand.StartingLocation.Type)));
            })
            .WithMessage(string.Format(CultureInfo.CurrentCulture, Messages.Required,
                nameof(RequestQuotationCommand.FinalLocation)));

        RuleFor(x => x.Cargo)
            .NotNull()
            .DependentRules(() =>
            {
                When(x => x.Cargo.Type == CargoType.Containerized,
                    () =>
                    {
                        RuleFor(x => x.Cargo.Containers)
                            .NotEmpty()
                            .WithMessage(string.Format(CultureInfo.CurrentCulture,
                                Messages.Required,
                                nameof(RequestQuotationCommand.Cargo.Containers)));

                        RuleForEach(x => x.Cargo.Containers).SetValidator(new ContainerValidator());

                        RuleFor(x => x.Cargo.LooseGoodsItems)
                            .Empty()
                            .WithMessage(string.Format(CultureInfo.CurrentCulture,
                                Messages.ShouldBeNullOrEmpty,
                                nameof(RequestQuotationCommand.Cargo.LooseGoodsItems)));

                        RuleFor(x => x.Cargo.BulkItems)
                            .Empty()
                            .WithMessage(string.Format(CultureInfo.CurrentCulture,
                                Messages.ShouldBeNullOrEmpty,
                                nameof(RequestQuotationCommand.Cargo.BulkItems)));

                        RuleFor(x => x.Cargo.Specifications)
                            .Empty()
                            .WithMessage(
                                string.Format(
                                    CultureInfo.CurrentCulture,
                                    Messages.ShouldBeNullOrEmpty,
                                    nameof(RequestQuotationCommand.Cargo.Specifications)));

                    });

                When(x => x.Cargo.Type == CargoType.AsLooseGoods,
                    () =>
                    {
                        RuleFor(x => x.Cargo.LooseGoodsItems)
                            .NotEmpty()
                            .WithMessage(string.Format(CultureInfo.CurrentCulture,
                                Messages.Required,
                                nameof(RequestQuotationCommand.Cargo.LooseGoodsItems)));

                        RuleForEach(x => x.Cargo.LooseGoodsItems)
                            .ChildRules(item => item
                                .RuleFor(x => x.Specifications)
                                .SetNonNullableValidator(new CargoSpecificationsValidator())
                                .When(x => x.Specifications is not null));

                        RuleFor(x => x.Cargo.Containers)
                            .Empty()
                            .WithMessage(string.Format(CultureInfo.CurrentCulture,
                                Messages.ShouldBeNullOrEmpty,
                                nameof(RequestQuotationCommand.Cargo.Containers)));

                        RuleFor(x => x.Cargo.BulkItems)
                            .Empty()
                            .WithMessage(string.Format(CultureInfo.CurrentCulture,
                                Messages.ShouldBeNullOrEmpty,
                                nameof(RequestQuotationCommand.Cargo.BulkItems)));

                        RuleFor(x => x.Cargo)
                            .Must(x => (x.Specifications is null
                                        && x.LooseGoodsItems is not null
                                        && x.LooseGoodsItems.All(y => y.Specifications is not null))
                                       || (x.Specifications is not null
                                           && x.LooseGoodsItems is not null
                                           && x.LooseGoodsItems.All(y => y.Specifications is null)))
                            .WithMessage(Messages.SpecificationShouldExistForCargoOrItem);
                    });

                When(x => x.Cargo.Type == CargoType.InBulk,
                    () =>
                    {
                        RuleFor(x => x.Cargo.BulkItems)
                            .NotEmpty()
                            .WithMessage(string.Format(CultureInfo.CurrentCulture, Messages.Required,
                                nameof(RequestQuotationCommand.Cargo.BulkItems)));

                        RuleFor(x => x.Cargo.Containers)
                            .Empty()
                            .WithMessage(string.Format(CultureInfo.CurrentCulture,
                                Messages.ShouldBeNullOrEmpty,
                                nameof(RequestQuotationCommand.Cargo.Containers)));

                        RuleFor(x => x.Cargo.LooseGoodsItems)
                            .Empty()
                            .WithMessage(string.Format(CultureInfo.CurrentCulture,
                                Messages.ShouldBeNullOrEmpty,
                                nameof(RequestQuotationCommand.Cargo.LooseGoodsItems)));

                        RuleFor(x => x.Cargo.Specifications)
                            .Empty()
                            .WithMessage(
                                string.Format(
                                    CultureInfo.CurrentCulture,
                                    Messages.ShouldBeNullOrEmpty,
                                    nameof(RequestQuotationCommand.Cargo.Specifications)));
                    });
            })
            .WithMessage(string.Format(CultureInfo.CurrentCulture, Messages.Required,
                nameof(RequestQuotationCommand.Cargo)));

        RuleFor(x => x.TransportationMode).IsInEnum();
    }
}
