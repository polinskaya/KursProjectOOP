using Курсовая.DataBase.RepositoryInterfaces;

namespace Курсовая.DataBase.Repositories
{
    public class RoomsRepository : BaseRepository<Аудитории>, IRoomsRepository
    {
        public RoomsRepository(DataBaseEntities context) : base(context)
        {
        }
    }
}