using System.Web;
using System.Text.Json;
using System.Net.Http.Json;
using System.Reflection;
namespace Cecs475.Scheduling.RegistrationApp {
	public class StudentDto {
		public int Id { get; set; }
		public required string FirstName { get; set; }
		public required string LastName { get; set; }
	}

	internal class Program {
		// We will learn what "async", "Task", and "await" mean in the future.

		public static async Task Main(string[] args) {
			HttpClient client = new HttpClient();
			client.BaseAddress = new Uri("http://localhost:5228/api/");

			Console.WriteLine("Menu:\n0. Quit\n1. Show all students\n2. Search by student ID\n3. Create a student");
			string? input = Console.ReadLine() ?? "0";
			int choice = int.Parse(input);


			// THE GENERAL FLOW:
			// Call one of the Async methods on the client;
			// Await the response;
			// Check the status code;
			// Parse and use the response if OK.

			switch (choice) {
				case 0:
					return;

				case 1:
					// For Get methods with no parameters, this is easy:
					// GET /api/students
					var studentsResponse = await client.GetAsync("students");
					if (studentsResponse.IsSuccessStatusCode) {
						// Parse the response content (body) as a list of StudentDto objects.
						var students = await studentsResponse.Content.ReadFromJsonAsync<List<StudentDto>>();
						if (students is null) {
							Console.WriteLine("No students found");
						}
						else {
							// Do something with the response.
							foreach (StudentDto student in students) {
								Console.WriteLine($"{student.FirstName} {student.LastName} (ID {student.Id})");
							}
						}
					}
					else {
						Console.WriteLine(studentsResponse.StatusCode);
					}
					break;

				case 2:
					// For methods with parameters, build a URI string with the parameters.
					Console.Write("Enter a student ID: ");
					int studentId = int.Parse(Console.ReadLine() ?? "0");

					var searchResponse = await client.GetAsync($"students/{studentId}");
					if (searchResponse.IsSuccessStatusCode) {
						var searchStudent = await searchResponse.Content.ReadFromJsonAsync<StudentDto>();
						if (searchStudent is null) {
							Console.WriteLine($"No student found with id {studentId}");
						}
						else {
							Console.WriteLine($"{searchStudent.FirstName} {searchStudent.LastName} (ID {searchStudent.Id})");
						}
					}
					break;

				case 3:
					// For methods with BODY parameters, pass the JSON body to the request using JsonContent.Create.
					Console.Write("Enter student's first name: ");
					string first = Console.ReadLine() ?? "Firstname";
					string last = Console.ReadLine() ?? "Lastname";

					StudentDto newStudent = new() { FirstName = first, LastName = last };
					var createStudentResponse = await client.PostAsync("students", JsonContent.Create(newStudent));
					if (createStudentResponse.IsSuccessStatusCode) {
						var student = await createStudentResponse.Content.ReadFromJsonAsync<StudentDto>();
						if (student is null) {
							Console.WriteLine("Something went wrong");
						}
						else {
							Console.WriteLine($"CREATED: {student.FirstName} {student.LastName} (ID {student.Id})");
						}
					}
					break;
			}
		} 
	}
}
