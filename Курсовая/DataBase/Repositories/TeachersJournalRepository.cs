using Курсовая.DataBase.RepositoryInterfaces;

namespace Курсовая.DataBase.Repositories
{
    public class TeachersJournalRepository : BaseRepository<Журнал>, ITeachersJournalRepository
    {
        public TeachersJournalRepository(DataBaseEntities context) : base(context)
        {
        }
    }
}