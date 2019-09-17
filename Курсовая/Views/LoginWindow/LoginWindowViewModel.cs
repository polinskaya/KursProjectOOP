using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Курсовая.DataBase;
using Курсовая.Services;
using Курсовая.Views.MainVindow;

namespace Курсовая
{
    public class LoginWindowViewModel : BaseViewModel
    {
        private RegistrationService _registrationService;

        public bool LoginResult { get; set; }
        public Регистрация LoggedUser { get; set; }
        public Регистрация NewUser { get; set; }

        public bool NeedAdditionalInformation { get; set; }

        public LoginWindowViewModel(RegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        //логика входа
        private void OnLogin(object obj)
        {
            //Если все проверки прошли и мы успешно все записали в базу, возвращаем true
            Регистрация userLogin = new Регистрация()
            {
                Логин = Email, Пароль = Password
            };
            var result = _registrationService.CheckLogin(userLogin);
            if (result != null)
            {
                LoggedUser = result;

                NeedAdditionalInformation = NewUser != null && LoggedUser.ID_регистрации == NewUser.ID_регистрации;
                LoginResult = true;

                OpenDialogWindow<MainWindow>(window =>
                {
                    MainWindowViewModel viewModel = new MainWindowViewModel(LoggedUser, NeedAdditionalInformation);
                    window.DataContext = viewModel;
                });

                CloseWindow(true);
                return;
            }
            MessageBox.Show($"Логин или пароль не верные.");
        }

        private void OnRegister(object obj)
        {
            var registrationViewModel = new WindowRegistrViewModel();
            var owner = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
            var dialogResult = OpenDialogWindow<RegistrationWindow>(window =>
            {
                window.Owner = owner;
                
                registrationViewModel.RequestClose += result =>
                {
                    ShowWindow();
                    window.Close();
                };
                registrationViewModel.RequsetHideParent += HideWindow;
                registrationViewModel.GetPasswordValue = window.GetPassWordValue;
                window.SetPassword = registrationViewModel.SetPasswordProp;
                window.DataContext = registrationViewModel;
            });
            
            if (registrationViewModel.RegistrationStatus)
            {
                NewUser = registrationViewModel.NewUser;
            }
        }

        #region Свойства

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

        //получение значения прямо из контролла
        public Func<string> GetPasswordValue;

        public bool SetPasswordProp(string password)
        {
            //проверка на правильность пароля
            //если что-то не так то возвращаем False

            Password = password;
            return true;
        }

        #endregion

        #region Команды
        //команда логина
        private RelayCommand loginCommand;

        public RelayCommand LoginCommand => loginCommand ?? (loginCommand = new RelayCommand(OnLogin));

        private RelayCommand registerCommand;

        public RelayCommand RegisterCommand => registerCommand ?? (registerCommand = new RelayCommand(OnRegister));

        #endregion
    }

    //перечисление для ролей

}