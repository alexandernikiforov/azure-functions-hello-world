using Microsoft.Azure.EventHubs;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace Functions
{
    public static class EventHubTrigger
    {
        [FunctionName("EventHubTrigger")]
        public static void RunAsync(
            [EventHubTrigger("evh-alnitest-dev-001", Connection = "EVENT_HUB_CONNECTION_STRING")]
            EventData[] data,
            [ServiceBus("sbq-alnitest", Connection = "SERVICE_BUS_CONNECTION_STRING")]
            ICollector<string> queue,
            ILogger log
        )
        {
            log.LogInformation($"C# Event Hub trigger function processed a batched message of length: {data.Length}");

            foreach (var message in data)
            {
                queue.Add(message.Body.ToString());
            }
        }
    }
}