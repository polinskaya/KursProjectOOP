using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Курсовая.Views.UiCommon.Dto
{
    public class RoomDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        
        public override string ToString()
        {
            return $"{Name}:{Type}";
        }
    }
}
