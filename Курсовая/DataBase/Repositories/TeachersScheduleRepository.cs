using Курсовая.DataBase.RepositoryInterfaces;

namespace Курсовая.DataBase.Repositories
{
    public class TeachersScheduleRepository : BaseRepository<Расписание>, ITeachersScheduleRepository
    {
        public TeachersScheduleRepository(DataBaseEntities context) : base(context)
        {
        }
    }
}