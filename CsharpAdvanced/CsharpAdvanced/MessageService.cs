using System;
using System.Collections.Generic;
using System.Text;

namespace CsharpAdvanced
{
    public class MessageService
    {
        public void onVideoEncoded(object source, EventArgs args)
        {
            Console.WriteLine("MessageService: Sending a text message...");
        }
    }
}