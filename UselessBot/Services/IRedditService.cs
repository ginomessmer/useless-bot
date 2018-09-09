using System.Threading.Tasks;
using UselessBot.Data;

namespace UselessBot.Services
{
    public interface IRedditService
    {
        Task<string> GetLatestHmmContentAsync();
        Task<Meme> GetRandomMemeAsync();
        Task<Meme> GetRandomTopMemeAsync();
    }
}