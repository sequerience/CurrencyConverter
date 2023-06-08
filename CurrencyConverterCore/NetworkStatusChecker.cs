using System.Net.NetworkInformation;

namespace CurrencyConverterCore
{
    /// <summary>
    /// Класс для проверки доступности сети.
    /// </summary>
    public class NetworkStatusChecker
    {
        /// <summary>
        /// Проверяет доступность сети.
        /// </summary>
        /// <returns>Значение true, если есть подключение к сети; в противном случае - false.</returns>
        public bool isConnectedToNetwork()
        {
            try
            {
                Ping ping = new Ping();

                PingReply reply = ping.Send("google.com");

                return (reply.Status == IPStatus.Success);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
