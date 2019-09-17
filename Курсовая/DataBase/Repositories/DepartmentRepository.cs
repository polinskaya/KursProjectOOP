using Курсовая.DataBase.RepositoryInterfaces;

namespace Курсовая.DataBase.Repositories
{
    public class DepartmentRepository : BaseRepository<Кафедры>, IDepartmentRepository
    {
        public DepartmentRepository(DataBaseEntities context) : base(context)
        {
        }
    }
}