﻿using System;

namespace CsharpAdvanced
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            UseEvents();
            //UseLambdaExpression();
            //UseDelegates();
            //UseGenerics();
        }

        private static void UseEvents()
        {
            var video = new Video() { Title = "video 1" };
            var videoEncoder = new VideoEncoder(); //publisher
            var mailService = new MailService(); //subscriber

            //registering a handler (OnVideoEncoded) for the event. 'mailService.OnVideoEncoded' is a reference or pointer to the method
            videoEncoder.VideoEncoded += mailService.OnVideoEncoded;

            videoEncoder.Encode(video);

            //_____________NOTES_____________
            //Events...
            //...a mechanism for communication between objects
            //...used in building Loosely Coupled Applications
            //...helps extending applications
            //Delegates...
            //...agreement/contract between Publisher and Subscriber
            //...determines the signature of the event handler method in Subscriber
        }

        private static void VideoEncoder_VideoEncoded(object source, EventArgs args)
        {
            throw new NotImplementedException();
        }

        //Predicate method...
        //private static bool IsLessThan10Dollars(Book book)
        //{
        //    return book.Price < 10;
        //}

        private static void UseLambdaExpression()
        {
            //EX.3 using a LE...
            var books = new BookRepository().GetBooks();

            var bargainBooks = books.FindAll(b => b.Price < 10);
            foreach (var book in bargainBooks)
            {
                Console.WriteLine(book.Title);
            }

            //EX.2 using the predicate method...
            //var books = new BookRepository().GetBooks();
            //var bargainBooks = books.FindAll(IsLessThan10Dollars);
            //foreach (var book in bargainBooks)
            //{
            //    Console.WriteLine(book.Title);
            //}
            //EX.1 creating a method that takes a number and multiplies it by a factor
            //const int factor = 5;

            //Func<int, int> multiplier = n => n * factor;

            //var result = multiplier(10);

            //Console.WriteLine(result); //returns 50 (10*5)

            //__________________NOTES________________
            //Lambda Expression (LE): An anonymous method. (no access modifier, no name, no return statement)
            //LE's are convenient and faster to write
            //LE's use delegates to point to methods
            //LE syntax: left side contains the arguments 'args', the lambda operator: '=>' is in the middle, the expression is on the right side
            //Syntax for a LE that doesn't contain arguments... ()=> ...
            //Syntax for a LE that contains a single argument... x => ...
            //Syntax for a LE that contains multiple arguments... (x,y,z) => ...

            //EX: doubling a number using a method...
            //static int Square(int number)
            //{
            //    return number * number;
            //}

            //Console.WriteLine(Square(5));
            //End of Method Ex.

            //EX: doubling a number using a LE...
            //Func<int, int> square = number => number * number;
            //Console.WriteLine(square(5));
        }

        private static void UseDelegates()
        {
            //creating an instance of PhotoProcessor...
            var processor = new PhotoProcessor();
            //creating an instance of PhotoFilters...
            var filters = new PhotoFilters();

            //delegate or 'handler' pointing to the ApplyBrightness method...
            //...refactored to use an out-of-the-box delegate
            Action<Photo> filterHandler = filters.ApplyBrightness;
            //adding additional filters...
            filterHandler += filters.ApplyContrast;

            processor.Process("photo.jpg", filterHandler);

            //_______________NOTES_______________________
            //Delegate: an object that knows how to call a method (or a group of methods)
            //&& a reference to a function
            //Why do we need delegates? For designing extensible (does not have to be recompiled for each deployment)
            //...and flexible applications/frameworks

            //Interfaces or Delegates?
            //Use a delegate when...
            //1. an eventing design pattern is used
            //2. the caller doesn't need to access other properties or methods on the object implementing the method
        }

        private static void UseGenerics()
        {
            var number = new Nullable<int>(5);
            Console.WriteLine("Has Value ?" + number.HasValue);
            Console.WriteLine("Value: " + number.GetValueOrDefault());

            //var book = new Booklist() { Isbn = "1111", Title = "C# Advanced" };

            var numbers = new GenericList<int>();
            numbers.Add(10);

            var books = new GenericList<Book>();
            books.Add(new Book());

            var dictionary = new GenericDictionary<string, Book>();
            dictionary.Add("12345", new Book());
        }
    }
}