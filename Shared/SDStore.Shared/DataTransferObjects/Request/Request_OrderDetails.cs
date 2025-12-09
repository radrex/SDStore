namespace SDStore.Shared.DataTransferObjects.Request
{
    using SDStore.Data.Shared.Enums;
    
    using System.ComponentModel.DataAnnotations;
    using System.Text.Json.Serialization;
    
    public record Request_OrderDetails()
    {
        [MaxLength(320)]
        [EmailAddress] // This attribute implementation is retarded. For example .@. is valid ¯\_(ツ)_/¯
        public required string Email { get; init; }
        
        [Required]
        [MaxLength(200)]
        public required string Address { get; init; }
        
        // Taken from: https://ihateregex.io/expr/phone/
        [Required]
        [RegularExpression(@"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$")]
        public required string PhoneNumber { get; init; }
        
        // [JsonConverter(typeof(JsonStringEnumConverter))]
        [Required]
        [EnumDataType(typeof(Currency),
            ErrorMessage = "Currency must be one of the following: BGN, EUR, USD, CAD, GBP")]
        public Currency Currency { get; init; }
        
        public IEnumerable<Request_OrderSelectedItems> Items { get; init; }
    }
}