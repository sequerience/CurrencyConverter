namespace CurrencyConverterCore.Interfaces
{
    public interface ICurrencyConverter
    {
        void AvailableCurrencies(string date);
        void CurrencyQuota(string quota, string date);
        void CurrencyExchange(string ammount, string fromCurrency, string toCurrency, string date);
        void ErrorMessage()
        {
            Console.WriteLine(new string('-', 30));

            Console.WriteLine(
            "Попробуйте писать команды так:\n" +
            "--currencies -> для вывода доступных валют\n" +
            "--quota USD --date 15/03/2021 -> для вывода курса валюты на конкретную дату\n" +
            "--exchange 10 --from USD --to EUR --date 15/03/2021 -> для обмена валюты");

            Console.WriteLine(new string('-', 30));
        }
    }
}