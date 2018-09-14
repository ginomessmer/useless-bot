using System;
using System.Collections.Generic;
using System.Text;

namespace UselessBot.Core.Data
{
    public class Gif : UserGeneratedContent
    {
        public Gif(string content, string key, ulong userId, string userName) 
            : base(content, key, userId, userName, tag: "gif")
        {

        }
    }
}
