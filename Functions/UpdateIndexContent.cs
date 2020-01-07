using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Spatial;
using Search;

namespace Functions
{
    public class UpdateIndexContent
    {
        private readonly SearchServiceClient _searchServiceClient;

        public UpdateIndexContent(SearchServiceClient searchServiceClient)
        {
            _searchServiceClient = searchServiceClient;
        }

        [FunctionName("UpdateIndexContent")]
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "update")]
            HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var actions =
                new IndexAction<Hotel>[]
                {
                    IndexAction.MergeOrUpload(
                        new Hotel()
                        {
                            HotelId = "1",
                            BaseRate = 199.0,
                            Description = "Best hotel in town",
                            DescriptionFr = "Meilleur hôtel en ville",
                            HotelName = "Fancy Stay",
                            Category = "Luxury",
                            Tags = new[] {"pool", "view", "wifi", "concierge"},
                            ParkingIncluded = false,
                            SmokingAllowed = false,
                            LastRenovationDate = new DateTimeOffset(2010, 6, 27, 0, 0, 0, TimeSpan.Zero),
                            Rating = 5,
                            Location = GeographyPoint.Create(47.678581, -122.131577)
                        }),
                    IndexAction.MergeOrUpload(
                        new Hotel()
                        {
                            HotelId = "2",
                            BaseRate = 79.99,
                            Description = "Cheapest hotel in town",
                            DescriptionFr = "Hôtel le moins cher en ville",
                            HotelName = "Roach Motel",
                            Category = "Budget",
                            Tags = new[] {"motel", "budget"},
                            ParkingIncluded = true,
                            SmokingAllowed = true,
                            LastRenovationDate = new DateTimeOffset(1982, 4, 28, 0, 0, 0, TimeSpan.Zero),
                            Rating = 1,
                            Location = GeographyPoint.Create(49.678581, -122.131577)
                        }),
                    IndexAction.MergeOrUpload(
                        new Hotel()
                        {
                            HotelId = "3",
                            BaseRate = 129.99,
                            Description = "Close to town hall and the river"
                        }),
                    IndexAction.Delete(new Hotel() {HotelId = "6"})
                };

            var batch = IndexBatch.New(actions);

            var indexClient = _searchServiceClient.Indexes.GetClient("hotels");

            indexClient.Documents.Index(batch);

            return new OkObjectResult("ok, updated");
        }
    }
}