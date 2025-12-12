using System.Text;
using SDStore.Data.Shared.Enums;
using SDStore.Shared;
using SimpleOOP.Data;

var sb = new StringBuilder();

var item1 = new Item("item1", 1.20M, PriceMode.PerItem, Currency.BGN);
var item2 = new Item("item2", 2.50M, PriceMode.PerKg, Currency.EUR);
var item3 = new Item("item3", 1200.00M, PriceMode.PerLiter, Currency.USD);

var order = new Order(Currency.BGN);

order.AddItem(item1, 3M);
order.AddItem(item2, 1.5M);
order.AddItem(item3, 1.2M);

sb.AppendLine("----- ORDER REVIEW -----");
sb.AppendLine($"Currency: {order.Currency}");

foreach (var orderItem in order.Items)
{
    sb.AppendLine($"{orderItem.Item.Name} x{orderItem.Amount}{orderItem.Item.PriceMode.GetDisplayName()} - {orderItem.Item.Price}{orderItem.Item.Currency.GetDisplayName()} (VAT {orderItem.Item.VAT_Percentage}%) | " +
                  $"SubTotal Gross: {orderItem.SubTotal_Gross}{order.Currency.GetDisplayName()} | " +
                  $"SubTotal VAT Excluded: {orderItem.SubTotal_VAT_Excluded}{order.Currency.GetDisplayName()} | " +
                  $"SubTotal Net: {orderItem.SubTotal_Net}{order.Currency.GetDisplayName()}");
}

sb.AppendLine();
sb.AppendLine($"Total Gross: {order.OrderTotal_Gross}{order.Currency.GetDisplayName()} | " +
              $"Total VAT Excluded: {order.OrderTotal_VatExcluded}{order.Currency.GetDisplayName()} | " +
              $"Total Net: {order.OrderTotal_Net}{order.Currency.GetDisplayName()}");

Console.WriteLine(sb.ToString());




