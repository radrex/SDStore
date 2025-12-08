namespace SDStore.API.Controllers
{
    using Services.Abstractions;
    using Shared.DataTransferObjects.Request;
    using Shared.DataTransferObjects.Response;
    
    using System.Net;
    using Microsoft.AspNetCore.Mvc;
    
    [ApiController]
    [Route("api/catalog")]
    public class CatalogController(ICatalogService catalog) : ControllerBase
    {
        // TODO: Switch repeated everywhere in actions, needs refactoring. Maybe create a OperationResultHandler or something.
        
        [HttpGet("{id:int}", Name = "ItemById")]
        [ProducesResponseType<Response_Item>(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetItem(int id, CancellationToken ct)
        {
            var result = await catalog.GetItem(id, ct);
            switch (result.StatusCode)
            {
                case HttpStatusCode.OK:
                    return Ok(result.Data);
                
                case HttpStatusCode.NotFound:
                    return NotFound(new
                    {
                        Message = "Item doesn't exist.",
                    });
                
                default:
                    return StatusCode(500);
            }
        }

        [HttpGet(Name = "GetItems")]
        [ProducesResponseType<IEnumerable<Response_Item>>(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetItems(CancellationToken ct)
        {
            var result = await catalog.GetItems(ct);
            switch (result.StatusCode)
            {
                case HttpStatusCode.OK:
                    return Ok(result.Data);
                
                case HttpStatusCode.NotFound:
                    return NotFound(new
                    {
                        Message = "No available items at the moment.",
                    });
                
                default:
                    return StatusCode(500);
            }
        }

        [HttpPost(Name = "CreateItem")]
        [ProducesResponseType<Response_Item>(statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType<Response_Item>(statusCode: StatusCodes.Status409Conflict)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateItem([FromBody] Request_ItemCreate item, CancellationToken ct)
        {
            var result = await catalog.CreateItem(item, ct);
            switch (result.StatusCode) 
            {
                case HttpStatusCode.Created:
                    return CreatedAtRoute("ItemById", new { id = result.Data!.Id }, result.Data);
                
                case HttpStatusCode.Conflict:
                    Response.Headers.Location = Url.RouteUrl("ItemById", new { id = result.Data!.Id }, Request.Scheme);
                    return Conflict(new
                    {
                        Message = "Item already exists.",
                        Item = result.Data,
                    });
                
                default:
                    return StatusCode(500);
            }
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType<Response_Item>(statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateItem(int id, [FromBody] Request_ItemUpdate item, CancellationToken ct)
        {
            var result = await catalog.UpdateItem(id, item, ct);
            switch (result.StatusCode) 
            {
                case HttpStatusCode.Created:
                    Response.Headers.Location = Url.RouteUrl("ItemById", new { id = result.Data!.Id }, Request.Scheme);
                    return CreatedAtRoute("ItemById", new { id = result.Data!.Id }, result.Data);
                
                case HttpStatusCode.NoContent:
                    Response.Headers.Location = Url.RouteUrl("ItemById", new { id = result.Data!.Id }, Request.Scheme);
                    return NoContent();
                
                default:
                    return StatusCode(500);
            }
        }
        
        // TODO: Add soft delete for items ?

        [HttpOptions]
        public IActionResult GetCatalogOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT");
            return Ok();
        }
    }
}