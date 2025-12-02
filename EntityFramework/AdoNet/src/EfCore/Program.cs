using EfCore;
using Microsoft.EntityFrameworkCore;

internal class Program {
	private static void Main(string[] args) {
		BooksContext context = new(@"Server=(localdb)\mssqllocaldb;Database=BooksExample;ConnectRetryCount=0");

		foreach (var author in context.Author) {
			// Secretly, we just ran
			//
			// SELECT *
			// FROM Author
			// INNER JOIN Book ON Author.Id = Book.AuthorId
			//
			// and the results were parsed into Author objects each with a collection of Book objects.

			foreach (var book in author.Books) {
				Console.WriteLine($"{book.Title} by {book.Author.FirstName} {book.Author.LastName}");
			}
		}
		Console.WriteLine();
		var f451 = context.Book.Where(b => b.Title == "Fahrenheit 451").FirstOrDefault();
		// Runs the query
		//
		// SELECT *
		// FROM Book
		// WHERE Title = 'Fahrenheit 451'
		// FETCH FIRST 1 ROWS ONLY
		//
		// and returns null if there are no results, or a Book object from the 1 result.
		if (f451 is not null) {
			Console.WriteLine($"Fahrenheit 451 is by the author {f451.Author.FirstName} {f451.Author.LastName}");
		}

		Console.WriteLine();
		var titles = context.Book.Select(b => b.Title);
		// Runs the query
		// 
		// SELECT Title
		// FROM Book
		//
		// and parses the result as a sequence of strings.
		foreach (var title in titles) {
			Console.WriteLine($"There is a book titled {title}");
		}
	}
}