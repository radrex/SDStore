namespace SDStore.Shared.DataTransferObjects.Response
{
    using SDStore.Data.Shared.Structs;
    using SDStore.Data.Shared.Enums;
    
    public record Response_Item
    {
        public int Id { get; init; }
        public required string Name { get; init; }
        public Money Price { get; init; }
        public PriceMode PriceMode { get; set; }
    }
}