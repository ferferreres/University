using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using University.BL.Data;
using University.BL.Models;

namespace University.BL.Repositories.Implements
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        private readonly UniversityContext universityContext;
        public StudentRepository(UniversityContext universityContext) : base(universityContext)
        {
            this.universityContext = universityContext;
        }

        public async Task<bool> DeleteCheckOnEntity(int id)
        {
            var flag = await universityContext.Enrollments.AnyAsync(x => x.CourseID == id);
            return flag;
        }

        public async Task<IEnumerable<Course>> GetCoursesByStudentId(int studentId)
        {
            IEnumerable<Course> listCourses;

            {listCourses = (from s in universityContext.Students
                            join e in universityContext.Enrollments on s.ID equals e.StudentID
                            join c in universityContext.Courses on e.CourseID equals c.CourseId
                               where s.ID == studentId select c).ToList();
            }

            return listCourses;
        }
    }
}
