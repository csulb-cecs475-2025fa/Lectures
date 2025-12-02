using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Cecs475.Scheduling.Model;
namespace Cecs475.Scheduling.Data {
	public class CatalogContext : DbContext {
		public DbSet<SemesterTerm> SemesterTerms { get; set; }
		public DbSet<Instructor> Instructors { get; set; }
		public DbSet<CatalogCourse> Courses { get; set; }
		public DbSet<Student> Students { get; set; }

		private string mConnectionString;
		// The parameter to the base constructor is the name of the ConnectionString in app.config
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

			modelBuilder.Entity<CatalogCourse>()
				.Property(c => c.CourseNumber)
				.HasMaxLength(5);
			modelBuilder.Entity<CatalogCourse>()
				.Property(c => c.DepartmentName)
				.HasColumnName("Department");
			modelBuilder.Entity<CatalogCourse>()
				.ToTable("CatalogCourses");
			modelBuilder.Entity<CatalogCourse>()
				.Navigation(c => c.Prerequisites)
				.AutoInclude();

			modelBuilder.Entity<CatalogCourse>()
				.HasMany(c => c.Prerequisites)
				.WithMany()
				.UsingEntity(join => join.ToTable("CatalogCourse_Prerequisites"));

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
