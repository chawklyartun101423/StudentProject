using Microsoft.AspNetCore.Mvc;
using StudentProject.Models;
using StudentProject.Service;

namespace StudentProject.Controllers
{
	public class StudentController : Controller

	{
		private readonly StudentService _studentService;

		public StudentController(StudentService studentService)
		{

			_studentService = studentService;

		}

		public async Task<IActionResult> StudentDataTable()
		{
			try
			{
				var draw = HttpContext.Request.Form["draw"].FirstOrDefault();

				// Paging Length 10,20  
				var length = Request.Form["length"].FirstOrDefault();

				// Skiping number of Rows count  
				var start = Request.Form["start"].FirstOrDefault();

				// Search Value from (Search box)  
				var searchValue = Request.Form["search[value]"].FirstOrDefault();

				// Sort Column Name  
				//var sortColumn = Request.Form["order[0][column]"].FirstOrDefault();
				var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();

				// Sort Column Direction ( asc ,desc)  
				var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();

				//Paging Size (10,20,50,100)  
				int pageSize = length != null ? Convert.ToInt32(length) : 0;
				int skip = start != null ? Convert.ToInt32(start) : 0;
				int recordsTotal = 0;

				// Getting all User data  
				var studentList = await _studentService.GetAllStudentList(sortColumn, sortColumnDirection, searchValue);

				//total number of rows count   
				recordsTotal = studentList.Count();
				//Paging   
				var data = studentList.Skip(skip).Take(pageSize).ToList();
				//Returning Json Data  
				return Json(new { draw, recordsFiltered = recordsTotal, recordsTotal, data });
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public IActionResult StudentListing()
		{
			var StudentList = _studentService.GetAllStudents();
			return View(StudentList);
		}
		public IActionResult AddStudent()
		{
			return View();
		}

		[HttpPost("/Save")]
		public IActionResult Save(Student student)
		{
			var result = _studentService.CreateStudent(student);
			if (result > 0)
			{
				// Save student logic here
				return RedirectToAction("StudentListing"); // Redirect to the same page or another action
			}

			return View(student);
		}
		[HttpGet]
		public IActionResult EditStudent(int id)
		{
			var result = _studentService.GetStudent(id);

			return View(result);
		}

		[HttpPost]
		public IActionResult Update(Student student)
		{
			var result = _studentService.UpdateStudent(student);

			if (result > 0)
			{

				return RedirectToAction("StudentListing");
			}
			return View();
		}
		[HttpGet]
		public IActionResult DeleteStudent(int id)
		{
			var result = _studentService.GetStudent(id);

			return View(result);
		}
		[HttpPost]
		public IActionResult RemoveStudent(int id)
		{

			var result = _studentService.DeleteStudent(id);
			if (result > 0)
			{
				return RedirectToAction("StudentListing");
			}

			return View();
		}


	}
}
