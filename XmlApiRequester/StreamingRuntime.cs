using CurrencyConverterCore.Interfaces;
using CurrencyConverterCore;

namespace XmlApiRequester
{
    static class StreamingRuntime
    {
        static async Task Main(string[] args)
        {
            IStreamingApiRequest streaming = new StreamingApiRequest();
            Task dailyThreadTask = Task.Run(() => streaming.DailyThread());

            await dailyThreadTask;
        }
    }
}