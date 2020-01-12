using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.EventGrid;
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace Functions
{
    public class GridWriterHttpTrigger
    {
        private static readonly string EventGridTopicEndpoint =
            Environment.GetEnvironmentVariable("EVENT_GRID_TOPIC_ENDPOINT");

        private static readonly string TopicHostname = new Uri(EventGridTopicEndpoint).Host;
        private readonly EventGridClient _eventGridClient;

        public GridWriterHttpTrigger(EventGridClient eventGridClient)
        {
            _eventGridClient = eventGridClient;
        }

        [FunctionName("GridWriterHttpTrigger")]
        public async Task<IActionResult> RunAsync(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "grid")]
            HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function (Grid Writer) processed a request.");

            var events = new List<EventGridEvent>
            {
                new EventGridEvent()
                {
                    Id = Guid.NewGuid().ToString(),
                    EventType = "MyFirstFunction.Items.NewItemCreated",
                    Data = new NewItemCreatedEvent() {ItemName = "Item 1"},
                    EventTime = DateTime.Now,
                    Subject = "Store A",
                    DataVersion = "3.7"
                }
            };

            await _eventGridClient.PublishEventsAsync(TopicHostname, events);
            Console.WriteLine("Events published to the Event Grid Topic");

            return (ActionResult) new OkObjectResult($"Event sent");
        }
    }
}