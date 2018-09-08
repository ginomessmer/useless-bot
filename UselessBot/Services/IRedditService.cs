using System.Threading.Tasks;

namespace UselessBot.Services
{
    public interface IRedditService
    {
        Task<string> GetLatestHmmContentAsync();
    }
}