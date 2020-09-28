using System;
using System.Collections.Generic;
using System.Text;

namespace CsharpAdvanced
{
    public class BookRepository
    {
        //LINQ Section...
        public IEnumerable<Book> GetBooks()
        {
            return new List<Book>
            {
                //object initializer syntax...
                new Book() {Title = "To Kill a Mockingbird", Price = 9 },
                new Book() {Title = "Atlas Shrugged", Price = 10 },
                new Book() {Title ="ADO.NET Step by Step", Price = 59 },
                new Book() {Title = "C# Advanced Topics", Price = 42.42f }
            };
        }

        //public List<Book> GetBooks()
        //{
        //    return new List<Book>()
        //    {
        //        new Book() {Title = "To Kill a Mockingbird", Price = 9},
        //        new Book() {Title = "Atlas Shrugged", Price = 10},
        //    };
        //}
    }
}