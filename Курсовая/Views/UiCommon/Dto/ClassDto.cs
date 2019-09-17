using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Курсовая.Common;

namespace Курсовая.Views.UiCommon.Dto
{
    public class ClassDto
    {
        public int Id { get; set; }
        public int SchId { get; set; }
        public Week Week { get; set; }
        public int  GroupId { get; set; }
        public string GroupString { get; set; }
        public int SubjectId { get; set; }
        public string SubjectString { get; set; }
        public DateTime Date { get; set; }
        public int? RoomId { get; set; }
        public string RoomString { get; set; }


        public override string ToString()
        {
            return $"{Date}:{SubjectString}:{GroupString}:{RoomString}";
        }
    }
}
