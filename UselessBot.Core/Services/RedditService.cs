using Microsoft.Extensions.Configuration;
using RedditSharp;
using RedditSharp.Things;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UselessBot.Core.Extensions;
using UselessBot.Core.Data;
using static RedditSharp.Things.Subreddit;

namespace UselessBot.Core.Services
{
    public class RedditService : IRedditService
    {
        private readonly Reddit reddit;
        private readonly IConfigurationRoot configuration;

        public RedditService(Reddit reddit, IConfigurationRoot configuration)
        {
            this.reddit = reddit;
            this.configuration = configuration;
        }

        public async Task<Post> GetRandomSubmissionAsync(string subredditName, Subreddit.Sort sort = Subreddit.Sort.Top)
        {
            subredditName = $"/r/{subredditName}";
            var subreddit = await reddit.GetSubredditAsync(subredditName);
            var post = (await subreddit.GetPosts(sort).ToList()).Random();

            return post;
        }

        public async Task<string> GetLatestHmmContentAsync()
        {
            var subreddit = await reddit.GetSubredditAsync("/r/hmmm");
            var hmm = (await subreddit.GetPosts(Subreddit.Sort.New).ToList()).Random();

            return hmm.Url.ToString();
        }

        public async Task<Meme> GetRandomMemeAsync()
        {
            var subredditCollection = configuration.GetSection("Modules").GetSection("Memes")
                .GetSection("Subreddits").Get<List<string>>();

            var randomSubredditName = subredditCollection.Random();
            var subreddit = await reddit.GetSubredditAsync($"/r/{randomSubredditName}");
            var post = (await subreddit.GetPosts(Subreddit.Sort.New).ToList()).Random();

            return new Meme(post.Title, post.Url.ToString());
        }

        public async Task<Meme> GetRandomTopMemeAsync()
        {
            var subredditCollection = configuration.GetSection("Modules").GetSection("Memes")
                .GetSection("Subreddits").Get<List<string>>();

            var randomSubredditName = subredditCollection.Random();
            var subreddit = await reddit.GetSubredditAsync($"/r/{randomSubredditName}");
            var post = (await subreddit.GetTop().ToList()).Random();

            return new Meme(post.Title, post.Url.ToString());
        }
    }
}
