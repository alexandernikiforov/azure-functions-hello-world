using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace Functions
{
    public static class EventHubTrigger
    {
        [FunctionName("EventHubTrigger")]
        [return: ServiceBus("sbq-alnitest", Connection = "SERVICE_BUS_CONNECTION_STRING")]
        public static string RunAsync(
            [EventHubTrigger("evh-alnitest-dev-001", Connection = "EVENT_HUB_CONNECTION_STRING")]
            string myEventHubMessage, ILogger log)
        {
            log.LogInformation($"C# Event Hub trigger function processed a message: {myEventHubMessage}");
            return myEventHubMessage;
        }
    }
}