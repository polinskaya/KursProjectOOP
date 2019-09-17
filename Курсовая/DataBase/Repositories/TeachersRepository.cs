using Курсовая.DataBase.RepositoryInterfaces;

namespace Курсовая.DataBase.Repositories
{
    public class TeachersRepository : BaseRepository<Преподаватели>, ITeachersRepository
    {
        public TeachersRepository(DataBaseEntities context) : base(context)
        {
        }
    }
}