using System;
using System.Collections.Generic;
using System.Text;

namespace UselessBot.Data
{
    public class Quote : UserGeneratedContent
    {
        public Quote(string content, string key, ulong userId, string userName) 
            : base(content, key, userId, userName, "quote")
        {

        }
    }
}
