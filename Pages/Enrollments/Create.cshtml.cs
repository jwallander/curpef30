using System.Threading.Tasks;
using ContosoUniversity.Data;
using ContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContosoUniversity.Pages.Enrollments
{
    public class CreateModel : EnrollmentsPageModel
    {
        private readonly ContosoUniversity.Data.SchoolContext _context;

        private Student student;
        private Course course;

        public CreateModel(ContosoUniversity.Data.SchoolContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Enrollment Enrollment { get; set; }

        public async Task<IActionResult> OnGetAsync(int? studentID, int? courseID)
        {
            student = await _context.Students.FindAsync(studentID);
            course = await _context.Courses.FindAsync(courseID);

            PopulateCourseDropDownList(_context, course?.CourseID);
            PopulateStudentDropDownList(_context, student?.ID);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var emptyEnrollment = new Enrollment();

            if (await TryUpdateModelAsync<Enrollment>(
                 emptyEnrollment,
                 "Enrollment",   // Prefix for form value.
                 s => s.CourseID, s => s.StudentID, s => s.Grade))
            {
                _context.Enrollments.Add(emptyEnrollment);
                await _context.SaveChangesAsync();
                return RedirectToPage("../Courses/Details", new { ID = emptyEnrollment.CourseID});
            }
             
            // Select DepartmentID if TryUpdateModelAsync fails.
            PopulateCourseDropDownList(_context, emptyEnrollment.CourseID);
            PopulateStudentDropDownList(_context, emptyEnrollment.StudentID);
            return Page();
        }
      }
}