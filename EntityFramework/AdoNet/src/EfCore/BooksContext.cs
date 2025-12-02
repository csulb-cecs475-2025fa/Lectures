using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCore {
	public class BooksContext : DbContext {
		private readonly string mConnectionString;
		public BooksContext(string connectionString) {
			mConnectionString = connectionString;
		}

		public DbSet<Author> Author { get; set; }
		public DbSet<Book> Book { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
			optionsBuilder.UseSqlServer(mConnectionString);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder) {
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<Author>()
				.Navigation(author => author.Books).AutoInclude();

			modelBuilder.Entity<Book>()
				.Navigation(book => book.Author).AutoInclude();
		}
	}
}
