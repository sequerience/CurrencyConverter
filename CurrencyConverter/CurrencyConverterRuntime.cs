using CurrencyConverterCore.Interfaces;
using CurrencyConverterCore;
using CommandLine;

namespace CurrencyConverter
{
    static class CurrencyConverterRuntime
    {
        static void Main(string[] args)
        {
            IUrlBuilder builder = new UrlBuilder();

            IDataProvider provider = new DataProvider();

            bool isAppRunning = true;

            string? input;

            while (isAppRunning)
            {
                input = Console.ReadLine();

                ICurrencyConverter converter = provider.CurrencyConverter();
                //ICurrencyConverter converter = new DataBaseCurrencyConverter();

                Parser.Default.ParseArguments<UserCommandOptions>(input?.Split(' '))
                    .WithParsed(options =>
                    {
                        if (options.allCurrencies)
                        {
                            converter.AvailableCurrencies(builder.GetTodayDate());
                        }

                        if (options.allCurrencies && options.date != null)
                        {
                            converter.AvailableCurrencies(options.date);
                        }

                        if (options.charCode != null && options.date != null)
                        {
                            converter.CurrencyQuota(options.charCode, options.date);
                        }

                        if (options.amount != null && options.fromCharCode != null &&
                            options.toCharCode != null && options.date != null)
                        {
                            converter.CurrencyExchange(options.amount, options.fromCharCode, options.toCharCode, options.date);
                        }

                        if (options.exit)
                        {
                            isAppRunning = false;
                        }
                    }).WithNotParsed(errors =>
                    {
                        converter.ErrorMessage();
                    });
            }
        }
    }
}