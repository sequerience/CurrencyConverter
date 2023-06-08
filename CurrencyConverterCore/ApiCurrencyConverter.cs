using System.Text;
using System.Xml.Linq;
using CurrencyConverterCore.Interfaces;

namespace CurrencyConverterCore
{
    /// <summary>
    /// Реализация интерфейса ICurrencyConverter, использующая API для конвертации валют.
    /// </summary>
    public class ApiCurrencyConverter : ICurrencyConverter
    {
        IUrlBuilder builder = new UrlBuilder();

        /// <summary>
        /// Конструктор класса ApiCurrencyConverter.
        /// </summary>
        public ApiCurrencyConverter()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        /// <summary>
        /// Выводит список доступных валют на указанную дату.
        /// </summary>
        /// <param name="date">Дата для получения списка доступных валют.</param>
        public void AvailableCurrencies(string date)
        {
            Console.WriteLine($"Список доступных валют на сегодня: {date}");
            Console.WriteLine(new string('-', 30));

            try
            {
                XDocument xmlDoc = XDocument.Load(builder.GetUrl(date));

                var currencyNodes = xmlDoc.Descendants("Valute");
                foreach (var currencyNode in currencyNodes)
                {
                    string? name = currencyNode.Element("Name")?.Value.ToString();
                    string? charCode = currencyNode.Element("CharCode")?.Value.ToString();

                    Console.WriteLine($"{charCode} - {name}");
                }
            }
            catch (Exception)
            {
                Console.WriteLine($"Нет подключения к интернету");
            }
        }

        private decimal currencyValue;

        /// <summary>
        /// Выводит курс указанной валюты на указанную дату.
        /// </summary>
        /// <param name="charCode">Код валюты.</param>
        /// <param name="date">Дата для получения курса валюты.</param>
        public void CurrencyQuota(string charCode, string date)
        {
            try
            {
                string currentCharCode = charCode.ToUpper();
                string currentDate = date;

                XDocument xmlDoc = XDocument.Load(builder.GetUrl(currentDate));

                var currencyNode = xmlDoc.Descendants("Valute")
                                    .FirstOrDefault(e => (string?)e
                                    .Element("CharCode") == currentCharCode);

                if (currencyNode != null)
                {
                    currencyValue =
                        Convert.ToDecimal(currencyNode.Element("Value")?.Value) /
                        Convert.ToDecimal(currencyNode.Element("Nominal")?.Value);

                    Console.WriteLine($"Курс {currentCharCode}: {currencyValue} RUB");
                }

                //Рубль в данном API имеет отношение к остальным валютам как 1 к 'значение валюты'
                if (currentCharCode == "RUB")
                {
                    var customNode = xmlDoc.Descendants("Valute")
                                    .FirstOrDefault(e => (string?)e
                                    .Element("CharCode") == "USD");

                    if (customNode != null)
                    {
                        currencyValue =
                            1 / Convert.ToDecimal(customNode.Element("Value")?.Value) /
                            Convert.ToDecimal(customNode.Element("Nominal")?.Value);
                        Console.WriteLine($"Курс {currentCharCode}: {currencyValue} USD\n" +
                            $"Чтобы посмотреть курс RUB к другой валюте воспользуйтесь --exchange");
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Нет подключения к интернету");
            }

        }

        private decimal currentCurrencyValue;
        private decimal requiredCurrencyValue;
        /// <summary>
        /// Выполняет конвертацию указанного количества валюты из одной валюты в другую на указанную дату.
        /// </summary>
        /// <param name="amount">Количество валюты для конвертации.</param>
        /// <param name="fromCharCode">Код валюты, из которой выполняется конвертация.</param>
        /// <param name="toCharCode">Код валюты, в которую выполняется конвертация.</param>
        /// <param name="date">Дата для получения курса валюты.</param>
        public void CurrencyExchange(string amount, string fromCharCode, string toCharCode, string date)
        {
            try
            {
                XDocument xdoc = XDocument.Load(builder.GetUrl(date));

                var currencies = xdoc.Descendants("Valute")
                    .Select(currencyNode => new
                    {
                        CharCode = currencyNode.Element("CharCode")?.Value,
                        Nominal = currencyNode.Element("Nominal")?.Value,
                        currencyNode.Element("Value")?.Value,
                    });

                foreach (var currency in currencies)
                {
                    if (currency.CharCode == fromCharCode)
                    {
                        currentCurrencyValue = Convert.ToDecimal(currency.Value);
                    }
                    if (currency.CharCode == toCharCode)
                    {
                        requiredCurrencyValue = Convert.ToDecimal(currency.Value);
                    }

                    //Рубль в данном API имеет отношение к остальным валютам как 1 к 'значение валюты'
                    if (fromCharCode == "RUB")
                    {
                        currentCurrencyValue = 1;
                    }
                    if (toCharCode == "RUB")
                    {
                        requiredCurrencyValue = 1;
                    }
                }
                Console.WriteLine($"{amount} {fromCharCode} = " +
                    $"{currentCurrencyValue / requiredCurrencyValue * Convert.ToDecimal(amount)} {toCharCode}");
            }
            catch (Exception)
            {
                Console.WriteLine("Нет подключения к интернету");
            }
        }
    }
}
