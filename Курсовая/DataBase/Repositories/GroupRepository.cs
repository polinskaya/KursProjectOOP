using Курсовая.DataBase.RepositoryInterfaces;

namespace Курсовая.DataBase.Repositories
{
    public class GroupRepository : BaseRepository<Группы>, IGroupRepository
    {
        public GroupRepository(DataBaseEntities context) : base(context)
        {
        }
    }
}