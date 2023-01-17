using FluentValidation;
using FluentValidation.Results;
using MerchandiseManagementApi.Domain;

namespace MerchandiseManagementApi.Dto.Request;

public class UpdateProductRequest
{
    public int? Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int? CategoryId { get; set; }
    public int? StockQuantity { get; set; }
    public Status? Status { get; set; }
    public ValidationResult Validate() => new UpdateProductRequestValidator().Validate(this);

    public Product ToProduct() =>
        new((int)Id!, DateTime.Now, DateTime.Now, true, Title!, Description!, (int)CategoryId!, null,
            (int)StockQuantity!, (Status)Status!);
}

public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
{
    public UpdateProductRequestValidator()
    {
        RuleFor(r => r.Id).NotEmpty().GreaterThan(0);
        RuleFor(r => r.Title).NotEmpty().MaximumLength(200);
        RuleFor(r => r.Description).NotEmpty();
        RuleFor(r => r.CategoryId).NotEmpty();
        RuleFor(r => r.StockQuantity).NotEmpty();
        RuleFor(r => r.StockQuantity).GreaterThan(0)
            .When(r => r.Status == Status.Live);
        RuleFor(r => r.Status).NotEmpty().IsInEnum();
    }
}