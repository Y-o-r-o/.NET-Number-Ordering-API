using API.Controllers.Base;
using BusinessLayer.DTOs;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[ApiExplorerSettings(GroupName = "NumberOrderingAPI_v1")]
[Route("[controller]")]
public sealed class NumberController : BaseApiController
{
    private readonly INumberService _numberService;

    public NumberController(INumberService numberService)
    {
        _numberService = numberService;
    }

    /// <summary>Create numbers.</summary>
    /// <param name="numbers">Numbers to create as a string.</param>
    /// <response code="200">Returns Ok response.</response>
    /// <response code="500">Returns error details.</response>
    /// <response code="400">Returns property error details.</response>
    [ProducesResponseType(typeof(HttpExceptionDTO), 500)]
    [ProducesResponseType(typeof(ValidationHttpExceptionDTO), 400)]
    [ProducesResponseType(typeof(string), 200)]
    [HttpPost]
    public async Task<IActionResult> CreateNumbersAsync(string numbers)
    {
        return HandleResult(await _numberService.CreateNumbersAsync(numbers));
    }

    /// <summary>Get latest saved numbers.</summary>
    /// <response code="200">Returns latest saved numbers as a string.</response>
    /// <response code="500">Returns error details.</response>
    [ProducesResponseType(typeof(HttpExceptionDTO), 500)]
    [ProducesResponseType(typeof(string), 200)]
    [HttpGet]
    public async Task<IActionResult> GetNumbersAsync()
    {
        return HandleResult(await _numberService.GetNumbersAsync());
    }
}