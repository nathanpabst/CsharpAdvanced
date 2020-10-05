using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace CsharpAdvanced
{
    public class YoutubeApi
    {
        public List<Video> GetVideos(string user)
        {
            try
            {
                // access youtube web service
                // read the data
                // create a list of video objects
                throw new Exception("Oops, something went wrong. :( ");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new YoutubeException("Could not fetch videos from youtube.", e);
            }

            return new List<Video>();
        }
    }
}