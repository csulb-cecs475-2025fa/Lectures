using Cecs475.Web8.Scheduling;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace Cecs475.Scheduling.Web.Controllers {
	// PROBLEM: EntityFramework objects cannot be serialized into Json. Their object relations don't 
	// play nice with a serializer. For example, a Student object has a list of references to ClassSections, each
	// of which have a list of references to Students in that section... a circular reference. How would we turn 
	// that into JSON?
	// So instead we create a Data Transfer Object class, and manually (ugh)
	// map entities to DTO instances.
	public class StudentDto {
		public int Id { get; set; }
		public required string FirstName { get; set; }
		public required string LastName { get; set; }

		// Serialization requires a default constructor and NO OTHERS, so we can't add a nice constructor
		// taking a Model.Student object. We'll make a static method instead.

		public static StudentDto From(Model.Student s) {
			return new StudentDto() {
				Id = s.Id,
				FirstName = s.FirstName,
				LastName = s.LastName
			};
		}

	}
	/// <summary>
	/// A controller for Student objects from the Entity Framework context.
	/// </summary>
	[ApiController]
	// The Route() attribute defines the URL path to access methods in this controller.
	[Route("api/students")]
	public class StudentsController : ControllerBase {
		// All our data comes from a database, which we will access with EntityFramework.
		private Data.CatalogContext mContext = new Data.CatalogContext(ApplicationSettings.ConnectionString);

		// This controller uses SYNCHRONOUS methods that BLOCK the controller while they execute, limiting
		// this controller's ability to handle more than one request "at a time". This is not the BEST way to do
		// things.


		// This method uses the GET verb on the controller's route, e.g., GET api/students
		[HttpGet]
		// IActionResult packages an HTTP response code with a response body. Our controller method
		// could return OK, or NotFound, or various other HTTP responses.

		// This method has no parameters. Combined with the GET verb, the implication is to return all
		// Student objects.
		public IActionResult GetStudents() {
			var allStudents = mContext.Students.Select(StudentDto.From); // map from Student to StudentDto.
			return Ok(allStudents);
		}

		// This method's route is api/students/<int id>
		[HttpGet("{id:int}")]
		public IActionResult GetStudent(int id) {
			var result = mContext.Students.Where(s => s.Id == id).Select(StudentDto.From)
				.SingleOrDefault();

			if (result is null) {
				return NotFound();
			}
			return Ok(result);
		}

		// Route: api/students/<string name>
		[HttpGet("{name:alpha}")]
		public IActionResult GetStudent(string name) {
			var result = mContext.Students.Where(s => s.FirstName + " " + s.LastName == name).Select(StudentDto.From)
				.FirstOrDefault();
			if (result is null) {
				return NotFound();
			}
			return Ok(result);
		}

		[HttpGet("{id:int}/transcript")]
		public IActionResult GetTranscript(int id) {
			var student = mContext.Students.Include(s => s.Transcript)
				.Where(s => s.Id == id).FirstOrDefault();
			if (student is null) {
				return NotFound();
			}
			return Ok(student.Transcript.Select(g => g.CourseSection.CatalogCourse.ToString()));
		}

		[HttpPost]
		// A POST route implies creating a new object. The object is too complicated to pass as a string
		// in the URL, so it must come from the body of the request. ASP.NET will handle deserializing
		// the JSON of the request body into a StudentDto object.
		public IActionResult Post([FromBody] StudentDto value) {
			Model.Student newStudent = new Model.Student() {
				FirstName = value.FirstName,
				LastName = value.LastName
			};
			mContext.Students.Add(newStudent);
			if (mContext.SaveChanges() > 0) {
				// Saving the changes assigns an ID to the object, so we can now retrieve
				// that object using our existing route.
				return GetStudent(newStudent.Id);
			}
			return NotFound();
		}

		[HttpPut("{id}")]
		// A PUT route implies updating an existing object, which is also specified in the request body.
		public IActionResult Put(int id, [FromBody] StudentDto value) {
			var student = mContext.Students.Where(s => s.Id == id).SingleOrDefault();
			if (student is null) {
				return NotFound();
			}
			student.FirstName = value.FirstName;
			student.LastName = value.LastName;
			int results = mContext.SaveChanges();
			if (results > 0) {
				return Ok();
			}
			else {
				return NoContent();
			}
		}

		[HttpDelete("{id}")]
		// Probably obvious what this one does.
		public IActionResult Delete(int id) {
			var student = mContext.Students.Where(s => s.Id == id).SingleOrDefault();
			if (student is null) {
				return NotFound();
			}
			mContext.Students.Remove(student);
			int results = mContext.SaveChanges();
			if (results > 0) {
				return Ok();
			}
			else {
				return NoContent();
			}
		}
	}
}
