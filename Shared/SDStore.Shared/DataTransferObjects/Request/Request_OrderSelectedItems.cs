namespace SDStore.Shared.DataTransferObjects.Request
{
    public record Request_OrderSelectedItems
    {
        public int Id { get; init; }
        public decimal Amount { get; init; }
    }
}