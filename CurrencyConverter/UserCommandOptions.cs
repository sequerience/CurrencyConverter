using CommandLine;

namespace CurrencyConverter
{
    /// <summary>
    /// Класс для создания пользовательских команд
    /// </summary>
    public class UserCommandOptions
    {
        [Option("currencies", HelpText = "Получить список доступных валют")]
        public bool allCurrencies { get; set; }

        [Option("quota", HelpText = "Введите код валюты")]
        public string? charCode { get; set; }

        [Option("date", HelpText = "Введите дату")]
        public string? date { get; set; }

        [Option("exchange", HelpText = "Введите количество")]
        public string? amount { get; set; }

        [Option("from", HelpText = "Введите код текущей валюты")]
        public string? fromCharCode { get; set; }

        [Option("to", HelpText = "Введите код требуемой валюты")]
        public string? toCharCode { get; set; }

        [Option("exit", HelpText = "Выход из приложения")]
        public bool exit { get; set; }
    }
}