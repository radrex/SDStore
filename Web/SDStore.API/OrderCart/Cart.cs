using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SDStore.API.OrderCart
{
    using SDStore.Data.Shared.Enums;
    using Shared.DataTransferObjects.Request;
    using Shared.DataTransferObjects.Response;
    
    public class Cart : ICartOperations
    {
        private readonly HttpClient _httpClient;
        
        public Cart(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7178/api/");
            Items = new Dictionary<int, decimal>();
        }
        
        #region OrderMetadata
        
        public required string Email { get; set; }
        public required string Address { get; set; }
        public required string PhoneNumber { get; set; }
        public Currency Currency { get; set; }
        
        public void SetEmail(string email)
        {
            // TODO: Validations
            Email = email;
        }

        public void SetAddress(string address)
        {
            // TODO: Validations
            Address = address;
        }

        public void SetPhoneNumber(string phoneNumber)
        {
            // TODO: Validations
            PhoneNumber = phoneNumber;
        }

        public void SetCurrency(Currency currency)
        {
            // TODO: Validations
            Currency = currency;
        }
        
        #endregion

        #region CartOperations
        
        public Dictionary<int, decimal> Items { get; set; }
        
        public void AddItem(int id, decimal amount)
        {
            // TODO: validations and calculations
            if (Items.ContainsKey(id))
            {
                Items[id] += amount;
                return;
            }
    
            Items[id] = amount;
        }
    
        public void UpdateItemAmount(int id, decimal amount)
        {
            // TODO: validations and calculations
            if (!Items.ContainsKey(id))
            {
                return;
            }
    
            Items[id] = amount;
        }
    
        public void RemoveItems(int id)
        {
            Items.Remove(id);
        }
    
        public void Clear()
        {
            Items.Clear();
        }

        public async Task<Response_OrderDetails> PlaceOrder(CancellationToken cancellationToken)
        {
            // TODO: Validations
            if (Items.Count == 0)
            {
                throw new InvalidOperationException("Cart is empty. Add at least 1 item before submitting your order.");
            }
            
            var response = await _httpClient.PostAsJsonAsync(
                "orders",
                new Request_OrderDetails
                {
                    Email = Email,
                    Address = Address,
                    PhoneNumber = PhoneNumber,
                    Currency = this.Currency,
                    Items = Items.Select(item => new Request_OrderSelectedItems
                    {
                        Id = item.Key,
                        Amount = item.Value,
                    })
                },
                cancellationToken);
            
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Order failed, please try again.");
            }
            
            var rawJson = await response.Content.ReadAsStringAsync(cancellationToken);
            var orderDetails = JsonSerializer.Deserialize<Response_OrderDetails>(
                rawJson,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { new JsonStringEnumConverter() }
                });
            
            if (orderDetails is null)
            {
                throw new InvalidOperationException($"Order endpoint returned empty response.");
            }
            
            Clear();
            return orderDetails;
        }
        
        #endregion
    }
}
