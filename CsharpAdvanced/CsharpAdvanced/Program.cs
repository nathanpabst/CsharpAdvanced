using System;

namespace CsharpAdvanced
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var book = new Booklist() { Isbn = "1111", Title = "C# Advanced" };

            var numbers = new GenericList<int>();
            numbers.Add(10);

            var books = new GenericList<Book>();
            books.Add(new Book());

            var dictionary = new GenericDictionary<string, Book>();
            dictionary.Add("12345", new Book());
        }
    }
}