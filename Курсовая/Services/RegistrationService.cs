using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Курсовая.Common;
using Курсовая.DataBase;
using Курсовая.DataBase.RepositoryInterfaces;

namespace Курсовая.Services
{
    public class RegistrationService
    {
        private IRegistrationRepository _registrationRepository;
        private IStudentsRepository _studentsRepository;
        private ITeachersRepository _teachersRepository;

        public RegistrationService(IRegistrationRepository registrationRepository, IStudentsRepository studentsRepository, ITeachersRepository teachersRepository)
        {
            _registrationRepository = registrationRepository;
            _studentsRepository = studentsRepository;
            _teachersRepository = teachersRepository;
        }


        public Регистрация RegisterNewUser(Регистрация item, BaseUser user)
        {
            int userId = 0; 
            if (item.Роли == (long) UserRole.Teacher)
            {
                userId = _teachersRepository.Create(new Преподаватели()
                {
                    Имя_преподавателя = user.Name, Отчество_преподавателя = user.Patronymic,
                    Фамилия_преподавателя = user.Surname
                }).ID_преподавателя;
            }
            else if (item.Роли == (long)UserRole.Student)
            {
                userId = _studentsRepository.Create(new Учащиеся()
                {
                    Имя_Учащегося = user.Name, Отчество_Учащегося = user.Patronymic, Фамилия_Учащегося = user.Surname,
                }).ID_Учащегося;
            }

            item.Ид_пользователя = userId;
            return _registrationRepository.Create(item);
        }



        public Регистрация CheckLogin(Регистрация user)
        {
            return _registrationRepository.FindAll(item => item.Логин == user.Логин && item.Пароль == user.Пароль).FirstOrDefault(); ;
        }
    }
}
