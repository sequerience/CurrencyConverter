using CurrencyConverterCore.Interfaces;
using System.Globalization;
using System.Text;

namespace CurrencyConverterCore
{
    /// <summary>
    /// Класс для построения URL-адресов для запросов курсов валют.
    /// </summary>
    public class UrlBuilder : IUrlBuilder
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса URLBuilder.
        /// </summary>
        public UrlBuilder()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        private string baseUrl = "http://www.cbr.ru/scripts/XML_daily.asp?date_req=";

        /// <summary>
        /// Возвращает текущую дату в формате "dd/MM/yyyy".
        /// </summary>
        /// <returns>Строка с текущей датой в формате "dd/MM/yyyy".</returns>
        public string GetTodayDate()
        {
            DateTime dateTime = DateTime.Today;

            string day = dateTime.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

            return day;
        }

        /// <summary>
        /// Возвращает URL-адрес для текущей даты.
        /// </summary>
        /// <returns>URL-адрес для текущей даты.</returns>
        public string GetUrl()
        {
            string requiredUrl = baseUrl + GetTodayDate();

            return requiredUrl;
        }

        /// <summary>
        /// Возвращает URL-адрес для указанной даты.
        /// </summary>
        /// <param name="date">Дата для построения URL-адреса.</param>
        /// <returns>URL-адрес для указанной даты.</returns>
        public string GetUrl(string date)
        {
            string requiredUrl = baseUrl + date;

            return requiredUrl;
        }


    }
}