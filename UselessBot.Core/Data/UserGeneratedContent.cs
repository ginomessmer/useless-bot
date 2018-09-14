using Discord;
using System;
using System.Collections.Generic;
using System.Text;

namespace UselessBot.Core.Data
{
    public class UserGeneratedContent
    {
        public int ID { get; set; }
        public string Key { get; set; }

        public string Content { get; set; }

        public ulong UserId { get; set; }
        public string UserName { get; set; }

        public string Tag { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public UserGeneratedContent(string content, string key, ulong userId, string userName, string tag = "default")
        {
            this.Content = content;
            this.Key = key;
            this.UserId = userId;
            this.UserName = userName;
            this.Tag = tag;
        }
    }
}
