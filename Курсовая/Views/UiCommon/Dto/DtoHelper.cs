using System;
using System.Collections.Generic;
using Курсовая.Common;
using Курсовая.DataBase;

namespace Курсовая.Views.UiCommon.Dto
{
    public static class DtoHelper
    {

        public static List<FacultyDto> ConvertToFacultyDtos(List<Факультет> facultes)
        {
            List<FacultyDto> res = new List<FacultyDto>();
            foreach (Факультет faculte in facultes)
            {
                res.Add(new FacultyDto(){ShortName = faculte.Код_факультета, FullName = faculte.Наименование_факультета});
            }

            return res;
        }


        public static List<DepartmentDto> ConvertToDepartmentDtos(List<Кафедры> departments)
        {
            List<DepartmentDto> res = new List<DepartmentDto>();
            foreach (Кафедры department in departments)
            {
                res.Add(new DepartmentDto()
                {
                    Faculty = department.Код_факультета, ShortName = department.Код_кафедры, FullName = department.Наименование_кафедры
                });
            }

            return res;
        }

        public static List<StudentDto> ConvertToStudentDtos(List<Учащиеся> students)
        {
            List<StudentDto> res = new List<StudentDto>();
            foreach (Учащиеся student in students)
            {
                res.Add(new StudentDto()
                {
                    GroupId = student.ID_группы, Id = student.ID_Учащегося, Surname = student.Фамилия_Учащегося,
                    Name = student.Имя_Учащегося, Patronymic = student.Отчество_Учащегося, Sex = student.Пол != null ? (Sex)student.Пол : Sex.Male
                });
            }

            return res;
        }

        public static List<GroupDto> ConvertToGroupDtos(List<Группы> groups)
        {
            List<GroupDto> res = new List<GroupDto>();
            foreach (Группы @group in groups)
            {
                res.Add(new GroupDto()
                {
                    Faculty = group.Код_факультета, Name = group.Номер_группы, Id = group.ID_группы, Specialty = group.Код_специальности, 
                    StartDate = group.Год_поступления != null ? new DateTime((long)group.Год_поступления) : DateTime.Today
                });
            }

            return res;
        }

        public static List<SubjectDto> ConvertToSubjectDtos(List<Дисциплины> groups)
        {
            List<SubjectDto> res = new List<SubjectDto>();
            foreach (Дисциплины @group in groups)
            {
                res.Add(new SubjectDto()
                {
                    Name = group.Название_дисциплины,
                    DepartmentShortName = group.Код_кафедры,
                    Id = group.ID_дисциплины
                });
            }

            return res;
        }

        public static List<ClassDto> ConvertToClassDtos(List<Занятие> classes)
        {
            List<ClassDto> res = new List<ClassDto>();
            foreach (Занятие item in classes)
            {
                res.Add(new ClassDto()
                {
                    Id = item.ID_Занятия,
                    GroupId = item.ID_Группы,
                    SubjectId = item.ID_Дисциплины,
                    RoomId = item.ID_Аудитории,
                    SchId = item.C_ID_Расписания,
                    Week = (Week)item.Неделя,
                    Date = item.ДатаИВремя,
                    GroupString = item.Группы == null ? String.Empty : $"{item.Группы.Код_факультета}:{item.Группы.Код_специальности}:{item.Группы.Номер_группы}:{EnumHelper.GroupTypeList[item.Группы.Тип ?? 0]}",
                    RoomString = item.Аудитории == null ?String.Empty : $"{item.Аудитории.Название}:{EnumHelper.SubjTypeList[item.Аудитории.Типа_аудитории_]}",
                    SubjectString = $"{item.Дисциплины.Код_кафедры}:{item.Дисциплины.Название_дисциплины}"
                });
            }

            return res;
        }
    }
}
