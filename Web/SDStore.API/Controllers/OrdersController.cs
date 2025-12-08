namespace SDStore.API.Controllers
{
    using ModelBinders;
    using Services.Abstractions;
    using Shared.DataTransferObjects.Request;
    using Shared.DataTransferObjects.Response;
    
    using System.Net;
    using Microsoft.AspNetCore.Mvc;
    
    [ApiController]
    [Route("api/orders")]
    public class OrdersController(IOrdersService orders) : ControllerBase
    {
        [HttpGet("{id:guid}", Name = "OrderById")]
        [ProducesResponseType<Response_OrderDetails>(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOrder(Guid id, CancellationToken ct)
        {
            var result = await orders.GetOrder(id, ct);
            switch (result.StatusCode)
            {
                case HttpStatusCode.OK:
                    return Ok(result.Data);
                
                case HttpStatusCode.NotFound:
                    return NotFound(new
                    {
                        Message = "Order doesn't exist.",
                    });
                
                default:
                    return StatusCode(500);
            }
        }
        
        [HttpGet(Name = "GetOrders")]
        [ProducesResponseType<IEnumerable<Response_OrderDetails>>(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOrders(CancellationToken ct)
        {
            var result = await orders.GetOrders(ct);
            switch (result.StatusCode)
            {
                case HttpStatusCode.OK:
                    return Ok(result.Data);
                
                case HttpStatusCode.NotFound:
                    return NotFound(new
                    {
                        Message = "No available orders at the moment.",
                    });
                
                default:
                    return StatusCode(500);
            }
        }
        
        [HttpGet("{email:email}", Name = "OrderByEmail")]
        [ProducesResponseType<IEnumerable<Response_OrderDetails>>(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOrders(string email, CancellationToken ct)
        {
            var result = await orders.GetOrders(email, ct);
            switch (result.StatusCode)
            {
                case HttpStatusCode.OK:
                    return Ok(result.Data);
                
                case HttpStatusCode.NotFound:
                    return NotFound(new
                    {
                        Message = "Orders for this user doesn't exist",
                    });
                
                default:
                    return StatusCode(500);
            }
        }
        
        [HttpGet("({ids})", Name = "GetOrdersCollection")]
        [ProducesResponseType<IEnumerable<Response_OrderDetails>>(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOrders(
            [ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids,
            CancellationToken ct)
        {
            var result = await orders.GetOrders(ids, ct);
            switch (result.StatusCode)
            {
                case HttpStatusCode.OK:
                    return Ok(result.Data);
                
                case HttpStatusCode.NotFound:
                    return NotFound(new
                    {
                        Message = "No orders with such ids.",
                    });
                
                default:
                    return StatusCode(500);
            }
        }
        
        [HttpPost(Name = "PlaceOrder")]
        [ProducesResponseType<Response_OrderDetails>(statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PlaceOrder([FromBody] Request_OrderDetails request, CancellationToken ct)
        {
            var result = await orders.PlaceOrder(request, ct);
            switch (result.StatusCode)
            {
                case HttpStatusCode.Created:
                    return CreatedAtRoute("OrderById", new { id = result.Data!.Id }, result.Data);
                
                default:
                    return StatusCode(500);
            }
        }
    }
}