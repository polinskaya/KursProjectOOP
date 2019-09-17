using Курсовая.DataBase.RepositoryInterfaces;

namespace Курсовая.DataBase.Repositories
{
    public class RegistrationRepository : BaseRepository<Регистрация>, IRegistrationRepository
    {
        public RegistrationRepository(DataBaseEntities context) : base(context)
        {
        }
    }
}