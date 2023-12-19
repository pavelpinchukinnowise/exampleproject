using FluentValidation;
using FluentValidation.Validators;

namespace Forwarding.Management.Application.Common.FluentValidation;

internal sealed class NullableChildValidatorAdaptor<T, TProperty>
    : ChildValidatorAdaptor<T, TProperty>, IPropertyValidator<T, TProperty?>, IAsyncPropertyValidator<T, TProperty?>
{
    public NullableChildValidatorAdaptor(IValidator<TProperty> validator, Type validatorType)
        : base(validator, validatorType)
    {
    }

    public override bool IsValid(ValidationContext<T> context, TProperty? value)
    {
        if (value is null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        return base.IsValid(context, value);
    }

    public override Task<bool> IsValidAsync(
        ValidationContext<T> context,
        TProperty? value,
        CancellationToken cancellation)
    {
        if (value is null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        return base.IsValidAsync(context, value, cancellation);
    }
}
