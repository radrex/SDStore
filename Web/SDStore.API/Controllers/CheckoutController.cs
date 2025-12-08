namespace SDStore.API.Controllers
{
    using OrderCart;
    using Shared.DataTransferObjects.Response;
    using SDStore.Data.Shared.Enums;
    
    using Microsoft.AspNetCore.Mvc;
    
    [ApiController]
    [Route("api/checkout")]
    public class CheckoutController(ICartOperations cart) : ControllerBase
    {
        [HttpPost("place-order")]
        [ProducesResponseType<Response_OrderDetails>(statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PlaceOrder(CancellationToken ct)
        {
            cart.SetEmail("test-testov@gmail.com");
            cart.SetAddress("16 Pinecrest Road, Sampleville, WA 98");
            cart.SetPhoneNumber("+1 (555) 010-1115");
            cart.SetCurrency(Currency.EUR);

            cart.AddItem(id: 1, amount: 1);
            cart.Clear();
            
            cart.AddItem(id: 2, amount: 3);
            cart.AddItem(id: 2, amount: 8);
            cart.AddItem(id: 14, amount: 12.20M);
            cart.UpdateItemAmount(id: 14, amount: 5.15M);
            cart.RemoveItems(id: 2);
            
            cart.AddItem(id: 3, amount: 16);
            cart.AddItem(id: 12, amount: 30_000.00M);
            cart.AddItem(id: 9, amount: 1500.00M);

            var response = await cart.PlaceOrder(ct);
            return Ok(response);
        }
    }
}
