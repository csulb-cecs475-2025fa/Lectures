using AdoDemo;
using System.Data.SqlClient;
using System.Diagnostics;

internal class Program {
	private static void Main(string[] args) {
		// Initialize the connection. 
		SqlConnection conn = new SqlConnection("data source=(LocalDb)\\MSSQLLocalDb;initial catalog=BooksExample;integrated security=True");
		conn.Open();

		// Create a SQL command.
		SqlCommand cmd = conn.CreateCommand();
		cmd.CommandText = "SELECT * FROM Book";

		// Execute the command.
		List<Book> books = [];
		SqlDataReader reader = cmd.ExecuteReader();

		// Process each of the rows in the result.
		while (reader.Read()) {
			// reader.Read() advances to the next row, and returns true if the row exists.

			Book b = new Book() {
				Id = (int)reader["Id"],
				Title = (string)reader["Title"],
				Publisher = (string)reader["Publisher"],
				AuthorId = (int)reader["AuthorId"]
			};
			books.Add(b);
		}
		// Always close a reader when we are done.
		reader.Close();

		foreach (Book b in books) { 
			SqlCommand authorCmd = conn.CreateCommand();
			authorCmd.CommandText = "SELECT * FROM Author WHERE Id = @AuthorId";
			authorCmd.Parameters.Add(new SqlParameter("@AuthorId", b.AuthorId));

			SqlDataReader authorReader = authorCmd.ExecuteReader();
			if (authorReader.Read()) {
				Author a = new Author() {
					Id = (int)authorReader["Id"],
					FirstName = (string)authorReader["FirstName"],
					LastName = (string)authorReader["LastName"]
				};
				// Print the book and author.
				Console.WriteLine($"{b.Title} by {a.FirstName} {a.LastName}");
			}
			authorReader.Close();
		}
		// Always close a connection when we are done.
		conn.Close();
	}

	/*
	 * FLAWS:
	 * Mixing SQL in with C# source code.
	 * What if an exception is thrown?
	 * Ugly architecture; no clear separation of concerns.
	 */
}