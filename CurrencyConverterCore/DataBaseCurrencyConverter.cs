using CurrencyConverterCore.Interfaces;

namespace CurrencyConverterCore
{
    /// <summary>
    /// Реализация интерфейса ICurrencyConverter, использующая базу данных для конвертации валют.
    /// </summary>
    public class DataBaseCurrencyConverter : ICurrencyConverter
    {
        private IDataOperations operations = new SqlDataOperations();

        /// <summary>
        /// Выводит список доступных валют на указанную дату.
        /// </summary>
        /// <param name="date">Дата для получения списка доступных валют.</param>
        public void AvailableCurrencies(string date)
        {
            operations.AvailableCurrenciesDataBase(date);
        }

        /// <summary>
        /// Выводит курс указанной валюты на указанную дату.
        /// </summary>
        /// <param name="charCode">Код валюты.</param>
        /// <param name="date">Дата для получения курса валюты.</param>
        public void CurrencyQuota(string charCode, string date)
        {
            operations.CurrencyQuotaDataBase(charCode, date);
        }

        /// <summary>
        /// Выполняет конвертацию указанного количества валюты из одной валюты в другую на указанную дату.
        /// </summary>
        /// <param name="amount">Количество валюты для конвертации.</param>
        /// <param name="fromCharCode">Код валюты, из которой выполняется конвертация.</param>
        /// <param name="toCharCode">Код валюты, в которую выполняется конвертация.</param>
        /// <param name="date">Дата для получения курса валюты.</param>
        public void CurrencyExchange(string amount, string fromCharCode, string toCharCode, string date)
        {
            operations.ExchangeCurrenciesDataBase(amount, fromCharCode, toCharCode, date);
        }
    }
}