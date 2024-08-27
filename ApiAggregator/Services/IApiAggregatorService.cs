using ApiAggregator.Models;

namespace ApiAggregator.Services
{
    public interface IApiAggregatorService
    {
        Task<AggregatedResponse> GetAggregatedDataAsync();
    }
}
