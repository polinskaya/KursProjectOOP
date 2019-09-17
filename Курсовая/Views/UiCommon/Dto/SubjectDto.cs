using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Курсовая.Views.UiCommon.Dto
{
    public class SubjectDto
    {
        public string Name { get; set; }
        public string DepartmentShortName { get; set; }
        public int Id { get; set; }

        public override string ToString()
        {
            return $"{DepartmentShortName}:{Name}";
        }
    }
}
