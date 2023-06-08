namespace CurrencyConverterCore.Models
{
    /// <summary>
    /// Модель валюты.
    /// </summary>
    public class CurrencyModel
    {
        public string name { get; }
        public string charCode { get; }
        public decimal value { get; }
        public decimal nominal { get; }
        public string date { get; }

        /// <summary>
        /// Конструктор класса CurrencyModel.
        /// </summary>
        /// <param name="name">Наименование валюты.</param>
        /// <param name="charCode">Код валюты.</param>
        /// <param name="value">Значение валюты.</param>
        /// <param name="nominal">Номинал валюты.</param>
        /// <param name="date">Дата.</param>
        public CurrencyModel(string name, string charCode, decimal value, decimal nominal, string date)
        {
            this.name = name;
            this.charCode = charCode;
            this.value = value;
            this.nominal = nominal;
            this.date = date;
        }
    }
}