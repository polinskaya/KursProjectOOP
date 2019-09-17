using Курсовая.DataBase.RepositoryInterfaces;

namespace Курсовая.DataBase.Repositories
{
    public class ClassesRepository : BaseRepository<Занятие>, IClassesRepository
    {
        public ClassesRepository(DataBaseEntities context) : base(context)
        {
        }
    }
}