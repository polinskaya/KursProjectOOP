using Курсовая.DataBase.RepositoryInterfaces;

namespace Курсовая.DataBase.Repositories
{
    public class FacultyRepository : BaseRepository<Факультет>, IFacultyRepository
    {
        public FacultyRepository(DataBaseEntities context) : base(context)
        {
        }
    }
}