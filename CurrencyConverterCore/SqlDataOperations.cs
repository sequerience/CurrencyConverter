using System.Text;
using System.Xml.Linq;
using CurrencyConverterCore.Interfaces;
using CurrencyConverterCore.Models;

namespace CurrencyConverterCore
{

    /// <summary>
    /// Класс для выполнения операций с данными валют в базе данных.
    /// </summary>
    public class SqlDataOperations : IDataOperations
    {
        private IDataAccess dataAccess = new SqlDataAccess();

        private IUrlBuilder builder = new UrlBuilder();

        /// <summary>
        /// Инициализирует новый экземпляр класса SqlDataOperations.
        /// </summary>
        public SqlDataOperations()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        private decimal GetCurrencyValue(string charCode, string date)
        {
            if (charCode == "RUB")
            {
                return 1;
            }

            return Convert.ToDecimal(dataAccess.SelectConcreteCurrency(charCode.ToUpper(), date));
        }

        private bool GetRowCountFromSearch(string date)
        {
            int rowCountFromSearch = dataAccess.GetRowCountFromSearch(date);

            if (rowCountFromSearch != 0)
            {
                return true;
            }
            else
            {
                Console.WriteLine("Нет данных за указанную дату");

                return false;
            }
        }

        /// <summary>
        /// Обновляет базу данных с информацией о валютах на текущую дату.
        /// </summary>
        public void UpdateDataBase()
        {
            string date = builder.GetTodayDate();

            XDocument xdoc = XDocument.Load(builder.GetUrl(date));

            var currencies = xdoc.Descendants("Valute")
                .Select(currencyNode => new
                {
                    Name = currencyNode.Element("Name")?.Value,
                    CharCode = currencyNode.Element("CharCode")?.Value,
                    Nominal = currencyNode.Element("Nominal")?.Value,
                    Value = Convert.ToDecimal(currencyNode.Element("Value")?.Value).ToString(),
                    Date = date
                });
            foreach (var currency in currencies)
            {
                dataAccess.InsertCurrency(currency.Name, currency.CharCode, currency.Nominal, currency.Value, currency.Date);
            }
        }

        /// <summary>
        /// Проверяет наличие валют в базе данных для указанной даты и выводит их список.
        /// </summary>
        /// <param name="date">Дата для поиска валют.</param>
        public void AvailableCurrenciesDataBase(string date)
        {
            bool isFounded = GetRowCountFromSearch(date);

            if (isFounded)
            {
                List<CurrencyModel> currencies = dataAccess.SelectAllCurrencies(date);

                foreach (var currency in currencies)
                {
                    Console.WriteLine($"{currency.charCode} - {currency.name}");
                }
            }
        }

        /// <summary>
        /// Выводит курс указанной валюты на указанную дату.
        /// </summary>
        /// <param name="charCode">Код валюты.</param>
        /// <param name="date">Дата для поиска курса валюты.</param>
        public void CurrencyQuotaDataBase(string charCode, string date)
        {
            bool isFounded = GetRowCountFromSearch(date);

            if (isFounded)
            {
                decimal currencyValue = GetCurrencyValue(charCode.ToUpper(), date);

                Console.WriteLine($"Курс {charCode.ToUpper()}: {currencyValue} RUB");
            }
        }

        /// <summary>
        /// Выполняет обмен указанного количества валюты на другую валюту по указанной дате.
        /// </summary>
        /// <param name="amount">Количество валюты для обмена.</param>
        /// <param name="fromCharCode">Код валюты для обмена.</param>
        /// <param name="toCharCode">Код валюты, на которую происходит обмен.</param>
        /// <param name="date">Дата для поиска курса валют.</param>
        public void ExchangeCurrenciesDataBase(string amount, string fromCharCode, string toCharCode, string date)
        {
            bool isFounded = GetRowCountFromSearch(date);

            if (isFounded)
            {
                decimal fromCurrencyValue = GetCurrencyValue(fromCharCode.ToUpper(), date);
                decimal toCurrencyValue = GetCurrencyValue(toCharCode.ToUpper(), date);

                decimal amountOfCurrency = Convert.ToDecimal(amount);

                decimal result = fromCurrencyValue / toCurrencyValue * amountOfCurrency;

                Console.WriteLine($"{amount} по курсу {fromCharCode.ToUpper()}: {result} {toCharCode.ToUpper()}");
            }
        }
    }
}
