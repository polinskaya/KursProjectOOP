using System.Windows;
using System.Windows.Controls;
using Курсовая.Views.CalendarWindow;
using Курсовая.Views.GroupWindow;
using Курсовая.Views.MainVindow;
using Курсовая.Views.SubjectsWindow;
using Курсовая.Views.UserWindow;

namespace Курсовая
{
    /// <summary>
    ///     Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ListViewMenu.SelectedItem = ListViewMenu.Items[0];
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Visible;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            ButtonOpenMenu.Visibility = Visibility.Visible;
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserControl usc = null;
            GridMain.Children.Clear();
            
            switch (((ListViewItem) ((ListView) sender).SelectedItem).Name)
            {
                case "User":
                    UserControlUser userControl = new UserControlUser();
                    UserViewModel userViewModel = new UserViewModel(((MainWindowViewModel)DataContext).CurrentUser);
                    userViewModel.GetPasswordValue = userControl.GetPassWordValue;
                    userViewModel.SetPassword = userControl.SetPasswordValue;
                    userControl.SetPassword = userViewModel.SetPasswordProp;
                    userControl.DataContext = userViewModel;
                    userControl.Loaded += (o, args) => userViewModel.InitControl();
                    GridMain.Children.Add(userControl);
                    break;
                case "People":
                    UserControlPeople peopleControl = new UserControlPeople();
                    GroupWindowViewModel viewModel = new GroupWindowViewModel();
                    peopleControl.DataContext = viewModel;
                    GridMain.Children.Add(peopleControl);
                    break;
                case "Book":
                    var subjUserControl = new UserControlBook();
                    subjUserControl.DataContext = new SubjectWindowViewModel();
                    GridMain.Children.Add(subjUserControl);
                    break;
                case "CheckboxesMarked":
                    usc = new UserControlCheckboxesMarked();
                    GridMain.Children.Add(usc);
                    break;
                case "Number5Box":
                    usc = new UserControlNumber5Box();
                    GridMain.Children.Add(usc);
                    break;
                case "Calendars":
                    usc = new UserControlCalendars();
                    usc.DataContext = new CalendarViewModel(((MainWindowViewModel)DataContext).GuiTeacher);
                    GridMain.Children.Add(usc);
                    break;
            }
        }
    }
}