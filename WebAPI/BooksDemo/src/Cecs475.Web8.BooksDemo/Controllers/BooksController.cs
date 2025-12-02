using Cecs475.Web.BooksDemo.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace Cecs475.Web.BooksDemo.Controllers {
	[ApiController]
	[Route("[controller]")] // All routes through this controller will start
							// with the root of the controller class's name, 
							// e.g. "Books".
	public class BooksController : ControllerBase {
		private Dictionary<int, Book> mRepository = new Dictionary<int, Book>();

		// Create some fake starting data.
		public BooksController() {
			mRepository[1] = new Book() {
				Id = 1,
				Publisher = "Leanpub.com",
				Title = "C#eckmate: Learning C# and Programming Chess"
			};
			mRepository[2] = new Book() {
				Id = 2,
				Publisher = "CSULB Press",
				Title = "Some CSULB Book"
			};
		}

		// Retrieve all books.
		[HttpGet]
		public IEnumerable<Book> GetAllBooks() {
			return mRepository.Values;
		}

		// Retrieve a specific id. Books/{id}
		[HttpGet("{id}")]
		public Book? GetById(int id) {
			if (mRepository.ContainsKey(id)) {
				return mRepository[id];
			}
			return null;
		}

		// Retrieve all books with the given publisher. books/publisher={publisher}
		[HttpGet("publisher={publisher}")]
		public IEnumerable<Book> GetWithPublisher(string publisher) {
			return mRepository.Values.Where(b => b.Publisher == publisher);
		}

		// Create a new book.
		[HttpPost]
		public Book Post([FromBody]Book newBook) {
			mRepository[newBook.Id] = newBook;
			return mRepository[newBook.Id];
		}

		// Delete a book by ID.
		[HttpDelete]
		public bool Delete(int id) {
			mRepository.Remove(id);
			return mRepository.ContainsKey(id);
		}
	}
}
