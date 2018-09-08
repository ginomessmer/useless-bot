using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UselessBot.Common.Extensions
{
    public static class ListExtensions
    {
        public static Random random = new Random();
        public static T Random<T>(this IEnumerable<T> list)
        {
            return list.ToArray().Random();
        }

        public static T Random<T>(this T[] array)
        {
            random = new Random();
            return array[random.Next(0, array.Length - 1)];
        }
    }
}
