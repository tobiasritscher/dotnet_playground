using System;
using Xunit;
using Moq;


namespace GradeBook.Tests
{
    public class BookTests
    {
        [Fact]
        public void BookCalculatesStatistics()
        {
            //arrange
            Mock<Book> mockBook = new Mock<Book>();
            mockBook.Setup(x => x.Name).Returns("");


            var book = new InMemoryBook(new List<double> {89.1, 90.5, 77.3});

            //act
            var results = book.GetStatistics();

            //assert
            Assert.Equal(85.6, results.Average, 1);
            Assert.Equal(90.5, results.High, 1);
            Assert.Equal(77.3, results.Low, 1);
            Assert.Equal(3, results.GradeCount);
            Assert.Equal('B', results.Letter);
        }
    }
}