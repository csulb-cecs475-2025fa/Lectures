using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Cecs475.Scheduling.Model;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cecs475.Web8.Scheduling;

namespace Cecs475.Scheduling.Web.Controllers {
	public class InstructorDto {
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public static InstructorDto From(Model.Instructor instr) {
			return new InstructorDto {
				Id = instr.Id,
				FirstName = instr.FirstName,
				LastName = instr.LastName
			};
		}
	}

	[ApiController]
	[Route("api/instructors")]
	public class InstructorsController : ControllerBase {
		private Data.CatalogContext mContext = new Data.CatalogContext(ApplicationSettings.ConnectionString);

		[HttpGet]
		public async Task<IActionResult> GetInstructors() {
			// Normally, calling Select on a DbSet will translate to a blocking (non-async) operation.
			// Instead, we use ToListAsync and await the result to perform a non-blocking call to the database.
			// Once we have the full results, we map them to DTOs and return.
			var instructors = await mContext.Instructors.ToListAsync();
			return Ok(instructors.Select(InstructorDto.From));
		}

		[HttpGet]
		[Route("{id:int}")]
		public async Task<IActionResult> GetInstructor(int id) {
			// SingleOrDefaultAsync is an async version of SingleOrDefault.
			var instructor = await mContext.Instructors.SingleOrDefaultAsync(i => i.Id == id);
			if (instructor != null) {
				return Ok(InstructorDto.From(instructor));
			}
			return NotFound();
		}

		[HttpPost]
		public async Task<IActionResult> CreateInstructor([FromBody]InstructorDto instructor) {
			var existing = await mContext.Instructors.SingleOrDefaultAsync(i => i.FirstName == instructor.FirstName
				&& i.LastName == instructor.LastName);

			if (existing != null) {
				return Forbid();
			}

			mContext.Instructors.Add(new Instructor() {
				FirstName = instructor.FirstName,
				LastName = instructor.LastName
			});

			// Save the changes to the db asynchronously.
			int records = await mContext.SaveChangesAsync();
			if (records == 1) {
				var loaded = await mContext.Instructors.SingleOrDefaultAsync(i => i.FirstName == instructor.FirstName
					&& i.LastName == instructor.LastName);
				return Ok(InstructorDto.From(loaded));
			}
			return BadRequest();
		}
	}
}
