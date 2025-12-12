namespace SimpleOOP.Calculators
{
    using SDStore.Data.Shared.Enums;
    
    public sealed class PerItemPriceCalculator : Calculator
    {
        public override PriceMode PriceMode => PriceMode.PerItem;
        
        public override decimal CalculateSubTotal_Gross(decimal unitPrice, decimal amount, Currency from, Currency to)
        {
            ValidatePrice(unitPrice);
            ValidateAmount(amount);

            var convertedPrice = ConvertToExchangeRate(unitPrice, from, to);
            return Math.Round(convertedPrice * amount, 2, MidpointRounding.ToEven);
        }

        public override decimal CalculateSubTotal_VAT_Excluded(decimal subTotal_Gross, decimal vatPercentage)
        {
            return Math.Round(((subTotal_Gross / (1 + (vatPercentage / 100))) - subTotal_Gross) * -1, 2, MidpointRounding.ToEven);
        }

        public override decimal CalculateSubTotal_Net(decimal subTotal_Gross, decimal subTotal_VAT_Excluded)
        {
            return Math.Round(subTotal_Gross - subTotal_VAT_Excluded, 2, MidpointRounding.ToEven);
        }
    }
}