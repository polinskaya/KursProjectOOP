using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Курсовая.Common;

namespace Курсовая
{
    /// <summary>
    ///     Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public Func<string, bool> SetPassword;
        public Func<string, bool> SetPasswordConform;

        public RegistrationWindow()
        {
            InitializeComponent();
            Loaded += WindowRegistr_Loaded;
        }

        private void WindowRegistr_Loaded(object sender, RoutedEventArgs e)
        {
//            ((WindowRegistrViewModel) DataContext).HideParentWindow();
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

    public class RoleTypeConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? null : EnumHelper.RoleTypeList[(int)value];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var strStatus = value as string;
            var index = EnumHelper.RoleTypeList?.IndexOf(strStatus);
            if (index != null)
            {
                return (UserRole)index;
            }

            return UserRole.Student;
        }
    }
}