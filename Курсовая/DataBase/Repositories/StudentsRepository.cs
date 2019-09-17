using Курсовая.DataBase.RepositoryInterfaces;

namespace Курсовая.DataBase.Repositories
{
    public class StudentsRepository : BaseRepository<Учащиеся>, IStudentsRepository
    {
        public StudentsRepository(DataBaseEntities context) : base(context)
        {
        }
    }
}