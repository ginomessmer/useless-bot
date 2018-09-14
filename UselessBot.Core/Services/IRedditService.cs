using RedditSharp.Things;
using System.Threading.Tasks;
using UselessBot.Core.Data;

namespace UselessBot.Core.Services
{
    public interface IRedditService
    {
        Task<Post> GetRandomSubmissionAsync(string subredditName, Subreddit.Sort sort = Subreddit.Sort.Top);
        Task<string> GetLatestHmmContentAsync();
        Task<Meme> GetRandomMemeAsync();
        Task<Meme> GetRandomTopMemeAsync();
    }
}