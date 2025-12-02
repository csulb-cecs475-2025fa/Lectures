using Cecs475.Web8.Scheduling;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace Cecs475.Scheduling.Web.Controllers {
	public class CatalogCourseDto {
		public int Id { get; set; }
		public required string DepartmentName { get; set; }
		public required string CourseNumber { get; set; }
		public required IEnumerable<int> PrerequisiteCourseIds { get; set; }

		public static CatalogCourseDto From(Model.CatalogCourse course) {
			return new CatalogCourseDto {
				Id = course.Id,
				DepartmentName = course.DepartmentName,
				CourseNumber = course.CourseNumber,
				PrerequisiteCourseIds = course.Prerequisites.Select(c => c.Id)
			};
		}
	}
	[ApiController]
	[Route("api/courses")]
	public class CoursesController : ControllerBase {
		private Data.CatalogContext mContext = new Data.CatalogContext(ApplicationSettings.ConnectionString);

		[HttpGet]
		public IEnumerable<CatalogCourseDto> GetCourses() {
			return mContext.Courses.Include(c => c.Prerequisites)
				.Select(CatalogCourseDto.From);
		}

		[HttpGet("{id:int}")]
		public IActionResult GetCourse(int id) {
			var course = mContext.Courses.Include(c => c.Prerequisites)
				.Where(c => c.Id == id).SingleOrDefault();
			if (course == null) {
				return NotFound();
			}
			return Ok(CatalogCourseDto.From(course));
		}

		[HttpGet("{name:alpha}")]
		public IActionResult GetCourse(string name) {
			var course = mContext.Courses.Include(c => c.Prerequisites)
				.Where(c => c.DepartmentName + " " + c.CourseNumber == name)
				.SingleOrDefault();
			if (course == null) {
				return NotFound();
			}
			return Ok(CatalogCourseDto.From(course));
		}
	}
}