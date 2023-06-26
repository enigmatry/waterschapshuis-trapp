using FluentValidation;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Api.Validation
{
    public static class RuleBuilderExtensions{
        public static IRuleBuilderOptions<T, TProperty> WithUniquePropertyViolationMessage<T, TProperty>(
            this IRuleBuilderOptions<T, TProperty> rule)
        {
            return rule.WithMessage(x => "'{PropertyName}' must be unique.");
        }
    }
}
