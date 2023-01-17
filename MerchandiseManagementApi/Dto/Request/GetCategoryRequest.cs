using FluentValidation;
using FluentValidation.Results;

namespace MerchandiseManagementApi.Dto.Request;

public class GetCategoryRequest
{
    public int? Id { get; set; }
    public ValidationResult Validate() => new GetCategoryRequestValidator().Validate(this);
}

public class GetCategoryRequestValidator : AbstractValidator<GetCategoryRequest>
{
    public GetCategoryRequestValidator()
    {
        RuleFor(r => r.Id).NotEmpty().GreaterThan(0);
    }
}