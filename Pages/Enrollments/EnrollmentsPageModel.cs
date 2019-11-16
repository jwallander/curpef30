using System.Linq;
using ContosoUniversity.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Pages.Enrollments
{
    public class EnrollmentsPageModel : PageModel
    {
        public SelectList CourseNameSL { get; set; }
        public SelectList StudentNameSL { get; set; }

        public void PopulateCourseDropDownList(SchoolContext _context,
            object selectedCourse = null)
        {
            var coursesQuery = from c in _context.Courses
                                   orderby c.Title // Sort by name.
                                   select c;

            CourseNameSL = new SelectList(coursesQuery.AsNoTracking(),
                        "CourseID", "Title", selectedCourse);
        }

        public void PopulateStudentDropDownList(SchoolContext _context,
            object selectedStudent = null)
        {
            var studentsQuery = from s in _context.Students
                orderby s.LastName // Sort by name.
                select s;

            StudentNameSL = new SelectList(studentsQuery.AsNoTracking(),
                "ID", "FullName", selectedStudent);
        }
    }
}