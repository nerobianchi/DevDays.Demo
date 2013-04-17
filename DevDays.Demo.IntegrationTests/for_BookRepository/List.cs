using System;

using FluentAssertions;

using NDbUnit.Core;
using NDbUnit.Core.SqlClient;

using Xunit;

namespace DevDays.Demo.IntegrationTests.for_BookRepository
{
	public class List : IDisposable
	{
		private INDbUnitTest iNDbUnitTest;

		[Fact]
		public void when_there_is_any_row_then_retur_them()
		{
			this.iNDbUnitTest = new SqlDbUnitTest(@"server=.\sql2008;database=devdays;integrated security = SSPI");
			this.iNDbUnitTest.ReadXmlSchema(@".\ListDataSet.xsd");
			this.iNDbUnitTest.ReadXml(@".\ListDataSet.xml");

			this.iNDbUnitTest.PerformDbOperation(DbOperationFlag.CleanInsertIdentity);

			BookRepository sut = new BookRepository();
			sut.List().Count.Should().Be(3);
		}

		public void Dispose()
		{
			this.iNDbUnitTest.PerformDbOperation(DbOperationFlag.DeleteAll);
		}
	}
}