using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NavigationDrawerPopUpMenu2;
using Курсовая.Common;
using Курсовая.DataBase;
using Курсовая.DataBase.Repositories;
using Курсовая.DataBase.RepositoryInterfaces;

namespace Курсовая.Views.MainVindow
{
    public class MainWindowViewModel : BaseViewModel
    {

        private ITeachersRepository _teachersRepository;

        public Регистрация CurrentUser;
        public bool IsTeacher => (UserRole) ((int) CurrentUser.Роли) == UserRole.Teacher;
        public bool IsNewUser { get; set; }

        public Преподаватели GuiTeacher { get; set; }

        public Учащиеся GuiStudent { get; set; }

        


        public MainWindowViewModel(Регистрация currentUser, bool newUser)
        {
            CurrentUser = currentUser;
            IsNewUser = newUser;
            _teachersRepository = new TeachersRepository(App.DataBaseContext);
            GuiTeacher = _teachersRepository.FindById(CurrentUser.Ид_пользователя.Value);
        }
    }
}
