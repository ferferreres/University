using System.Collections.Generic;
using System.Threading.Tasks;
using University.BL.Models;

namespace University.BL.Services
{
    public interface IStudentService : IGenericService<Student>
    {
        Task<bool> DeleteCheckOnEntity(int id);

        Task<IEnumerable<Course>> GetCoursesByStudentId(int studentId);
    }
}
