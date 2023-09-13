using University.BL.Models;
using University.BL.Repositories;

namespace University.BL.Services.Implements
{
    public class StudentService : GenericService<Student>, IStudentService
    {
        public StudentService(IStudentRepository studentRepository) : base(studentRepository)
        {

        }
    }
}
