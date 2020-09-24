using System;

namespace CsharpAdvanced
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var books = new BookRepository().GetBooks();
            var bargainBooks = books.FindAll(IsLessThan10Dollars);

            foreach (var book in bargainBooks)
            {
                Console.WriteLine(book.Title);
            }
            //UseLambdaExpression();
            //UseDelegates();
            //UseGenerics();
        }

        //Predicate method
        private static bool IsLessThan10Dollars(Book book)
        {
            return book.Price < 10;
        }

        private static void UseLambdaExpression()
        {
            //EX: creating a method that takes a number and multiplies it by a factor
            const int factor = 5;

            Func<int, int> multiplier = n => n * factor;

            var result = multiplier(10);

            Console.WriteLine(result); //returns 50 (10*5)

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