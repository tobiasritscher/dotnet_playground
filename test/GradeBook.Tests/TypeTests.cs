using System;
using Xunit;


namespace GradeBook.Tests
{
    public class TypeTest
    {
        [Fact]
        public void GetBookReturnsDifferentObkects()
        {
            var book1 = GetBook("Book_1");
            var book2 = GetBook("Book_2");

            Assert.Equal("Book_1", book1.GetName());
            Assert.Equal("Book_2", book2.GetName());
            Assert.NotSame(book1, book2);
        }

        [Fact]
        public void TwoVarsReferenceSameObject()
        {
            var book1 = GetBook("Book_1");
            var book2 = book1;
            book1.ChangeName("book_2");

            Assert.Same(book1, book2);
            Assert.True(Object.ReferenceEquals(book1, book2)); //same as Line 26
            Assert.Equal("book_2", book2.GetName());
        }

        [Fact]
        public void PassByReference()
        {
            var book1 = GetBook("Book_1");
            GetBookSetName(ref book1, "New Book"); //alternative is "out" instead of ref (some small differences)

            Assert.Equal("New Book", book1.GetName());
        }

        private void GetBookSetName(ref Book book, string name)
        {
            book = new Book(name);
        }

        private Book GetBook(string name)
        {
            return new Book(name);
        }
    }
}