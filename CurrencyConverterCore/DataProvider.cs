using CurrencyConverterCore.Interfaces;

namespace CurrencyConverterCore
{
    /// <summary>
    /// Класс, предоставляющий провайдер данных для конвертера валют.
    /// </summary>
    public class DataProvider : IDataProvider
    {
        /// <summary>
        /// Возвращает экземпляр объекта, реализующего интерфейс ICurrencyConverter.
        /// </summary>
        /// <returns>Экземпляр объекта, реализующего интерфейс ICurrencyConverter.</returns>
        public ICurrencyConverter CurrencyConverter()
        {
            var checker = new NetworkStatusChecker();
            bool connectToNetwork = checker.isConnectedToNetwork();

            return connectToNetwork ? new ApiCurrencyConverter() : new DataBaseCurrencyConverter();
        }
    }
}
