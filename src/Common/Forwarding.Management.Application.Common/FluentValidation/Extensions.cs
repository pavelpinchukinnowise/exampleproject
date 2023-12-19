using FluentValidation;

namespace Forwarding.Management.Application.Common.FluentValidation;

public static class Extensions
{
    public static IRuleBuilderOptions<T, TProperty?> SetNonNullableValidator<T, TProperty>(
        this IRuleBuilder<T, TProperty?> ruleBuilder,
        IValidator<TProperty> validator,
        params string[] ruleSets)
    {
        var adapter = new NullableChildValidatorAdaptor<T, TProperty>(validator, validator.GetType())
        {
            RuleSets = ruleSets
        };

        return ruleBuilder.SetValidator(adapter);
    }
}
