using Курсовая.DataBase.RepositoryInterfaces;

namespace Курсовая.DataBase.Repositories
{
    public class SpecialtyRepository : BaseRepository<Специальности>, ISpecialtyRepository
    {
        protected SpecialtyRepository(DataBaseEntities context) : base(context)
        {
        }
    }
}