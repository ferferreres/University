using System.Threading.Tasks;
using University.BL.Models;

namespace University.BL.Repositories
{
    public interface IStudentRepository : IGenericRepository<Student>
    {
        Task<bool> DeleteCheckOnEntity(int id);
    }
}
