using Microsoft.Extensions.Configuration;
using RedditSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UselessBot.Common.Extensions;

namespace UselessBot.Services
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

        public async Task<string> GetLatestHmmContentAsync()
        {
            var subreddit = await reddit.GetSubredditAsync("/r/hmmm");
            var hmm = (await subreddit.GetTop().ToList()).Random();

            return hmm.Url.ToString();
        }

        public async Task<string> GetRandomMemeContentAsync()
        {
            var subredditCollection = configuration.GetSection("Modules").GetSection("Memes")
                .GetSection("Subreddits").Get<List<string>>();

            var randomSubredditName = subredditCollection.Random();
            var subreddit = await reddit.GetSubredditAsync($"/r/{randomSubredditName}");
            var post = (await subreddit.GetTop().ToList()).Random();

            return post.Url.ToString();
        }
    }
}
