namespace SDStore.Data.Shared.Structs
{
    using Enums;
    
    using System.ComponentModel.DataAnnotations;
    using System.Text.Json.Serialization;
    using System.ComponentModel.DataAnnotations.Schema;

    [method: JsonConstructor]
    public readonly struct Money(decimal amount, Currency currency)
    {
        [Column(TypeName = "decimal(18,2)")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Amount must have at most 2 decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Amount should be in 0 - 9999999999999999.99 range.")]
        public decimal Amount { get; init; } = amount;

        [Required]
        [EnumDataType(typeof(Currency),
            ErrorMessage = "Currency must be one of the following: BGN, EUR, USD, CAD, GBP")]
        public Currency Currency { get; init; } = currency;
    }
}