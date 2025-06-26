using Application.Commons.Models.Pageination;
using Domain.Constants;
using Domain.Extentions;
using FluentValidation;

namespace Application.Commons.Validation.Pagination
{
    public class PageRequestValidator : AbstractValidator<PageRequest>
    {
        public PageRequestValidator()
        {
            When(val => val.PageNumber.IsNotNull(), () =>
            {
                RuleFor(e => e.PageNumber).GreaterThanOrEqualTo(1);
            });

            When(val => val.PageSize.IsNotNull(), () =>
            {
                RuleFor(e => e.PageSize).GreaterThanOrEqualTo(1).LessThanOrEqualTo(MaxLength.PAGESIZE_MAX_VALUE);
            });
        }
    }
}
