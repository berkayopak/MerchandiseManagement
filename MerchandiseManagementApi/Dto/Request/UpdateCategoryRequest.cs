using FluentValidation;
using FluentValidation.Results;
using MerchandiseManagementApi.Domain;

namespace MerchandiseManagementApi.Dto.Request;

public class UpdateCategoryRequest
{
    public int? Id { get; set; }
    public string? Title { get; set; }
    public Status? Status { get; set; }
    public int? MinStockQuantity { get; set; }
    public ValidationResult Validate() => new UpdateCategoryRequestValidator().Validate(this);

    public Category ToCategory() =>
        new((int)Id!, DateTime.Now, DateTime.Now, true, Title!, (Status)Status!, (int)MinStockQuantity!);
}

public class UpdateCategoryRequestValidator : AbstractValidator<UpdateCategoryRequest>
{
    public UpdateCategoryRequestValidator()
    {
        RuleFor(r => r.Id).NotEmpty().GreaterThan(0);
        RuleFor(r => r.Title).NotEmpty().MaximumLength(200);
        RuleFor(r => r.Status).NotEmpty().IsInEnum();
        RuleFor(r => r.MinStockQuantity).NotEmpty().GreaterThanOrEqualTo(0);
    }
}