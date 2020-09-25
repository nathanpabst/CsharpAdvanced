﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;

namespace CsharpAdvanced
{
    //VideoEncoder class is the 'publisher' and/or 'event sender'
    //MailService and MessageService are the 'subscribers' and/or the 'event receivers'
    public class VideoEncoder
    {
        //refactoring to use .NETs version
        public event EventHandler<VideoEventArgs> VideoEncoded;

        public void Encode(Video video)
        {
            Console.WriteLine("encoding video...");
            //Thread.Sleep(3000) simulates an encoding process by delaying the application for 3 seconds
            Thread.Sleep(3000);

            OnVideoEncoded(video);
        }

        //the OnVideoEncoded method raises the event && notifies subscribers
        //...EX. typical implementation of a method in the subscriber class
        //...'this' is passing a reference to the VideoEncoder
        protected virtual void OnVideoEncoded(Video video)
        {
            VideoEncoded?.Invoke(this, new VideoEventArgs() { Video = video });
        }

        //_____________________NOTES__________________
        //Delegates...
        //...Agreement/Contract between the Publisher & Subscriber
        //...Determines the signature of the event handler method in the Subscriber class
        //Event Handler...the method called by the publisher when the event is raised.
        //...serves as a contract or delegate
        //HT notify subscribers of a published event...
        //1. define a delegate
        //2. define an event based on the delegate
        //3. raise the event
    }
}