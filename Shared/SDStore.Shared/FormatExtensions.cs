namespace SDStore.Shared
{
    using SDStore.Data.Shared.Enums;
    
    public static class FormatExtensions
    {
        extension(decimal amount)
        {
            public string ToCurrency(Currency currency)
            {
                return currency switch
                {
                    Currency.BGN => $"{amount:N2}{currency.GetDisplayName()}",
                    Currency.EUR => $"{currency.GetDisplayName()}{amount:N2}",
                    Currency.USD => $"{currency.GetDisplayName()}{amount:N2}",
                    Currency.CAD => $"{currency.GetDisplayName()}{amount:N2}",
                    Currency.GBP => $"{currency.GetDisplayName()}{amount:N2}",
                    _ => $"{amount:C2}"
                };
            }

            public string ToPriceMode(PriceMode priceMode)
            {
                return $"{amount:N}{priceMode.GetDisplayName()}";
            }

            public string ToVATPercentage(decimal vatPercentage)
            {
                return $"{amount:N2} ({vatPercentage}%)";
            }
        }
    }
}