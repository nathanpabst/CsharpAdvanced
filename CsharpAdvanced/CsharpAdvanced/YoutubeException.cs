using System;

namespace CsharpAdvanced
{
    public class YoutubeException : Exception
    {
        public YoutubeException(string message, Exception innerException)
        : base(message, innerException)
        {
        }
    }
}