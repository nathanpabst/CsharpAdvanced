using System;
using System.Collections.Generic;
using System.Text;

namespace CsharpAdvanced
{
    public class MailService
    {
        public void OnVideoEncoded(object source, VideoEventArgs e)
        {
            Console.WriteLine("MailService: sending an email..." + e.Video.Title);
        }
    }
}