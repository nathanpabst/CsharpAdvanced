using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Channels;

namespace CsharpAdvanced
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            UseExceptionHandling();
            //UseDynamic();
            //UseNullableTypes();
            //UseLinq();
            //UseExtensionMethods();
            //UseEvents();
            //UseLambdaExpression();
            //UseDelegates();
            //UseGenerics();
        }

        private static void UseExceptionHandling()
        {
            try
            {
                var api = new YoutubeApi();
                var videos = api.GetVideos("nate");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            //Global Exception Handler block...
            //try
            //{
            //    using (var streamReader = new StreamReader(@"c:\file.zip"))
            //    {
            //        var content = streamReader.ReadToEnd();
            //    }
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine("Something went wrong...");
            //}

            //try
            //{
            //    var calculator = new Calculator();
            //    var result = calculator.Divide(42, 0);
            //}
            //catch (DivideByZeroException ex)
            //{
            //    Console.WriteLine("don't divide by zero, dummy.");
            //}
            //catch (ArithmeticException ex)
            //{
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine("hmmm, something went wrong.");
            //}
        }

        private static void UseDynamic()
        {
            //Conversions & Casts...with dynamics you get implicit conversion from and to the target type
            //EX...
            //int i = 5;
            //dynamic d = i; // runtime type is integer, value: 5
            //dynamic a = 10; // type: dynamic int, value: 10
            //dynamic b = 5; // type of dynamic int, value: 5
            //var c = ""; // c is a string
            //var c = a + b; // c is now a dynamic with a value of 15
            //dynamic name = "Nate"; //during debug mode, the type of name will be 'string' and then change to an 'int' as execution continues
            //name = 42;
            //NOTE: it's very important to write unit tests when using dynamics to ensure the code is running as expected.

            //_______________NOTES_______________
            // Programming Language Types:
            // 1. statically-typed languages: C#, Java..type resolution at compile-time...offers early feedback (compile time)
            // 2. dynamically-typed languages: Ruby, Javascript, Python ...type resolution at run-time...offers ease of use and speedier to code
            // C# History: started as a static language. .NET 4 added the dynamic capability to improve interoperability with...
            // ...COM (eg writing office applications) & Dynamic languages (IronPython)
            // Without Dynamic, we have to use Reflection...a way to inspect the metadata of a type and access properties and methods
            //CLR: common language runtime... .NETs virtual machine that gets compile time code in intermediate language (IL) and converts
            //...the code into machine code at runtime
            //DLR: dynamic language runtime. DLR sits on top of CLR and gives dynamic language capability to C#
        }

        private static void UseNullableTypes()
        {
            //_______________EX 3. Using the ternary operator '?:'
            //This operator evaluates a boolean expression and returns the result of one of the two expressions,
            //...depending on whether the the expression evaluates to true or false
            //Explanation of the following code...if 'date' is not null, the GetValueOrDefault() code will execute and set the value to date3...
            //...if the date variable is null, date3 will be set to DateTime.Today
            DateTime? date = null;
            DateTime date3 = (date != null) ? date.GetValueOrDefault() : DateTime.Today;
            Console.WriteLine("Ternary operator: " + date3); //returns 10/1/2020 12:00:00
            Console.WriteLine("date: " + date.HasValue); //returns false

            //_______________EX 2. refactoring EX 1 code to use the Null Coalescing Operator: '??'
            //DateTime? date = null;
            // if date2 has a value, set it to the date variable. Otherwise, set date2 to DateTime.Today
            //DateTime date2 = date ?? DateTime.Today;
            //Console.WriteLine("Null Coalescing Operator: " +date2); //returns 10/1/2020 12:00:00 AM

            //________________EX. 1...using null values
            //DateTime? date = null;
            //DateTime date2;
            //if (date != null)
            //{
            //    date2 = date.GetValueOrDefault();
            //}
            //else
            //{
            //    date2 = DateTime.Today;
            //}
            //Console.WriteLine(date2); //returns today's date

            //___________NOTES________________
            //Value Types cannot be set to null...solved by using the nullable generic structure in the System namespace
            //1. Nullable<DateTime> date = null;
            //DateTime? date = null;
            ////Members of a nullable type: (type 'date.' to view the full list) GetValueOrDefault, HasValue, & Value are the most used
            //Console.WriteLine("GetValueOrDefault: " + date.GetValueOrDefault()); //returns the default 1/1/0001 12:00:00 AM. *Best practice
            //Console.WriteLine("HasValue: " + date.HasValue); //returns false
            //Console.WriteLine("Value: " + date.Value); //throws an InvalidOperationException
        }

        private static void UseLinq()
        {
            //LINQ Extension Methods: translates to a SQL query (instead of writing Stored Procedures)
            var books = new BookRepository().GetBooks();

            //var book = books.Single(b => b.Title == "Atlas Shrugged");
            //var book = books.First(b => b.Title == "Atlas Shrugged");
            //var book = books.FirstOrDefault(b => b.Title == "Atlas Shruged"); // will return a NullReferenceException...title does not exist (mis-spelled)
            //var book = books.Last(b => b.Title == "Atlas Shrugged");
            //var book = books.LastOrDefault(b => b.Title == "Atlas Shrugged");

            //Console.WriteLine(book.Title + " " + book.Price);

            //var pagedBooks = books.Skip(1).Take(2);
            //foreach (var pagedBook in pagedBooks)
            //{
            //    Console.WriteLine(pagedBook.Title); // will return ADO.NET Step by Step & Atlas Shrugged
            //}

            //var count = books.Count();
            //Console.WriteLine(count);

            var maxPrice = books.Max(b => b.Price); // returns the maximum value in a sequence
            var minPrice = books.Min(b => b.Price); // returns the minimum value in a sequence
            var inventorySum = books.Sum(b => b.Price); // returns the sum
            var inventoryAverage = books.Average(b => b.Price);

            Console.WriteLine("maximum price: " + maxPrice);
            Console.WriteLine("minimum price: " + minPrice);
            Console.WriteLine("inventory sum: " + inventorySum);
            Console.WriteLine("inventory average: " + inventoryAverage);
        }

        private static void UseExtensionMethods()
        {
            //using extension methods from the IEnumerable interface
            IEnumerable<int> numbers = new List<int>() { 1, 42, 21, 5 };
            var max = numbers.Max();
            var min = numbers.Min();
            var listCount = numbers.Count();
            var firstNum = numbers.ToArray().First();

            Console.WriteLine("max: " + max);
            Console.WriteLine("min: " + min);
            Console.WriteLine("count: " + listCount);
            Console.WriteLine("first number: " + firstNum);

            //string post = "super long, drawn out, and boring blog post...";
            //var shortenedPost = post.Shorten(5);
            //var charLength = post.Length;
            //var replaceChar = post.Replace("o", "O");

            //Console.WriteLine(shortenedPost);
            //Console.WriteLine(charLength);
            //Console.WriteLine(replaceChar);

            //___________NOTES______________
            // Extension Methods allow us to add methods to an existing class
            //...without changing its source code or
            //...creating a new class that inherits from it
        }

        private static void UseEvents()
        {
            var video = new Video() { Title = "video 1" };
            var videoEncoder = new VideoEncoder(); // publisher
            var mailService = new MailService(); // subscriber
            var messageService = new MessageService(); // subscriber

            //registering a handler (OnVideoEncoded) for the event. 'mailService.OnVideoEncoded' is a reference or pointer to the method
            videoEncoder.VideoEncoded += mailService.OnVideoEncoded;
            videoEncoder.VideoEncoded += messageService.OnVideoEncoded;

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
            //var books = new BookRepository().GetBooks();

            //var bargainBooks = books.FindAll(b => b.Price < 10);
            //foreach (var book in bargainBooks)
            //{
            //    Console.WriteLine(book.Title);
            //}

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
            //var number = new Nullable<int>(5);
            //Console.WriteLine("Has Value ?" + number.HasValue);
            //Console.WriteLine("Value: " + number.GetValueOrDefault());

            ////var book = new Booklist() { Isbn = "1111", Title = "C# Advanced" };

            //var numbers = new GenericList<int>();
            //numbers.Add(10);

            //var books = new GenericList<Book>();
            //books.Add(new Book());

            //var dictionary = new GenericDictionary<string, Book>();
            //dictionary.Add("12345", new Book());
        }
    }
}