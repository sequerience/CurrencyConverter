using CurrencyConverterCore.Models;

namespace CurrencyConverterCore.Interfaces
{
    public interface IDataAccess
    {
        void OpenConnection();
        void CloseConnection();
        bool CheckEmptyDataBase();
        public int GetRowCountFromSearch(string date);
        void InsertCurrency(string name, string charCode, string nominal, string value, string date);
        List<CurrencyModel> SelectAllCurrencies(string date);
        decimal SelectConcreteCurrency(string charCode, string date);
    }
}