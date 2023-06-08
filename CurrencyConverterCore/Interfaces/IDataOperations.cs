namespace CurrencyConverterCore.Interfaces
{
    public interface IDataOperations
    {
        void UpdateDataBase();
        void AvailableCurrenciesDataBase(string date);
        void CurrencyQuotaDataBase(string charCode, string date);
        void ExchangeCurrenciesDataBase(string amount, string fromCharCode, string toCharCode, string date);
    }
}

