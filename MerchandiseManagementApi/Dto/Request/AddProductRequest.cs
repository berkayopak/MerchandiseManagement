using FluentValidation;
using FluentValidation.Results;
using MerchandiseManagementApi.Domain;

namespace MerchandiseManagementApi.Dto.Request;

public class AddProductRequest
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int? CategoryId { get; set; }
    public int? StockQuantity { get; set; }
    public Status? Status { get; set; }
    public ValidationResult Validate() => new AddProductRequestValidator().Validate(this);

    public Product ToProduct() =>
        new(0, DateTime.Now, DateTime.Now, true, Title!, Description!, (int)CategoryId!, null, (int)StockQuantity!,
            (Status)Status!);
}

public class AddProductRequestValidator : AbstractValidator<AddProductRequest>
{
    public AddProductRequestValidator()
    {
        RuleFor(r => r.Title).NotEmpty().MaximumLength(200);
        RuleFor(r => r.Description).NotEmpty();
        RuleFor(r => r.CategoryId).NotEmpty();
        RuleFor(r => r.StockQuantity).NotEmpty();
        RuleFor(r => r.Status).NotEmpty().IsInEnum();
    }
}