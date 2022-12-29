using FluentValidation;
using nowe_Restaurant_API.Entities;

namespace nowe_Restaurant_API.Models.Validators
{
    public class SearchQueryValidator : AbstractValidator<SearchQuery>
    {
        private int[] pages = new [] { 5, 10, 15 };
        private string[] allowedsortby = { nameof(Restaurant.Name), nameof(Restaurant.Destripcion), nameof(Restaurant.Category) };
        public SearchQueryValidator()
        {
            RuleFor(s => s.pagesize).GreaterThan(0);
            RuleFor(s => s.pagenumber).GreaterThan(0);

            RuleFor(a => a.pagesize).Custom((Value, Context) =>
            {
                if (!pages.Contains(Value))
                {
                    Context.AddFailure("pagesize", $"Page size must in [{string.Join(",", pages)}]");
                }
            });
            RuleFor(s => s.sortby).Must(value => string.IsNullOrEmpty(value) || allowedsortby.Contains(value))
                .WithMessage($"must empty of [{string.Join(",", allowedsortby)}]");
        }
        
        



    }
}
