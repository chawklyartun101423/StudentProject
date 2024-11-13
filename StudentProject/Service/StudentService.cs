using System.Diagnostics.Eventing.Reader;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using StudentProject.Models;
using System.Linq.Dynamic.Core;

namespace StudentProject.Service
{
	public class StudentService
	{
		private readonly ApplicationDbContext _context;

		public StudentService(ApplicationDbContext context)
		{
			_context = context;

		}
		public int CreateStudent(Student student)
		{
			//new student into the database context
			_context.Students.Add(student);
			var result = _context.SaveChanges();
			return result;
		}

		public int UpdateStudent(Student student)
		{
			var isExistStudent = _context.Students.Any(x => x.Id == student.Id);
			if (isExistStudent == true)
			{

				var result = _context.Students.Where(x => x.Id == student.Id)
					 .ExecuteUpdate(x => x
					 .SetProperty(a => a.Name, student.Name)
					 .SetProperty(a => a.Email, student.Email)
					 .SetProperty(a => a.PhoneNumber, student.PhoneNumber
					 ));
				return 1;
			}
			else
				return 0;
		}

		public int DeleteStudent(int studentId)
		{
			var isExistStudent = _context.Students.Any(x => x.Id == studentId);
			if (isExistStudent)
			{
				var successDelete = _context.Students.Where(x => x.Id == studentId).ExecuteDelete();

				return successDelete;
			}
			return 0;

		}

		public Student? GetStudent(int id)
		{
			var IsExistStudent = _context.Students.Any(x => x.Id == id);

			if (IsExistStudent == true)
			{
				var Student = _context.Students.FirstOrDefault(x => x.Id == id);
				return Student;
			}

			else
				return null;



		}

		public List<Student> GetAllStudents()
		{
			var result = _context
				  .Students
				  .ToList();
		
			return result;
		}

		public async Task<IQueryable<Student>> GetAllStudentList(string sortColumn, string sortColumnDirection, string searchValue)
		{
			var StudentList = (from student in _context.Students
							   select new Student
							   {
								   Id = student.Id,
								   Name = student.Name,
								   Email = student.Email,
								   PhoneNumber=student.PhoneNumber,
  
							   }).ToList().AsQueryable();

			//Sorting
			if (!string.IsNullOrEmpty(sortColumn))
			{

				StudentList.OrderBy(sortColumn + " " + sortColumnDirection);

			}
			//Search
			if (!string.IsNullOrEmpty(searchValue))
			{

				StudentList = StudentList.Where(x => x.Name.IndexOf(searchValue, StringComparison.OrdinalIgnoreCase) >= 0 ||
				   x.Email != null && x.Email.IndexOf(searchValue, StringComparison.OrdinalIgnoreCase) >= 0 ||
				   x.PhoneNumber != null && x.PhoneNumber.IndexOf(searchValue, StringComparison.OrdinalIgnoreCase) >= 0
				   );
			}

			return StudentList;

		}

	}
}

