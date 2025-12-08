namespace SDStore.Shared.DataTransferObjects.Request
{
    using SDStore.Data.Shared.Enums;
 
    using System.Text.Json.Serialization;
    
    public record Request_OrderDetails()
    {
        public required string Email { get; init; }
        public required string Address { get; init; }
        public required string PhoneNumber { get; init; }
        
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Currency Currency { get; init; }
        
        public IEnumerable<Request_OrderSelectedItems> Items { get; init; }
    }
}