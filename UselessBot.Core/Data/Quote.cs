using System;
using System.Collections.Generic;
using System.Text;

namespace UselessBot.Core.Data
{
    public class Quote : UserGeneratedContent
    {
        public Quote(string content, string key, ulong userId, string userName) 
            : base(content, key, userId, userName, "quote")
        {

        }

        public string ToMessageString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("```");
            builder.Append(this.Content);
            builder.Append("```");
            builder.AppendLine($"- `{Key}`");

            return builder.ToString();
        }
    }
}
