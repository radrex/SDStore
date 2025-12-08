// See https://aka.ms/new-console-template for more information

using System.Text;
using ConsoleCalc;
using SDStore.Data.Shared.Enums;

var calculator = new CalculatorUtils();
StringBuilder sb = new StringBuilder();

//----------------------------------------------------------------------
sb.AppendLine("todor.jivkov@prb.bg - pay in BGN - 1 Uaz (20% VAT), 2 Simson (20% VAT), 16 F-16 (15% VAT)");
sb.AppendLine("/--------------------------- OrderItem ---------------------------");

decimal uaz_1_subtotal_gross = calculator.CalculateTotalGross(10500.00M, 1.00M, Currency.BGN, Currency.BGN);
decimal uaz1_vat_excluded = calculator.Calculate_VAT_Excluded(uaz_1_subtotal_gross, 20.00M);
decimal uaz1_subtotal_net = calculator.Calculate_SubTotal_Net(uaz_1_subtotal_gross, uaz1_vat_excluded);
sb.AppendLine($"SubTotal_Gross = {uaz_1_subtotal_gross}M, VAT_Excluded = {uaz1_vat_excluded}M, SubTotal_Net = {uaz1_subtotal_net}M");

decimal simson_2_subtotal_gross = calculator.CalculateTotalGross(2999.00M, 2.00M, Currency.BGN, Currency.BGN);
decimal simson_2_vat_excluded = calculator.Calculate_VAT_Excluded(simson_2_subtotal_gross, 20.00M);
decimal simson_2_subtotal_net = calculator.Calculate_SubTotal_Net(simson_2_subtotal_gross, simson_2_vat_excluded);
sb.AppendLine($"SubTotal_Gross = {simson_2_subtotal_gross}M, VAT_Excluded = {simson_2_vat_excluded}M, SubTotal_Net = {simson_2_subtotal_net}M");

decimal f16_16_subtotal_gross = calculator.CalculateTotalGross(150000000.00M, 16.00M, Currency.USD, Currency.BGN);
decimal f16_16_vat_excluded = calculator.Calculate_VAT_Excluded(f16_16_subtotal_gross, 15.00M);
decimal f16_16_subtotal_net = calculator.Calculate_SubTotal_Net(f16_16_subtotal_gross, f16_16_vat_excluded);
sb.AppendLine($"SubTotal_Gross = {f16_16_subtotal_gross}M, VAT_Excluded = {f16_16_vat_excluded}M, SubTotal_Net = {f16_16_subtotal_net}M");

sb.AppendLine("/--------------------------- Order -------------------------------");
sb.AppendLine($"Total_Gross = {calculator.Calculate_Total(new List<decimal>{uaz_1_subtotal_gross, simson_2_subtotal_gross, f16_16_subtotal_gross})}M, " +
              $"Total_VAT_Excluded = {calculator.Calculate_Total(new List<decimal>{uaz1_vat_excluded, simson_2_vat_excluded, f16_16_vat_excluded})}M, " +
              $"Total_Net = {calculator.Calculate_Total(new List<decimal>{uaz1_subtotal_net, simson_2_subtotal_net, f16_16_subtotal_net})}M");
sb.AppendLine();

//----------------------------------------------------------------------
sb.AppendLine("vlad.putin@ussr.ru - pay in USD - Ricin 0.72l / 9%, Sarin 2.11l / 9.2%, Novichok Agent 0.21kg / 22% VAT");
sb.AppendLine("/--------------------------- OrderItem ---------------------------");

decimal ricin_0_72_subtotal_gross = calculator.CalculateTotalGross(419.30M, 0.72M, Currency.USD, Currency.USD);
decimal ricin_vat_excluded = calculator.Calculate_VAT_Excluded(ricin_0_72_subtotal_gross, 09.00M);
decimal ricin_subtotal_net = calculator.Calculate_SubTotal_Net(ricin_0_72_subtotal_gross, ricin_vat_excluded);
sb.AppendLine($"SubTotal_Gross = {ricin_0_72_subtotal_gross}M, VAT_Excluded = {ricin_vat_excluded}M, SubTotal_Net = {ricin_subtotal_net}M");

decimal sarin_2_11_subtotal_gross = calculator.CalculateTotalGross(761.50M, 2.11M, Currency.GBP, Currency.USD);
decimal sarin_vat_excluded = calculator.Calculate_VAT_Excluded(sarin_2_11_subtotal_gross, 09.20M);
decimal sarin_subtotal_net = calculator.Calculate_SubTotal_Net(sarin_2_11_subtotal_gross, sarin_vat_excluded);
sb.AppendLine($"SubTotal_Gross = {sarin_2_11_subtotal_gross}M, VAT_Excluded = {sarin_vat_excluded}M, SubTotal_Net = {sarin_subtotal_net}M");

decimal novichok_0_21_subtotal_gross = calculator.CalculateTotalGross(23500.50M, 0.21M, Currency.BGN, Currency.USD);
decimal novichok_vat_excluded = calculator.Calculate_VAT_Excluded(novichok_0_21_subtotal_gross, 22.00M);
decimal novichok_subtotal_net = calculator.Calculate_SubTotal_Net(novichok_0_21_subtotal_gross, novichok_vat_excluded);
sb.AppendLine($"SubTotal_Gross = {novichok_0_21_subtotal_gross}M, VAT_Excluded = {novichok_vat_excluded}M, SubTotal_Net = {novichok_subtotal_net}M");

sb.AppendLine("/--------------------------- Order -------------------------------");
sb.AppendLine($"Total_Gross = {calculator.Calculate_Total(new List<decimal>{ricin_0_72_subtotal_gross, sarin_2_11_subtotal_gross, novichok_0_21_subtotal_gross})}M, " +
              $"Total_VAT_Excluded = {calculator.Calculate_Total(new List<decimal>{ricin_vat_excluded, sarin_vat_excluded, novichok_vat_excluded})}M, " +
              $"Total_Net = {calculator.Calculate_Total(new List<decimal>{ricin_subtotal_net, sarin_subtotal_net, novichok_subtotal_net})}M");
sb.AppendLine();

//----------------------------------------------------------------------
sb.AppendLine("austrian-painter@thirdreich.de - pay in EUR - Zimmerit - 100000kg (5% VAT) / Mustard Gas - 30000l (6.2% VAT) / JP-8 Kerosene - 999999.99l (12.4% VAT)");
sb.AppendLine("/--------------------------- OrderItem ---------------------------");

decimal zimmerit_100000_subtotal_gross = calculator.CalculateTotalGross(250.50M , 100000.00M, Currency.EUR, Currency.EUR);
decimal zimmerit_vat_excluded = calculator.Calculate_VAT_Excluded(zimmerit_100000_subtotal_gross, 05.00M);
decimal zimmerit_subtotal_net = calculator.Calculate_SubTotal_Net(zimmerit_100000_subtotal_gross, zimmerit_vat_excluded);
sb.AppendLine($"SubTotal_Gross = {zimmerit_100000_subtotal_gross}M, VAT_Excluded = {zimmerit_vat_excluded}M, SubTotal_Net = {zimmerit_subtotal_net}M");

decimal mustardGas_30000_subtotal_gross = calculator.CalculateTotalGross(211.20M, 30000.00M, Currency.EUR, Currency.EUR);
decimal mustardGas_vat_excluded = calculator.Calculate_VAT_Excluded(mustardGas_30000_subtotal_gross, 06.20M);
decimal mustardGas_subtotal_net = calculator.Calculate_SubTotal_Net(mustardGas_30000_subtotal_gross, mustardGas_vat_excluded);
sb.AppendLine($"SubTotal_Gross = {mustardGas_30000_subtotal_gross}M, VAT_Excluded = {mustardGas_vat_excluded}M, SubTotal_Net = {mustardGas_subtotal_net}M");

decimal kerosene_999999_99_subtotal_gross = calculator.CalculateTotalGross(14308.83M, 999999.99M, Currency.USD, Currency.EUR);
decimal kerosene_vat_excluded = calculator.Calculate_VAT_Excluded(kerosene_999999_99_subtotal_gross, 06.20M);
decimal kerosene_subtotal_net = calculator.Calculate_SubTotal_Net(kerosene_999999_99_subtotal_gross, kerosene_vat_excluded);
sb.AppendLine($"SubTotal_Gross = {kerosene_999999_99_subtotal_gross}M, VAT_Excluded = {kerosene_vat_excluded}M, SubTotal_Net = {kerosene_subtotal_net}M");

sb.AppendLine("/--------------------------- Order -------------------------------");
sb.AppendLine($"Total_Gross = {calculator.Calculate_Total(new List<decimal>{zimmerit_100000_subtotal_gross, mustardGas_30000_subtotal_gross, kerosene_999999_99_subtotal_gross})}M, " +
              $"Total_VAT_Excluded = {calculator.Calculate_Total(new List<decimal>{zimmerit_vat_excluded, mustardGas_vat_excluded, kerosene_vat_excluded})}M, " +
              $"Total_Net = {calculator.Calculate_Total(new List<decimal>{zimmerit_subtotal_net, mustardGas_subtotal_net, kerosene_subtotal_net})}M");
sb.AppendLine();

//----------------------------------------------------------------------
sb.AppendLine("zelensky@example.ua - pay in GBP - Bayraktar TB2 - 15 (10% VAT) / AR-M1 Ammo - 100000kg (22% VAT) / JP-8 Kerosene - 10000l (12.4% VAT) / HFD-R - 30000l (15.2% VAT)");
sb.AppendLine("/--------------------------- OrderItem ---------------------------");

decimal bayraktar_15_subtotal_gross = calculator.CalculateTotalGross(7000000.00M, 15.00M, Currency.USD, Currency.GBP);
decimal bayraktar_vat_excluded = calculator.Calculate_VAT_Excluded(bayraktar_15_subtotal_gross, 10.00M);
decimal bayraktar_subtotal_net = calculator.Calculate_SubTotal_Net(bayraktar_15_subtotal_gross, bayraktar_vat_excluded);
sb.AppendLine($"SubTotal_Gross = {bayraktar_15_subtotal_gross}M, VAT_Excluded = {bayraktar_vat_excluded}M, SubTotal_Net = {bayraktar_subtotal_net}M");

decimal arm1_100000_subtotal_gross = calculator.CalculateTotalGross(1755.00M, 100000.00M, Currency.BGN, Currency.GBP);
decimal arm1_vat_excluded = calculator.Calculate_VAT_Excluded(arm1_100000_subtotal_gross, 22.00M);
decimal arm1_subtotal_net = calculator.Calculate_SubTotal_Net(arm1_100000_subtotal_gross, arm1_vat_excluded);
sb.AppendLine($"SubTotal_Gross = {arm1_100000_subtotal_gross}M, VAT_Excluded = {arm1_vat_excluded}M, SubTotal_Net = {arm1_subtotal_net}M");

decimal kerosene_10000_subtotal_gross = calculator.CalculateTotalGross(14308.83M, 10000.00M, Currency.USD, Currency.GBP);
decimal kerosene_10000_vat_excluded = calculator.Calculate_VAT_Excluded(kerosene_10000_subtotal_gross, 12.40M);
decimal kerosene_10000_subtotal_net = calculator.Calculate_SubTotal_Net(kerosene_10000_subtotal_gross, kerosene_10000_vat_excluded);
sb.AppendLine($"SubTotal_Gross = {kerosene_10000_subtotal_gross}M, VAT_Excluded = {kerosene_10000_vat_excluded}M, SubTotal_Net = {kerosene_10000_subtotal_net}M");

decimal hfd_30000_subtotal_gross = calculator.CalculateTotalGross(1442.10M, 30000, Currency.BGN, Currency.GBP);
decimal hfd_30000_vat_excluded = calculator.Calculate_VAT_Excluded(hfd_30000_subtotal_gross, 15.20M);
decimal hfd_30000_subtotal_net = calculator.Calculate_SubTotal_Net(hfd_30000_subtotal_gross, hfd_30000_vat_excluded);
sb.AppendLine($"SubTotal_Gross = {hfd_30000_subtotal_gross}M, VAT_Excluded = {hfd_30000_vat_excluded}M, SubTotal_Net = {hfd_30000_subtotal_net}M");

sb.AppendLine("/--------------------------- Order -------------------------------");
sb.AppendLine($"Total_Gross = {calculator.Calculate_Total(new List<decimal>{bayraktar_15_subtotal_gross, arm1_100000_subtotal_gross, kerosene_10000_subtotal_gross, hfd_30000_subtotal_gross})}M, " +
              $"Total_VAT_Excluded = {calculator.Calculate_Total(new List<decimal>{bayraktar_vat_excluded, arm1_vat_excluded, kerosene_10000_vat_excluded, hfd_30000_vat_excluded})}M, " +
              $"Total_Net = {calculator.Calculate_Total(new List<decimal>{bayraktar_subtotal_net, arm1_subtotal_net, kerosene_10000_subtotal_net, hfd_30000_subtotal_net})}M");
sb.AppendLine();

//----------------------------------------------------------------------
Console.WriteLine(sb);