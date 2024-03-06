using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Base;

[ApiController]
[Route("api/[controller]")]
public class BaseApiController : ControllerBase
{
    protected ActionResult HandleResult<T>(T result)
    {
        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }
}