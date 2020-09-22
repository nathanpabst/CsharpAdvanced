using System;

namespace CsharpAdvanced
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            UseDelegates();
            //UseGenerics();
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