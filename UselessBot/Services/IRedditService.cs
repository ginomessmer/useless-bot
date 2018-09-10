using RedditSharp.Things;
using System.Threading.Tasks;
using UselessBot.Data;

namespace UselessBot.Services
{
    public interface IRedditService
    {
        Task<Post> GetRandomSubmissionAsync(string subredditName, Subreddit.Sort sort = Subreddit.Sort.Top);
        Task<string> GetLatestHmmContentAsync();
        Task<Meme> GetRandomMemeAsync();
        Task<Meme> GetRandomTopMemeAsync();
    }
}