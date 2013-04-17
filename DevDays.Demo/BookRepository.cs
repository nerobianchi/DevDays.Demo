using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DevDays.Demo
{
	/// <summary>
	///    The book repository.
	/// </summary>
	public class BookRepository : IBookRepository
	{
		/// <summary>
		/// The list.
		/// </summary>
		/// <returns>
		/// The <see cref="List"/>.
		/// </returns>
		public List<Book> List()
		{
			SqlCommand sqlCommand = new SqlCommand
			{
				CommandText = "select * from Book",
				Connection = new SqlConnection
				{
					ConnectionString = @"server=.\sql2008;database=devdays;integrated security = SSPI"
				}
			};

			if (sqlCommand.Connection.State != ConnectionState.Open)
			{
				sqlCommand.Connection.Open();
			}

			SqlDataReader reader = sqlCommand.ExecuteReader();

			DataTable dataTable = new DataTable();
			dataTable.Load(reader);

			List<Book> list = null;
			if (dataTable.Rows.Count > 0)
			{
				list = new List<Book>();
			}

			if (list != null)
			{
				list.AddRange(from DataRow dataRow in dataTable.Rows
								  select new Book
												{
													Id = Convert.ToInt32(dataRow["Id"])
												});
			}

			return list;
		}
	}
}