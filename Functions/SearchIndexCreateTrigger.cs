using System;
using System.Threading.Tasks;
using Microsoft.Azure.Search;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Search;
using Index = Microsoft.Azure.Search.Models.Index;

namespace Functions
{
    public class SearchIndexCreateTrigger
    {
        private readonly SearchServiceClient _searchServiceClient;

        public SearchIndexCreateTrigger(SearchServiceClient searchServiceClient)
        {
            _searchServiceClient = searchServiceClient;
        }

        [FunctionName("SearchIndexCreateTrigger")]
        public async Task RunAsync([TimerTrigger("0 */1 * * * *")] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.UtcNow}");

            var definition = new Index()
            {
                Name = "hotels",
                Fields = FieldBuilder.BuildForType<Hotel>()
            };

            var index = await _searchServiceClient.Indexes.CreateOrUpdateAsync(definition);

            log.LogInformation($"Index created or updated: {index.Name}");
        }
    }
}