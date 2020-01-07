using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Search;

namespace Functions
{
    public class CreateSearchIndex
    {
        private readonly SearchServiceClient _searchServiceClient;

        public CreateSearchIndex(SearchServiceClient searchServiceClient)
        {
            _searchServiceClient = searchServiceClient;
        }

        [FunctionName("CreateSearchIndex")]
        public async Task<IActionResult> RunAsync(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "index")]
            HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var definition = new Index()
            {
                Name = "hotels",
                Fields = FieldBuilder.BuildForType<Hotel>()
            };

            var index = await _searchServiceClient.Indexes.CreateOrUpdateAsync(definition);

            log.LogInformation($"Index created or updated: {index.Name}");
            return new OkObjectResult($"Index {index.Name}");
        }
    }
}