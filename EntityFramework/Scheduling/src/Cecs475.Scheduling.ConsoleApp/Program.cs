using Cecs475.Scheduling.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test {
	class Program {
		static void Main(string[] args) {


			CatalogContext con = new CatalogContext(@"data source=(LocalDb)\MSSQLLocalDb;initial catalog=CsulbCatalog;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework");
			con.Database.EnsureCreated();

			int choice = -1;
			do {
				Console.WriteLine("Menu:\n0. Quit\n1. Populate database\n2. Show courses\n3. Show course sections\n"
					+ "4. Print transcript\n5. Attempt to register");
				choice = int.Parse(Console.ReadLine()!);

				switch (choice) {
					case 1:
						// Add some courses to the catalog
						var cecs174 = new CatalogCourse() {
							DepartmentName = "CECS",
							CourseNumber = "174",
						};
						con.Courses.Add(cecs174);

						var cecs274 = new CatalogCourse() {
							DepartmentName = "CECS",
							CourseNumber = "274",
						};
						cecs274.Prerequisites.Add(cecs174);
						con.Courses.Add(cecs274);

						var cecs228 = new CatalogCourse() {
							DepartmentName = "CECS",
							CourseNumber = "228",
						};
						cecs228.Prerequisites.Add(cecs174);
						con.Courses.Add(cecs228);

						var cecs277 = new CatalogCourse() {
							DepartmentName = "CECS",
							CourseNumber = "277",
						};
						cecs277.Prerequisites.Add(cecs274);
						cecs277.Prerequisites.Add(cecs228);
						con.Courses.Add(cecs277);

						// Add a semester term
						var spring2017 = new SemesterTerm() {
							Name = "Spring 2017",
							StartDate = new DateTime(2017, 1, 23),
							EndDate = new DateTime(2017, 5, 26)
						};
						con.SemesterTerms.Add(spring2017);
						var fall2017 = new SemesterTerm() {
							Name = "Fall 2017",
							StartDate = new DateTime(2017, 8, 21),
							EndDate = new DateTime(2017, 12, 22)
						};
						con.SemesterTerms.Add(fall2017);

						// Add instructors
						var neal = new Instructor() {
							FirstName = "Neal",
							LastName = "Terrell",
						};
						con.Instructors.Add(neal);
						var anthony = new Instructor() {
							FirstName = "Anthony",
							LastName = "Giacalone"
						};
						con.Instructors.Add(anthony);

						// Add sections
						var cecs174_99 = new ClassSection() {
							CatalogCourse = cecs174,
							SectionNumber = 1,
							Instructor = neal,
							MeetingDays = DaysOfWeek.Monday | DaysOfWeek.Wednesday,
							StartTime = new DateTime(2017, 1, 1, 8, 0, 0), // 9 am
							EndTime = new DateTime(2017, 1, 1, 8, 50, 0),
						};
						spring2017.CourseSections.Add(cecs174_99);

						var cecs228_99 = new ClassSection() {
							CatalogCourse = cecs228,
							SectionNumber = 99,
							Instructor = anthony,
							MeetingDays = DaysOfWeek.Friday,
							StartTime = new DateTime(2017, 1, 1, 10, 0, 0), // 9 am
							EndTime = new DateTime(2017, 1, 1, 11, 50, 0),
						};
						spring2017.CourseSections.Add(cecs228_99);

						var cecs228_01 = new ClassSection() {
							CatalogCourse = cecs228,
							SectionNumber = 1,
							Instructor = neal,
							MeetingDays = DaysOfWeek.Tuesday | DaysOfWeek.Thursday,
							StartTime = new DateTime(2017, 1, 1, 9, 0, 0), // 9 am
							EndTime = new DateTime(2017, 1, 1, 9, 50, 0),
						};
						fall2017.CourseSections.Add(cecs228_01);

						var cecs277_01 = new ClassSection() {
							CatalogCourse = cecs277,
							SectionNumber = 1,
							Instructor = anthony,
							MeetingDays = DaysOfWeek.Monday | DaysOfWeek.Wednesday,
							StartTime = new DateTime(2017, 1, 1, 12, 30, 0), // 9 am
							EndTime = new DateTime(2017, 1, 1, 13, 20, 0),
						};
						fall2017.CourseSections.Add(cecs277_01);

						var cecs274_05 = new ClassSection() {
							CatalogCourse = cecs274,
							SectionNumber = 5,
							Instructor = anthony,
							MeetingDays = DaysOfWeek.Tuesday | DaysOfWeek.Thursday,
							StartTime = new DateTime(2017, 1, 1, 9, 30, 0), // 9 am
							EndTime = new DateTime(2017, 1, 1, 10, 20, 0),
						};
						fall2017.CourseSections.Add(cecs274_05);

						var cecs274_11 = new ClassSection() {
							CatalogCourse = cecs274,
							SectionNumber = 11,
							Instructor = anthony,
							MeetingDays = DaysOfWeek.Monday| DaysOfWeek.Wednesday | DaysOfWeek.Friday,
							StartTime = new DateTime(2017, 1, 1, 13, 0, 0), // 1 pm
							EndTime = new DateTime(2017, 1, 1, 13, 50, 0),
						};
						fall2017.CourseSections.Add(cecs274_11);

						Student s1 = new Student() {
							FirstName = "Abby",
							LastName = "Blitzen",
						};
						s1.Transcript.Add(new CourseGrade() {
							CourseSection = cecs174_99,
							GradeEarned = GradeTypes.A
						});
						s1.Transcript.Add(new CourseGrade() {
							CourseSection = cecs228_99,
							GradeEarned = GradeTypes.D
						});
						con.Students.Add(s1);

						Student s2 = new Student() {
							FirstName = "Carmen",
							LastName = "Diego",
						};
						con.Students.Add(s2);
						s2.EnrolledClasses.Add(cecs274_11);

						con.SaveChanges();
						break;


					case 2:
						// Print all CECS courses in the catalog

						// Load all courses, and include their prerequisite references.
						var allCourses = con.Courses.Include(c => c.Prerequisites)
							.OrderBy(c => c.CourseNumber);

						foreach (var course in allCourses) {
							Console.Write($"{course.DepartmentName} {course.CourseNumber}");
							if (course.Prerequisites.Count > 0) {
								Console.Write(" (Prerequisites: ");
								Console.Write(string.Join(", ", course.Prerequisites));
								Console.Write(")");
							}
							Console.WriteLine();
						}

						break;

					case 3:
						// Print all offered sections for Fall 2017

						// Our CatalogContext configures ClassSection to always include its CatalogCourse.
						// We also want Instructor here, so we must include it.
						var fallSem = con.SemesterTerms
										.Include(s => s.CourseSections)
										.ThenInclude(s => s.Instructor)
										.Where(s => s.Name == "Fall 2017").FirstOrDefault();
						if (fallSem is null) {
							break;
						}

						Console.WriteLine($"{fallSem.Name}: {fallSem.StartDate.ToString("MMM dd")} - {fallSem.EndDate.ToString("MMM dd")}");
						foreach (var section in fallSem.CourseSections) {
							Console.WriteLine($"{section.CatalogCourse}-{section.SectionNumber.ToString("D2")} -- " +
								$"{section.Instructor.FirstName[0]} {section.Instructor.LastName} -- " +
								$"{section.MeetingDays}, {section.StartTime.ToShortTimeString()} to {section.EndTime.ToShortTimeString()}");
						}
						break;

					case 4:
						// Print a student's transcript.

						Console.WriteLine("Enter a name:");
						string name = Console.ReadLine()!;
						string[] split = name.Split(' ');
						string first = split[0], last = split[1];
						
						// Load the student and include their transcript.
						Student? tStudent = con.Students
												.Include(s => s.Transcript)
												.Where(s => s.FirstName == first && s.LastName == last)
							.FirstOrDefault();
						if (tStudent is null) {
							Console.WriteLine("Not found");
							break;
						}
						Console.WriteLine($"{tStudent.FirstName} {tStudent.LastName}");
						Console.WriteLine("Transcript: {0}",
							string.Join(", ", tStudent.Transcript));

						break;

					case 5:
						// Attempt to enroll a student in a class section.
						Console.WriteLine("Enter a name:");
						string enrollName = Console.ReadLine()!;
						string[] enrollSplit = enrollName.Split(' ');
						string enrollFirst = enrollSplit[0], enrollLast = enrollSplit[1];
						Student? eStudent = con.Students
												.Include(s => s.Transcript)
												.Include(s => s.EnrolledClasses)
												.Where(s => s.FirstName == enrollFirst && s.LastName == enrollLast)
							.FirstOrDefault();
						if (eStudent is null) {
							Console.WriteLine("Not found");
							break;
						}

						Console.WriteLine("Enter a class section for Fall 2017, e.g., CECS 174-01:");
						string className = Console.ReadLine()!;
						string[] csplit = className.Split([' ', '-']);
						string dept = csplit[0];
						string num = csplit[1];
						if (!int.TryParse(csplit[2], out int secNum)) {
							Console.WriteLine("Bad class identifier");
							break;
						}

						// Load the semester, including its sections.
						SemesterTerm f2017 = con.SemesterTerms
												.Include(s => s.CourseSections)
												.Where(s => s.Name == "Fall 2017").First();
						// Load the class section matching the given department name, number, and section.
						ClassSection? sec2 = f2017.CourseSections.Where(
							c => c.CatalogCourse.DepartmentName == dept &&
							c.CatalogCourse.CourseNumber == num &&
							c.SectionNumber == secNum).FirstOrDefault();

						if (sec2 is null) {
							Console.WriteLine("Section not found");
							break;
						}

						var register = eStudent.CanRegisterForCourseSection(sec2);
						Console.WriteLine(register);
						break;

				}
				Console.WriteLine();
				Console.WriteLine();

			} while (choice != 0);
		}



	}
}