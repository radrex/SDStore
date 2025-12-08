namespace SDStore.Data.Shared.Structs
{
    using Enums;
    
    using System.Text.Json.Serialization;
    
    public readonly struct Money
    {
        [JsonConstructor]
        public Money(decimal amount, Currency currency)
        {
            Amount = amount;
            Currency = currency;
        }

        public decimal Amount { get; }
        public Currency Currency { get; }
    }
}