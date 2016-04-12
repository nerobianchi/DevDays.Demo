#region licence

// <copyright file="List.cs" company="Ciceksepeti">
// </copyright>
// <summary>
// 	Project Name:	DevDays.Demo.Tests
// 	Created By: 	erdem.ozdemir
// 	Create Date:	09.03.2016 14:05
// 	Last Changed By:	erdem.ozdemir
// 	Last Change Date:	09.03.2016 14:42
// </summary>

#endregion licence

using FluentAssertions;
using Moq;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Ploeh.AutoFixture.Xunit;
using System;
using System.Collections.Generic;
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
							 new object[]
							 {
								 1, new List<Book>
									 {
										 new Book()
									 }
							 },
							 new object[]
							 {
								 2, new List<Book>
									 {
										 new Book(),
										 new Book()
									 }
							 }
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

		[Fact]
		public void when_there_is_no_book_then_return_an_information_about_non_existence2()
		{
			IBookRepository iBookRepository = Substitute.For<IBookRepository>();
			iBookRepository.List().Returns((List<Book>)null);

			//Mock<IBookRepository> mock = new Mock<IBookRepository>();
			//mock.Setup(m => m.List()).Returns((List<Book>)null);
			//IBookRepository iBookRepository = mock.Object;
			BookService sut = new BookService(iBookRepository);

			Assert.Throws<ThereIsNoBookException>(() => sut.List());

			Action action = () => sut.List();
			action.ShouldThrow<ThereIsNoBookException>().And.Message.Should().Be("there is no");
		}

		[Theory]
		[AutoData]
		public void IntroductoryTest(int expectedNumber, MyClass sut)
		{
			int result = sut.Echo(expectedNumber);
			Assert.Equal(expectedNumber, result);
		}
	}

	public class MyClass
	{
		public int Echo(int expectedNumber)
		{
			return expectedNumber;
		}
	}
}