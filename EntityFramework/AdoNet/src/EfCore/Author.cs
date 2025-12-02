using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCore {
	public class Author {
		public required int Id { get; set; }
		public required string FirstName { get; set; }
		public required string LastName { get; set; }

		public ICollection<Book> Books { get; set; } = [];
	}
}
