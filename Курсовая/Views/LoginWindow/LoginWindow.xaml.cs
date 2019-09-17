using System;
using System.Windows;

namespace Курсовая
{
    /// <summary>
    ///     Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public Func<string, bool> SetPassword;

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Password_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            SetPassword?.Invoke(password.Password); //Устанавливаем знаечение свойства во ViewModel
        }

        public string GetPassWordValue()
        {
            return password.Password;
        }
    }
}