using Microsoft.Azure.EventGrid;
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.Search;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Functions.Startup))]

namespace Functions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton((s) =>
            {
                var searchServiceName = System.Environment.GetEnvironmentVariable("SEARCH_SERVICE_NAME");
                var adminApiKey = System.Environment.GetEnvironmentVariable("SEARCH_SERVICE_ADMIN_API_KEY");
                return new SearchServiceClient(searchServiceName, new SearchCredentials(adminApiKey));
            });

            builder.Services.AddSingleton((provider =>
            {
                var eventGridAccessKey = System.Environment.GetEnvironmentVariable("EVENT_GRID_ACCESS_KEY");
                return new EventGridClient(new TopicCredentials(eventGridAccessKey));
            }));
        }
    }
}