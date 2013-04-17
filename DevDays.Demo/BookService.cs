using System.Collections.Generic;

namespace DevDays.Demo
{
	public class BookService
	{
		private readonly IBookRepository iBookRepository;

		public BookService(IBookRepository iBookRepository)
		{
			this.iBookRepository = iBookRepository;
		}

		public List<Book> List()
		{
			List<Book> books = this.iBookRepository.List();

			if (books == null)
			{
				throw new ThereIsNoBookException();
			}

			return books;
		}
	}
}