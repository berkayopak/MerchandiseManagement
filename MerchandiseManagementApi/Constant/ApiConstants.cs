using FluentValidation.Results;
using MerchandiseManagementApi.Common;

namespace MerchandiseManagementApi.Constant;

public static class ApiConstants
{
    public static OperationResult<object?> BadRequestResult(string requestName, ValidationResult validationResult) =>
        new(null, false, $"Invalid {requestName}", validationResult,
            StatusCodes.Status400BadRequest);
}