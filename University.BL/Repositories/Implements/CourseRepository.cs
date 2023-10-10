using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using University.BL.Data;
using University.BL.Models;

namespace University.BL.Repositories.Implements
{
    public class CourseRepository : GenericRepository<Course>, ICourseRepository
    {
        private readonly UniversityContext universityContext;
        public CourseRepository(UniversityContext universityContext) : base(universityContext)
        {
            this.universityContext = universityContext;
        }

        public async Task<bool> DeleteCheckOnEntity(int id)
        {
            var flag = await universityContext.Enrollments.AnyAsync(x => x.CourseID == id);
            return flag;
        }

        public async Task<IEnumerable<Student>> GetStudentsByCourseId(int courseId)
        {
            IEnumerable<Student> listStudents;

            {
                listStudents = (from c in universityContext.Courses
                    join e in universityContext.Enrollments on c.CourseId equals e.CourseID
                    join s in universityContext.Students on e.StudentID equals s.ID
                    where c.CourseId == courseId
                    select s).ToList();
            }

            return listStudents;
        }
    }
}
