using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using University.BL.Models;
using University.BL.Repositories;

namespace University.BL.Services.Implements
{
    public class StudentService : GenericService<Student>, IStudentService
    {
        private readonly IStudentRepository studentRepository;

        public StudentService(IStudentRepository studentRepository) : base(studentRepository)
        {
            this.studentRepository = studentRepository;
        }

        public async Task<bool> DeleteCheckOnEntity(int id)
        {
            return await studentRepository.DeleteCheckOnEntity(id);
        }

        public async Task<IEnumerable<Course>> GetCoursesByStudentId(int studentId)
        {
            return await studentRepository.GetCoursesByStudentId(studentId);
        }
    }
}
