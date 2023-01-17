using FluentValidation;
using FluentValidation.Results;

namespace MerchandiseManagementApi.Dto.Request;

public class GetProductRequest
{
    public int? Id { get; set; }
    public ValidationResult Validate() => new GetProductRequestValidator().Validate(this);
}

public class GetProductRequestValidator : AbstractValidator<GetProductRequest>
{
    public GetProductRequestValidator()
    {
        RuleFor(r => r.Id).NotEmpty().GreaterThan(0);
    }
}