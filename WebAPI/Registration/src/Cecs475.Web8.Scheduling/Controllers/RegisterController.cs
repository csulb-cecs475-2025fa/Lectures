using Cecs475.Scheduling.Model;
using Cecs475.Web8.Scheduling;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cecs475.Scheduling.Web.Controllers {
	public class ClassSectionDto {
		public int Id { get; set; }
		public int SemesterTermId { get; set; }
		public CatalogCourseDto CatalogCourse { get; set; }
		public int SectionNumber { get; set; }

		public static ClassSectionDto From(ClassSection section) {
			return new ClassSectionDto {
				Id = section.Id,
				SemesterTermId = section.Semester.Id,
				SectionNumber = section.SectionNumber,
				CatalogCourse = CatalogCourseDto.From(section.CatalogCourse)
			};
		}
	}

	public class RegistrationDto {
		public int StudentID { get; set; }
		public ClassSectionDto CourseSection { get; set; }
	}

	[ApiController]
	[Route("api/register")]
	public class RegisterController : ControllerBase {
		private Data.CatalogContext mContext = new Data.CatalogContext(ApplicationSettings.ConnectionString);

		[HttpPost]
		public async Task<IActionResult> RegisterForCourse([FromBody]RegistrationDto studentCourse) {
			// Load the student and the semester concurrently.
			var studentTask = mContext.Students.FirstOrDefaultAsync(s => s.Id == studentCourse.StudentID);
			var semesterTask = mContext.SemesterTerms.Where(
				t => t.Id == studentCourse.CourseSection.SemesterTermId)
				.SingleOrDefaultAsync();

			Student? student = await studentTask;
			if (student is null) {
				return NotFound();
			}

			SemesterTerm? term = await semesterTask;
			if (term is null) {
				return NotFound();
			}

			// Find the class section.
			ClassSection? section = term.CourseSections.SingleOrDefault(
				c => c.CatalogCourse.DepartmentName == studentCourse.CourseSection.CatalogCourse.DepartmentName
					  && c.CatalogCourse.CourseNumber == studentCourse.CourseSection.CatalogCourse.CourseNumber
					  && c.SectionNumber == studentCourse.CourseSection.SectionNumber);
			if (section is null) {
				return NotFound();
			}

			var regResult = student.CanRegisterForCourseSection(section);
			if (regResult == RegistrationResults.Success) {
				section.EnrolledStudents.Add(student);
				await mContext.SaveChangesAsync();
			}

			return Ok(regResult);
		}
	}
}
