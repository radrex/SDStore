namespace SDStore.API.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [Route("api")]
    [ApiController]
    public class RootController(LinkGenerator linkGenerator) : ControllerBase
    {
        public IActionResult GetRoot([FromHeader(Name = "Accept")] string mediaType)
        {
            if (!mediaType.Contains("application/vnd.sdstore.apiroot"))
            {
                return NoContent();
            }

            // TODO: Implement link generation for possible endpoints
            var list = new List<string>();

            return Ok(list);
        }
    }
}