using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UselessBot.Core.Extensions
{
    public static class ListExtensions
    {
        public static T Random<T>(this IEnumerable<T> list)
        {
            return list.ToArray().Random();
        }

        public static T Random<T>(this T[] array)
        {
            return array.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
        }
    }
}
