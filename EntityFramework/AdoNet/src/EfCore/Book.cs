using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCore {
	public class Book {
		public required int Id { get; init; }
		public required string Title { get; init; }
		public required string Publisher { get; init; }
		public required int AuthorId { get; init; }
		public required Author Author { get; init; }
	}
}
