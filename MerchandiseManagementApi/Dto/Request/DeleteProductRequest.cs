using FluentValidation;
using FluentValidation.Results;

namespace MerchandiseManagementApi.Dto.Request;

public class DeleteProductRequest
{
    public int? Id { get; set; }
    public ValidationResult Validate() => new DeleteProductRequestValidator().Validate(this);
}

public class DeleteProductRequestValidator : AbstractValidator<DeleteProductRequest>
{
    public DeleteProductRequestValidator()
    {
        RuleFor(r => r.Id).NotEmpty().GreaterThan(0);
    }
}