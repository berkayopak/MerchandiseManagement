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
public class ProductController : ControllerBase
{
    private readonly IProductFacade _productFacade;

    public ProductController(IProductFacade productFacade) =>
        _productFacade = productFacade;

    [HttpGet("search")]
    public async Task<ActionResult<OperationResult<IEnumerable<ProductResponse>>>> Search([FromQuery] SearchProductRequest request)
    {
        var validationResult = request.Validate();
        if (!validationResult.IsValid)
            return BadRequest(ApiConstants.BadRequestResult(nameof(request), validationResult));

        var product = await _productFacade.Search(request.Keyword,
            (request.MinStockQuantity ?? 0, request.MaxStockQuantity ?? 0));
        var productResponses = product.Select(p => new ProductResponse(p)).ToList();

        return Ok(new OperationResult<IEnumerable<ProductResponse>>(productResponses, true, null, validationResult,
            StatusCodes.Status200OK));
    }

    [HttpGet("")]
    public async Task<ActionResult<OperationResult<ProductResponse?>>> Get([FromQuery] GetProductRequest request)
    {
        var validationResult = request.Validate();
        if (!validationResult.IsValid)
            return BadRequest(ApiConstants.BadRequestResult(nameof(request), validationResult));

        var product = await _productFacade.Find(request.Id ?? 0);
        var productResponse = product != null
            ? new ProductResponse(product)
            : null;

        return Ok(new OperationResult<ProductResponse?>(productResponse, true, null, validationResult,
            StatusCodes.Status200OK));
    }

    [HttpPost("")]
    public async Task<ActionResult<OperationResult<ProductResponse>>> Add(AddProductRequest request)
    {
        var validationResult = request.Validate();
        if (!validationResult.IsValid)
            return BadRequest(ApiConstants.BadRequestResult(nameof(request), validationResult));

        var product = await _productFacade.Add(request.ToProduct());
        var productResponse = new ProductResponse(product);

        return Ok(new OperationResult<ProductResponse>(productResponse, true, null, validationResult,
            StatusCodes.Status200OK));
    }

    [HttpPut("")]
    public async Task<ActionResult<OperationResult<bool>>> Update(UpdateProductRequest request)
    {
        var validationResult = request.Validate();
        if (!validationResult.IsValid)
            return BadRequest(ApiConstants.BadRequestResult(nameof(request), validationResult));

        var updateSuccess = await _productFacade.Update(request.ToProduct());

        return Ok(new OperationResult<bool>(updateSuccess, true, null, validationResult,
            StatusCodes.Status200OK));
    }

    [HttpDelete("")]
    public async Task<ActionResult<OperationResult<bool>>> Delete([FromQuery] DeleteProductRequest request)
    {
        var validationResult = request.Validate();
        if (!validationResult.IsValid)
            return BadRequest(ApiConstants.BadRequestResult(nameof(request), validationResult));

        var deleteSuccess = await _productFacade.Delete(request.Id ?? 0);

        return Ok(new OperationResult<bool>(deleteSuccess, true, null, validationResult,
            StatusCodes.Status200OK));
    }
}