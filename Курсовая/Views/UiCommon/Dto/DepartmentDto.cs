using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Курсовая.Views.UiCommon.Dto
{
    public class DepartmentDto
    {
        public string ShortName { get; set; }
        public string FullName { get; set; }
        public string Faculty { get; set; }

        public override string ToString()
        {
            return $"{Faculty}:{ShortName}";
        }
    }
}
