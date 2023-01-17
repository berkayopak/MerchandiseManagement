using FluentValidation;
using FluentValidation.Results;
using MerchandiseManagementApi.Domain;

namespace MerchandiseManagementApi.Dto.Request;

public class AddCategoryRequest
{
    public string? Title { get; set; }
    public Status? Status { get; set; }
    public int? MinStockQuantity { get; set; }
    public ValidationResult Validate() => new AddCategoryRequestValidator().Validate(this);

    public Category ToCategory() =>
        new(0, DateTime.Now, DateTime.Now, true, Title!, (Status)Status!, (int)MinStockQuantity!);
}

public class AddCategoryRequestValidator : AbstractValidator<AddCategoryRequest>
{
    public AddCategoryRequestValidator()
    {
        RuleFor(r => r.Title).NotEmpty().MaximumLength(200);
        RuleFor(r => r.Status).NotEmpty().IsInEnum();
        RuleFor(r => r.MinStockQuantity).NotEmpty().GreaterThanOrEqualTo(0);
    }
}