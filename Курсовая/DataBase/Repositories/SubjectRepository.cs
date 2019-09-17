using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Курсовая.DataBase.RepositoryInterfaces;

namespace Курсовая.DataBase.Repositories
{
    public class SubjectRepository :BaseRepository<Дисциплины> , ISubjectsRepository
    {
        public SubjectRepository(DataBaseEntities context) : base(context)
        {
        }
    }
}
