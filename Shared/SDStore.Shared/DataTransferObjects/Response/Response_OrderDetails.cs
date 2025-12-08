namespace SDStore.Shared.DataTransferObjects.Response
{
    using SDStore.Data.Shared.Enums;
    
    public record Response_OrderDetails
    {
        public Guid Id { get; init; }
        public required string Email { get; init; }
        public required string Address { get; init; }
        public required string PhoneNumber { get; init; }
        public Currency Currency { get; init; }
        public required string  Total_Gross { get; init; }
        public required string Total_VAT_Excluded { get; init; }
        public required string Total_Net { get; init; }
        // TODO: Add TrackingNumber, OrderState (Processing, Delivered, Completed, etc.) ?
        
        public IEnumerable<Response_OrderItem> Items { get; init; }
    }
}