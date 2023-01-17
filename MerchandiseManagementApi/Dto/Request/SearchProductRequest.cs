using FluentValidation;
using FluentValidation.Results;

namespace MerchandiseManagementApi.Dto.Request;

public class SearchProductRequest
{
    public string? Keyword { get; set; }
    public int? MinStockQuantity { get; set; }
    public int? MaxStockQuantity { get; set; }
    public ValidationResult Validate() => new SearchProductRequestValidator().Validate(this);
}

public class SearchProductRequestValidator : AbstractValidator<SearchProductRequest>
{
    public SearchProductRequestValidator()
    {
        RuleFor(r => r.Keyword).MaximumLength(200);
        RuleFor(r => r.MinStockQuantity).GreaterThanOrEqualTo(0)
            .When(r => r.MinStockQuantity != null);
        RuleFor(r => r.MaxStockQuantity).GreaterThanOrEqualTo(0)
            .When(r => r.MaxStockQuantity != null);
    }
}