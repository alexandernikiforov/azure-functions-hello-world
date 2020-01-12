// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName=GridTrigger

using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Azure.EventHubs;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Extensions.Logging;

namespace Functions
{
    public class GridTrigger
    {
        private readonly EventHubClient _eventHubClient;

        public GridTrigger(EventHubClient eventHubClient)
        {
            _eventHubClient = eventHubClient;
        }

        [FunctionName("GridTrigger")]
        public async Task RunAsync([EventGridTrigger] EventGridEvent eventGridEvent, ILogger log)
        {
            log.LogInformation(eventGridEvent.Data.ToString());
            var data = eventGridEvent.Data.ToString();
            var time = eventGridEvent.EventTime.ToString(CultureInfo.InvariantCulture);

            for (var i = 0; i < 100; i++)
            {
                var message = $"{data} : {i} at {time}";
                await _eventHubClient.SendAsync(new EventData(Encoding.UTF8.GetBytes(message)));
            }
        }
    }
}