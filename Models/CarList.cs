using Newtonsoft.Json;

namespace Models
{
    public class CarList
    {
        [JsonProperty("cars")]
        public List<Car> Car { get; set; }
    }
}
