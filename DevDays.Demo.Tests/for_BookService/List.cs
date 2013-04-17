using System;
using System.Collections.Generic;

using Moq;

using Xunit;
using Xunit.Extensions;

namespace DevDays.Demo.Tests.for_BookService
{
	public class List
	{
		[Theory]
		[PropertyData("GetList")]
		public void when_there_exists_any_books_then_return_them(int expected, List<Book> returnList)
		{
			Mock<IBookRepository> mock = new Mock<IBookRepository>();
			mock.Setup(m => m.List()).Returns(returnList);
			IBookRepository iBookRepository = mock.Object;
			BookService sut = new BookService(iBookRepository);
			List<Book> list = sut.List();

			int actual = list.Count;

			Assert.Equal(expected, actual);
		}

		public static object[] GetList
		{
			get
			{
				return new[]
					       {
								 new object []{1,new List<Book>{new Book()}},
					        new object []{2,new List<Book>{new Book(),new Book()}}
					       };
			}
		}

		[Fact]
		public void when_there_is_no_book_then_return_an_information_about_non_existence()
		{
			Mock<IBookRepository> mock = new Mock<IBookRepository>();
			mock.Setup(m => m.List()).Returns((List<Book>)null);
			IBookRepository iBookRepository = mock.Object;
			BookService sut = new BookService(iBookRepository);

			Assert.Throws<ThereIsNoBookException>(() => sut.List());
		}
	}
}