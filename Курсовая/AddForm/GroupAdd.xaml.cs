using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Курсовая.Common;

namespace Курсовая.AddForm
{
    /// <summary>
    /// Логика взаимодействия для AddGroupWindow.xaml
    /// </summary>
    public partial class AddGroupWindow : Window
    {
        public AddGroupWindow()
        {
            InitializeComponent();
        }
    }

    public class GroupTypeConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? null : EnumHelper.GroupTypeList[(int)value];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var strStatus = value as string;
            var index = EnumHelper.GroupTypeList?.IndexOf(strStatus);
            if (index != null)
            {
                return (GroupType)index;
            }

            return GroupType.FullTime;
        }
    }
}
