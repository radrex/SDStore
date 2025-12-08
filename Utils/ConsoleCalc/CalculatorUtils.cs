namespace ConsoleCalc
{
    using SDStore.Data.Shared.Enums;
    
    public class CalculatorUtils
    {
        public decimal CalculateTotalGross(decimal price, decimal amount, Currency baseCurrency, Currency targetCurrency)
        {
            decimal convertedPrice = price * GetExchangeRate(baseCurrency, targetCurrency);
            return Math.Round(convertedPrice * amount, 2, MidpointRounding.ToEven);
        }
        
        public decimal Calculate_VAT_Excluded(decimal subTotal_Gross, decimal VAT_Percentage)
        {
            return Math.Round(((subTotal_Gross / (1 + (VAT_Percentage / 100))) - subTotal_Gross) * -1, 2, MidpointRounding.ToEven);;
        }

        public decimal Calculate_SubTotal_Net(decimal subTotal_Gross, decimal VAT_Excluded)
        {
            return Math.Round(subTotal_Gross - VAT_Excluded, 2, MidpointRounding.ToEven);
        }
        
        // SubTotal_Gross, Total_Gross, Total_VAT_Excluded, Total_Net
        public decimal Calculate_Total(List<decimal> total_List)
        {
            return Math.Round(total_List.Sum(x => x), 2, MidpointRounding.ToEven);
        }

        // TODO: Maybe we can find a free exchange rate api from the web and use it instead hardcoding this, but will do for now.
        /// <summary>
        /// Gives you the exchange rate. Using BGN as a base currency.
        /// </summary>
        private decimal GetExchangeRate(Currency from, Currency to)
        {
            var ratesToBGN = new Dictionary<Currency, decimal>
            {
                { Currency.BGN, 1M},
                { Currency.EUR, 1.95583M},
                { Currency.USD, 1.679M},
                { Currency.CAD, 1.214M},
                { Currency.GBP, 2.237M},
            };

            if (from == to)
            {
                return 1M;
            }

            return ratesToBGN[from] * (1M / ratesToBGN[to]);
        }
    }
}