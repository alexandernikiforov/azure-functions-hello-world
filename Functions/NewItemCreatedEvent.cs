using Newtonsoft.Json;

namespace Functions
{
    public class NewItemCreatedEvent
    {
        [JsonProperty(PropertyName = "name")] public string ItemName;
    }
}