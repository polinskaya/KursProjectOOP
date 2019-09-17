using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Курсовая.Common;

namespace Курсовая.Views.UserWindow
{
    /// <summary>
    ///     Логика взаимодействия для UserControlUser.xaml
    /// </summary>
    public partial class UserControlUser : UserControl
    {
        public Func<string, bool> SetPassword;

        public UserControlUser()
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

        public void SetPasswordValue(string password)
        {
            this.password.Password = password;
        }
    }

    public class SexTypeConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? null : EnumHelper.SexTypeList[(int)value];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var strStatus = value as string;
            var index = EnumHelper.SexTypeList?.IndexOf(strStatus);
            if (index != null)
            {
                return (Sex)index;
            }

            return Sex.Male;
        }
    }
}