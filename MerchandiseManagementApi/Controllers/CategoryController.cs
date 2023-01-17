using MerchandiseManagementApi.Common;
using MerchandiseManagementApi.Constant;
using MerchandiseManagementApi.Dto.Request;
using MerchandiseManagementApi.Dto.Response;
using MerchandiseManagementApi.Facade;
using Microsoft.AspNetCore.Mvc;

namespace MerchandiseManagementApi.Controllers;

[ApiController]
[Route("[controller]")]
//Api version information is taken from here, even if we don't give it,
//it would get 1.0 by default, but I wanted to show its usage.
[ApiVersion("1.0")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryFacade _categoryFacade;

    public CategoryController(ICategoryFacade categoryFacade) =>
        _categoryFacade = categoryFacade;

    [HttpGet("")]
    public async Task<ActionResult<OperationResult<CategoryResponse?>>> Get([FromQuery] GetCategoryRequest request)
    {
        var validationResult = request.Validate();
        if (!validationResult.IsValid)
            return BadRequest(ApiConstants.BadRequestResult(nameof(request), validationResult));

        var category = await _categoryFacade.Find(request.Id ?? 0);
        var categoryResponse = category != null
            ? new CategoryResponse(category)
            : null;

        return Ok(new OperationResult<CategoryResponse?>(categoryResponse, true, null, validationResult,
            StatusCodes.Status200OK));
    }

    [HttpPost("")]
    public async Task<ActionResult<OperationResult<CategoryResponse>>> Add(AddCategoryRequest request)
    {
        var validationResult = request.Validate();
        if (!validationResult.IsValid)
            return BadRequest(ApiConstants.BadRequestResult(nameof(request), validationResult));

        var category = await _categoryFacade.Add(request.ToCategory());
        var categoryResponse = new CategoryResponse(category);

        return Ok(new OperationResult<CategoryResponse>(categoryResponse, true, null, validationResult,
            StatusCodes.Status200OK));
    }

    [HttpPut("")]
    public async Task<ActionResult<OperationResult<bool>>> Update(UpdateCategoryRequest request)
    {
        var validationResult = request.Validate();
        if (!validationResult.IsValid)
            return BadRequest(ApiConstants.BadRequestResult(nameof(request), validationResult));

        var updateSuccess = await _categoryFacade.Update(request.ToCategory());

        return Ok(new OperationResult<bool>(updateSuccess, true, null, validationResult,
            StatusCodes.Status200OK));
    }

    [HttpDelete("")]
    public async Task<ActionResult<OperationResult<bool>>> Delete([FromQuery] DeleteCategoryRequest request)
    {
        var validationResult = request.Validate();
        if (!validationResult.IsValid)
            return BadRequest(ApiConstants.BadRequestResult(nameof(request), validationResult));

        var deleteSuccess = await _categoryFacade.Delete(request.Id ?? 0);

        return Ok(new OperationResult<bool>(deleteSuccess, true, null, validationResult,
            StatusCodes.Status200OK));
    }
}