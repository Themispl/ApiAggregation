namespace ApiAggregator.Models
{
    public class AggregatedResponse
    {
        public List<PhoneModels> Phones { get; set; } // For the Phones API
        public NewsResponse News { get; set; } // For the news API
        public List<Post> JSONPlaceholder { get; set; } // For the JSONPlaceholder API
    }
}
