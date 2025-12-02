using Cecs475.Scheduling.Model;
using Cecs475.Web8.Scheduling;
using Microsoft.AspNetCore.Mvc;

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
		public IActionResult RegisterForCourse([FromBody]RegistrationDto studentCourse) {
			Student? student = mContext.Students.Where(s => s.Id == studentCourse.StudentID).FirstOrDefault();
			// Simulate a slow connection / complicated operation by sleeping.
			Thread.Sleep(3000);

			if (student is null) {
				return NotFound();
			}

			SemesterTerm? term = mContext.SemesterTerms.Where(
				t => t.Id == studentCourse.CourseSection.SemesterTermId)
				.SingleOrDefault();

			if (term is null) {
				return NotFound();
			}

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
				mContext.SaveChanges();
			}

			return Ok(regResult);
		}
	}
}
