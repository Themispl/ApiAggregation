namespace ApiAggregator.Models
{
    public class PhoneModels
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ApiObjectData Data { get; set; }
    }

    public class ApiObjectData
    {
        public string Color { get; set; }
        public string Capacity { get; set; }
    }
}
