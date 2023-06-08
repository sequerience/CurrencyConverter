using System.Configuration;
using System.Data.SqlClient;
using System.Text;
using CurrencyConverterCore.Interfaces;
using CurrencyConverterCore.Models;

namespace CurrencyConverterCore
{
    /// <summary>
    /// Класс, предоставляющий доступ к данным через SQL-сервер.
    /// </summary>
    public class SqlDataAccess : IDataAccess
    {
        public SqlConnection sqlConnection;

        /// <summary>
        /// Конструктор класса SqlDataAccess. Устанавливает соединение с базой данных при создании экземпляра класса.
        /// </summary>
        public SqlDataAccess()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CurrenciesDataBase"].ConnectionString);
            sqlConnection.Open();
        }

        /// <summary>
        /// Открывает соединение с базой данных.
        /// </summary>
        public void OpenConnection()
        {
            try
            {
                sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CurrenciesDataBase"].ConnectionString);
                sqlConnection.Open();
            }
            catch (SqlException)
            {
                Console.WriteLine($"Ошибка подключения");
            }
        }

        /// <summary>
        /// Закрывает соединение с базой данных.
        /// </summary>
        public void CloseConnection()
        {
            try
            {
                sqlConnection?.Close();
            }
            catch (SqlException)
            {
                Console.WriteLine("Ошибка отсоединения от базы данных");
            }
        }

        /// <summary>
        /// Проверяет, является ли база данных пустой.
        /// </summary>
        /// <returns>True, если база данных пуста. False, если база данных содержит данные.</returns>
        public bool CheckEmptyDataBase()
        {
            try
            {
                string query = "SELECT COUNT(Name) FROM [Currencies]";

                using (SqlCommand command = new SqlCommand(query, sqlConnection))
                {
                    int result = (int)command.ExecuteScalar();

                    return result == 0;
                }
            }
            catch (SqlException)
            {
                Console.WriteLine("Ошибка базы данных при проверке заполненности базы");
            }
            return false;
        }

        /// <summary>
        /// Возвращает количество записей, найденных по заданной дате.
        /// </summary>
        /// <param name="date">Дата для поиска.</param>
        /// <returns>Количество записей, найденных по заданной дате.</returns>
        public int GetRowCountFromSearch(string date)
        {
            try
            {
                string query = "SELECT COUNT(*) FROM [Currencies] WHERE Date = @Date";

                using (SqlCommand command = new SqlCommand(query, sqlConnection))
                {
                    command.Parameters.AddWithValue("@Date", date);

                    int result = (int)command.ExecuteScalar();

                    return result;
                }
            }
            catch (SqlException)
            {
                Console.WriteLine("Ошибка базы данных при получении найденных колонок");

                return 0;
            }
        }

        /// <summary>
        /// Вставляет новую запись о валюте в базу данных.
        /// </summary>
        /// <param name="name">Наименование валюты.</param>
        /// <param name="charCode">Код валюты.</param>
        /// <param name="nominal">Номинал валюты.</param>
        /// <param name="value">Значение валюты.</param>
        /// <param name="date">Дата записи.</param>
        public void InsertCurrency(string name, string charCode, string nominal, string value, string date)
        {
            try
            {
                string query = "INSERT INTO [Currencies] (Name, CharCode, Nominal, Value, Date) VALUES (@Name, @CharCode, @Nominal, @Value, @Date)";

                using (SqlCommand command = new SqlCommand(query, sqlConnection))
                {
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@CharCode", charCode);
                    command.Parameters.AddWithValue("@Nominal", nominal);
                    command.Parameters.AddWithValue("@Value", value);
                    command.Parameters.AddWithValue("@Date", date);

                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException)
            {
                Console.WriteLine("Ошибка базы данных при внесении изменений");
            }
        }

        /// <summary>
        /// Обновляет запись о валюте в базе данных.
        /// </summary>
        /// <param name="name">Наименование валюты.</param>
        /// <param name="charCode">Код валюты.</param>
        /// <param name="nominal">Номинал валюты.</param>
        /// <param name="value">Значение валюты.</param>
        /// <param name="date">Дата записи.</param>
        public void UpdateCurrency(string name, string charCode, string nominal, string value, string date)
        {
            try
            {
                string query = "UPDATE [Currencies] SET Name = @Name, CharCode = @CharCode, Nominal = @Nominal, Value = @Value, Date = @Date";

                using (SqlCommand command = new SqlCommand(query, sqlConnection))
                {
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@CharCode", charCode.ToUpper());
                    command.Parameters.AddWithValue("@Nominal", nominal);
                    command.Parameters.AddWithValue("@Value", value);
                    command.Parameters.AddWithValue("@Date", date);

                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException)
            {
                Console.WriteLine("Ошибка базы данных при обновлении списка валют");
            }
        }

        /// <summary>
        /// Возвращает значение конкретной валюты по её коду и дате.
        /// </summary>
        /// <param name="charCode">Код валюты.</param>
        /// <param name="date">Дата записи.</param>
        /// <returns>Значение конкретной валюты.</returns>
        public decimal SelectConcreteCurrency(string charCode, string date)
        {
            try
            {
                string query = "SELECT * FROM [Currencies] WHERE Date LIKE '%' + @Date + '%' AND CharCode LIKE '%' + @CharCode + '%'";

                using (SqlCommand command = new SqlCommand(query, sqlConnection))
                {
                    command.Parameters.AddWithValue("@Date", date);
                    command.Parameters.AddWithValue("@CharCode", charCode.ToUpper());

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            decimal value = Convert.ToDecimal(reader["Value"]);
                            decimal nominal = Convert.ToDecimal(reader["Nominal"]);

                            decimal currencyValue = value / nominal;

                            return currencyValue;
                        }
                        reader?.Close();
                    }
                }
            }
            catch (SqlException)
            {
                Console.WriteLine("Ошибка базы данных при получении конкретного значения");
            }
            return 0;
        }

        /// <summary>
        /// Возвращает список всех валют, найденных по заданной дате.
        /// </summary>
        /// <param name="date">Дата записи.</param>
        /// <returns>Список моделей валют.</returns>
        public List<CurrencyModel> SelectAllCurrencies(string date)
        {
            List<CurrencyModel> currencies = new List<CurrencyModel>();

            try
            {
                string query = "SELECT * FROM [Currencies] WHERE Date LIKE '%' + @Date + '%'";

                using (SqlCommand command = new SqlCommand(query, sqlConnection))
                {
                    command.Parameters.AddWithValue("@Date", date);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CurrencyModel currency = new CurrencyModel(
                                reader["Name"].ToString(),
                                reader["CharCode"].ToString(),
                                Convert.ToDecimal(reader["Value"]),
                                Convert.ToDecimal(reader["Nominal"]),
                                reader["Date"].ToString());

                            currencies.Add(currency);
                        }
                        reader?.Close();

                        return currencies;
                    }
                }
            }
            catch (SqlException)
            {
                Console.WriteLine("Ошибка БД при получении листа значений");
            }
            return currencies;
        }
    }
}