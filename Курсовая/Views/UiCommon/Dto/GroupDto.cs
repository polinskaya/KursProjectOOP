using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Курсовая.Common;

namespace Курсовая.Views.UiCommon.Dto
{
    public class GroupDto
    {
        public int Id { get; set; }
        public GroupType Type { get; set; }
        public string Name { get; set; }
        public string Faculty { get; set; }
        public string Specialty { get; set; }
        public DateTime StartDate { get; set; }

        public override string ToString()
        {
            return $"{Faculty}:{Specialty}:{Name}:{EnumHelper.GroupTypeList[(int)Type]}";
        }
    }
}
