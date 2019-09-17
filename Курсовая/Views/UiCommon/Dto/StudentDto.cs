using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Курсовая.Common;

namespace Курсовая.Views.UiCommon.Dto
{
    public class StudentDto : BaseUser
    {
        public int Id { get; set; }
        public Sex Sex { get; set; }
        public int? GroupId { get; set; }

        public override string ToString()
        {
            return $"{Surname} {Name[0]}. {Patronymic[0]}";
        }
    }
}
