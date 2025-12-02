namespace ThreeTier {
	internal class Program {
		// The Presentation layer configures data layer settings
		// and then retrieves Business objects. 
		static void Main(string[] args) {
			Business.Configuration.ConnectionString = @"Server=(localdb)\mssqllocaldb;Database=BooksExample;ConnectRetryCount=0";
			
			foreach (var book in Business.Book.GetAllBooks()) {
				var author = book.GetAuthor();
				Console.WriteLine($"{book.Title} by {author.FirstName} {author.LastName}");

				Console.WriteLine($"\tAll books by {author.FirstName} {author.LastName}");
				foreach (var b2 in author.Books) {
					Console.WriteLine($"\t{b2.Title}");
				}
			}
		}
	}

	namespace Business {
		/// <summary>
		///  Configuration settings to be shared across Business tier classes.
		/// </summary>
		public static class Configuration {
			public static string ConnectionString { get; set; } = "";
		}
	}
}
