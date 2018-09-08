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

        public RedditService(Reddit reddit)
        {
            this.reddit = reddit;
        }

        public async Task<string> GetLatestHmmContentAsync()
        {
            var subreddit = await reddit.GetSubredditAsync("/r/hmmm");
            var hmm = (await subreddit.GetTop().ToList()).Random();

            return hmm.Url.ToString();
        }
    }
}
