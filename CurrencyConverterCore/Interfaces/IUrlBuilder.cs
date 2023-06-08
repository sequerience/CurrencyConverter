namespace CurrencyConverterCore.Interfaces
{
    public interface IUrlBuilder
    {
        string GetTodayDate();
        string GetUrl();
        string GetUrl(string date);
    }
}
