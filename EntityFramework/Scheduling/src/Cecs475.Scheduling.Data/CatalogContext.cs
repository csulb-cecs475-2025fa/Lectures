using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cecs475.Scheduling.Model {
	public class X {

	}

	public class CatalogContext : DbContext {
		public DbSet<SemesterTerm> SemesterTerms { get; set; }
		public DbSet<Instructor> Instructors { get; set; }
		public DbSet<CatalogCourse> Courses { get; set; }
		public DbSet<Student> Students { get; set; }



		private string mConnectionString;
		public CatalogContext(string connectionString) : base() {
			mConnectionString = connectionString;
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
			base.OnConfiguring(optionsBuilder);
			
			optionsBuilder.UseSqlServer(mConnectionString);
			
			// TODO: replace the line above with the one below, IF you are connecting to a Postgres
			// or (most) other SQL database other than Microsoft SQL Server.
			// optionsBuilder.UseNpgsql(mConnectionString);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder) {
			base.OnModelCreating(modelBuilder);

			// Instead of specifying column constraints with C# Attributes, we can also use a "builder"
			// patern.

			// Set constraints on the CatalogCourse columns.
			// As a reminder, the "Id" property will automatically become the table's primary key.
			modelBuilder.Entity<CatalogCourse>()
				.Property(c => c.CourseNumber)
				.HasMaxLength(5); // CourseNumber max length is 5.

			modelBuilder.Entity<CatalogCourse>()
				.Property(c => c.DepartmentName)
				.HasColumnName("Department"); // The C# property DepartmentName will be called Department in the database.

			modelBuilder.Entity<CatalogCourse>()
				.ToTable("CatalogCourses"); // Set the name of the table manually.

			modelBuilder.Entity<CatalogCourse>()
				.Navigation(c => c.Prerequisites)
				.AutoInclude(); // When retrieving a CatalogCourse, always include its prerequisites.

			modelBuilder.Entity<CatalogCourse>()
				.HasMany(c => c.Prerequisites)
				.WithMany()
				.UsingEntity(join => join.ToTable("CatalogCourse_Prerequisites")); // Prerequisites are a many-to-many association.


			modelBuilder.Entity<ClassSection>()
				.HasMany(c => c.EnrolledStudents)
				.WithMany(s => s.EnrolledClasses)
				.UsingEntity(join => join.ToTable("Enrollments"));
			modelBuilder.Entity<ClassSection>()
				.Navigation(s => s.CatalogCourse)
				.AutoInclude();
			modelBuilder.Entity<ClassSection>()
				.Navigation(s => s.Semester)
				.AutoInclude();

			modelBuilder.Entity<CourseGrade>()
				.Navigation(g => g.StudentOfRecord)
				.AutoInclude();
			modelBuilder.Entity<CourseGrade>()
				.Navigation(g => g.CourseSection)
				.AutoInclude();
		}
	}
}
