using System;
using System.Collections.Generic;
using System.Text;

namespace CsharpAdvanced
{
    public class MailService
    {
        public void OnVideoEncoded(object source, EventArgs e)
        {
            Console.WriteLine("MailService: sending an email...");
        }
    }
}