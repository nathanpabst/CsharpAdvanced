using System;
using System.Collections.Generic;
using System.Text;

namespace CsharpAdvanced
{
    public class BookRepository
    {
        public List<Book> GetBooks()
        {
            return new List<Book>()
            {
                new Book() {Title = "To Kill a Mockingbird", Price = 9},
                new Book() {Title = "Atlas Shrugged", Price = 10},
            };
        }
    }
}