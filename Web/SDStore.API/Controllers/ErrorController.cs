namespace SDStore.API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    
    [ApiController]
    [Route("error")]
    public class ErrorController : ControllerBase
    {
        public IActionResult HandleError()
        {
            return Problem();
        }
    }
}