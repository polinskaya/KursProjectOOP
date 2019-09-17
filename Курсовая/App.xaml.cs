using System.Windows;
using MaterialDesignThemes.Wpf;
using Курсовая;
using Курсовая.DataBase;
using Курсовая.DataBase.Repositories;
using Курсовая.Services;

namespace NavigationDrawerPopUpMenu2
{
    /// <summary>
    ///     Interação lógica para App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static DataBaseEntities DataBaseContext = new DataBaseEntities();
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            
            var loginWindow = new LoginWindow(); //создание объекта окна
            var loginWindowViewModel = new LoginWindowViewModel(new RegistrationService(new RegistrationRepository(DataBaseContext), new StudentsRepository(DataBaseContext), new TeachersRepository(DataBaseContext))); // создание оъбекта ViewModel для окна логина\
            loginWindowViewModel.GetPasswordValue = loginWindow.GetPassWordValue; // устанавливаем метод получения пароля во ViewModel
            loginWindowViewModel.RequestClose += result =>
            {
                loginWindow.Close();
            };
            loginWindowViewModel.RequestShow += () =>
            {
                loginWindow.WindowState = loginWindowViewModel.PrevWindowState;
                loginWindow.ShowInTaskbar = true;
            };
            
            loginWindowViewModel.RequestHide += () =>
            {
                loginWindowViewModel.PrevWindowState = loginWindow.WindowState;
                loginWindow.WindowState = WindowState.Minimized;
                loginWindow.ShowInTaskbar = false;
            };
            loginWindow.SetPassword = loginWindowViewModel.SetPasswordProp; //Соеденили c ViewModel
            //установка контекста желательно должна идти после того как все объекты инициализироанны
            loginWindow.DataContext = loginWindowViewModel; // Устанавливаем контекст для биндинга свойств и команд 
            loginWindow.ShowDialog(); // вызываем окно как диалог для получения результата логина

            if (loginWindowViewModel.LoginResult)
            {
                
                //открываем новое главное окошко со всеми приблудами
                //для дополнения информации надо будет установить нужный флаг loginWindowViewModel.NeedAdditionalInformation
                //во ViewModel главного окна
            }
            else
            {
                //выход
                return;
            }
        }
    }
}