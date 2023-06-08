using CurrencyConverterCore.Interfaces;

namespace CurrencyConverterCore
{
    /// <summary>
    /// Класс для выполнения ежедневного потока данных API.
    /// </summary>
    public class StreamingApiRequest : IStreamingApiRequest
    {
        /// <summary>
        /// Флаг, указывающий на состояние работы приложения.
        /// </summary>
        public bool isAppRunning = true;

        /// <summary>
        /// Запускает ежедневный поток данных API.
        /// </summary>
        public void DailyThread()
        {
            while (isAppRunning)
            {
                DateTime now = DateTime.Now;

                if (now.Hour == 0 && now.Minute == 0 && now.Second == 0)
                {
                    IDataOperations dailyApiRequest = new SqlDataOperations();

                    dailyApiRequest.UpdateDataBase();
                }


                Thread.Sleep(1000);
            }
        }
    }
}
