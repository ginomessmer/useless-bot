using System;
using System.Collections.Generic;
using System.Text;

namespace UselessBot.Core.Data
{
    public class Meme
    {
        public string Title { get; set; }
        public string ImageUrl { get; set; }

        public Meme(string title, string imageUrl)
        {
            Title = title;
            ImageUrl = imageUrl;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(Title);
            builder.AppendLine(ImageUrl);

            return builder.ToString();
        }
    }
}
