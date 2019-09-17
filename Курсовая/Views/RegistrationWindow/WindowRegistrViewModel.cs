using System;
using System.Collections.Generic;
using System.Windows;
using NavigationDrawerPopUpMenu2;
using Курсовая.Common;
using Курсовая.DataBase;
using Курсовая.DataBase.Repositories;
using Курсовая.Services;

namespace Курсовая
{
    public class WindowRegistrViewModel : BaseViewModel
    {

        private RegistrationService _registrationService;

        public WindowRegistrViewModel()
        {
            _registrationService = new RegistrationService(new RegistrationRepository(App.DataBaseContext),
                new StudentsRepository(App.DataBaseContext), 
                new TeachersRepository(App.DataBaseContext));
        }


        public bool SetPasswordProp(string password)
        {
            //проверка на правильность пароля
            //если что-то не так то возвращаем False

            Password = password;
            return true;
        }


        private void OnRegister(object obj)
        {

            if (UserName.Length == 0 || UserPatronymic.Length == 0 || UserSurname.Length == 0 || eMail.Length == 0)
            {
                MessageBox.Show("Не вернные данные.");
                return;
            }

            BaseUser user = new BaseUser()
            {
                Name = UserName,
                Surname = UserSurname,
                Patronymic = UserPatronymic
            };

            Регистрация regUser = new Регистрация()
            {
                Логин = Email, Пароль = Password, Роли = (int)SelectedUserRole
            };

            var sdf = _registrationService.RegisterNewUser(regUser, user);

            NewUser = sdf;
            RegistrationStatus = true;

            CloseWindow(true);
        }

        #region Свойства

        public bool RegistrationStatus { get; set; }
        public Регистрация NewUser { get; set; }


        private string userName;
        public string UserName
        {
            get => userName;
            set
            {
                userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }


        private string userSurname;
        public string UserSurname
        {
            get => userSurname;
            set
            {
                userSurname = value;
                OnPropertyChanged(nameof(UserSurname));
            }
        }

        private string userPatronymic;

        public string UserPatronymic
        {
            get => userPatronymic;
            set
            {
                userPatronymic = value;
                OnPropertyChanged(nameof(UserPatronymic));
            }
        }

        private string eMail;

        public string Email
        {
            get => eMail;
            set
            {
                // до установки свойства можно сделать проверку на правильность занчений
                eMail = value;
                //оповещаем что свойство изменилось
                OnPropertyChanged(nameof(Email));
            }
        }

        //увы к контролу пароля прибиндиться нельзя по умолчанию
        private string password;

        public string Password
        {
            get => GetPasswordValue?.Invoke() ?? string.Empty;
            set => password = value;
        }

        private UserRole selectedRole;

        public UserRole SelectedUserRole
        {
            get => selectedRole;
            set
            {
                selectedRole = value;
                OnPropertyChanged(nameof(SelectedUserRole));
            }
        }

        //инициализация происходит при создании объекта
        public List<string> UserRoles { get; set; } = EnumHelper.RoleTypeList;

        //получение значения прямо из контролла
        public Func<string> GetPasswordValue;

        #endregion


        #region Команды

        private RelayCommand registerCommand;

        public RelayCommand RegisterCommand => registerCommand ?? (registerCommand = new RelayCommand(OnRegister));

        #endregion
    }
}