using System;
using Xunit;


namespace GradeBook.Tests
{
    public class TypeTest
    {
        [Fact]
        public void Test1()
        {
            var book1 = GetBook("Book_1");
            
        }

        private Book GetBook(string name)
        {
            return new Book(name);
        }
    }
}