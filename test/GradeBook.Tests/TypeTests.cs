using System;
using Xunit;


namespace GradeBook.Tests
{
    public delegate string WriteLogDelegate(string logMessage);

    public class TypeTest
    {
        [Fact]
        public void WriteLogDelegateCanPointToMethod()
        {
            WriteLogDelegate log = ReturnMessage;

            log += ReturnMessage;
            var result = log("Hello"); //ReturnMessage will be called twice

            Assert.Equal("Hello", result);
        }

        private string ReturnMessage(string message)
        {
            return message;
        }

        [Fact]
        public void GetBookReturnsDifferentObkects()
        {
            var book1 = GetBook("Book_1");
            var book2 = GetBook("Book_2");

            Assert.Equal("Book_1", book1.Name);
            Assert.Equal("Book_2", book2.Name);
            Assert.NotSame(book1, book2);
        }

        [Fact]
        public void TwoVarsReferenceSameObject()
        {
            var book1 = GetBook("Book_1");
            var book2 = book1;
            book1.Name = "book_2";

            Assert.Same(book1, book2);
            Assert.True(Object.ReferenceEquals(book1, book2)); //same as Line 26
            Assert.Equal("book_2", book2.Name);
        }

        [Fact]
        public void PassByReference()
        {
            var book1 = GetBook("Book_1");
            GetBookSetName(ref book1, "New Book"); //alternative is "out" instead of ref (some small differences)

            Assert.Equal("New Book", book1.Name);
        }

        private void GetBookSetName(ref InMemoryBook book, string name)
        {
            book = new InMemoryBook(name);
        }

        private InMemoryBook GetBook(string name)
        {
            return new InMemoryBook(name);
        }
    }
}