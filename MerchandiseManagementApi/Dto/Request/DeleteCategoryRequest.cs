using FluentValidation;
using FluentValidation.Results;

namespace MerchandiseManagementApi.Dto.Request;

public class DeleteCategoryRequest
{
    public int? Id { get; set; }
    public ValidationResult Validate() => new DeleteCategoryRequestValidator().Validate(this);
}

public class DeleteCategoryRequestValidator : AbstractValidator<DeleteCategoryRequest>
{
    public DeleteCategoryRequestValidator()
    {
        RuleFor(r => r.Id).NotEmpty().GreaterThan(0);
    }
}