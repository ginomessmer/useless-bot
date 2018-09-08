using System;

namespace UselessBot
{
    public class Program
    {
        public static void Main(string[] args) => new App().RunAsync().GetAwaiter().GetResult();
    }
}
