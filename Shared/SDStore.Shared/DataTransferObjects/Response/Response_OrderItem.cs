namespace SDStore.Shared.DataTransferObjects.Response
{
    public record Response_OrderItem
    {
        public string Name { get; init; }
        public string Amount { get; init; }
        public string SubTotal_Gross { get; init; }
        public string VAT_Excluded { get; init; }
    }
}