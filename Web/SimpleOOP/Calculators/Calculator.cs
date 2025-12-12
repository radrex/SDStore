namespace SimpleOOP.Calculators
{
    using SDStore.Data.Shared.Enums;
    using System.Text.RegularExpressions;
    
    /// <summary>
    /// Implementing this class purely to show inheritance and polymorphism.
    /// PerItemPriceCalculator, PerKgPriceCalculator and PerLiterPriceCalculator will inherit this class and
    /// override its abstract properties and methods. Also, will be able to use the protected methods inside the children.
    /// </summary>
    public abstract class Calculator
    {
        private static readonly IReadOnlyDictionary<Currency, decimal> _ratesToBGN = 
            new Dictionary<Currency, decimal>()
            {
                { Currency.BGN, 1M},
                { Currency.EUR, 1.95583M},
                { Currency.USD, 1.679M},
                { Currency.CAD, 1.214M},
                { Currency.GBP, 2.237M},
            };
        
        public abstract PriceMode PriceMode { get; }
        
        public abstract decimal CalculateSubTotal_Gross(decimal unitPrice, decimal amount, Currency from, Currency to);
        public abstract decimal CalculateSubTotal_VAT_Excluded(decimal subTotal_Gross, decimal vatPercentage);
        public abstract decimal CalculateSubTotal_Net(decimal subTotal_Gross, decimal subTotal_VAT_Excluded);
        
        protected void ValidateAmount(decimal amount)
        {
            if (amount <= 0 || !Regex.IsMatch(amount.ToString("0.00"), @"^[0-9]{1,16}(?:\.[0-9]{1,2})?$"))
            {
                throw new ArgumentException("Invalid decimal price - decimal(18,2)");
            }
        }

        protected void ValidatePrice(decimal price)
        {
            if (price <= 0 || !Regex.IsMatch(price.ToString("0.00"), @"^[0-9]{1,16}(?:\.[0-9]{1,2})?$"))
            {
                throw new ArgumentException("Invalid decimal price - decimal(18,2)");
            }
        }

        protected static decimal ConvertToExchangeRate(decimal price, Currency from, Currency to)
        {
            var convertedPrice = price * GetExchangeRate(from, to);
            return convertedPrice;
        }
        
        private static decimal GetExchangeRate(Currency from, Currency to)
        {
            if (from == to)
            {
                return 1M;
            }

            return _ratesToBGN[from] * (1M / _ratesToBGN[to]);
        }
    }
}