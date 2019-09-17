using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Курсовая.Common
{

    public enum Sex
    {
        Male,
        Female
    }

    public enum UserRole
    {
        Teacher,
        Student
    }

    public enum Week
    {
        Even,
        Odd
    }

    public enum GroupType
    {
        FullTime,
        Extramural
    }

    public static class EnumHelper
    {
        public static List<string> timeTypeList = new List<string>()
        {
            "8:00-9:35",
            "9:50-11:25",
            "11:40-13:15",
            "13:50-15:25",
            "15:40-17:15",
            "17:30-19:05",
            "19:20-21:05"
        };
        public static List<string> GroupNumbersTypeList = new List<string>()
        {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"
        };
        public static List<string> weekTypeList = new List<string>()
        {
            "Четная",
            "Нечетная"
        };
        public static List<string> KyrsTypeList = new List<string>()
        {
            "1",
            "2",
            "3",
            "4",
            "5"
        };
        public static List<string> GroupTypeList = new List<string>()
        {
            "Очная",
            "Заочная"
        };
        public static List<string> SexTypeList = new List<string>()
        {
            "Мужской",
            "Женский"
        };
        public static List<string> RoleTypeList = new List<string>()
        {
            "Преподаватель",
            "Студент"
        };
        public static List<string> PulpitTypeList = new List<string>()
        {          
            "ВЫШМАТ",
            "МЕХАНИКА",
            "ДИЗАЙН",
            "ПРОГРАММИРОВАНИЕ"
        };
        public static List<string> SpecTypeList = new List<string>()
        {
            "ИСИТ",
            "ПОИТ",
            "ДЭВИ",
            "ПОИБМС"
        };
        public static List<string> SubjTypeList = new List<string>()
        {
            "лекция",
            "лабораторная",
            "практическая",
            "семинар",
            "курсовая"
        };
    }
}
