using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NavigationDrawerPopUpMenu2;
using Курсовая.Common;
using Курсовая.DataBase;
using Курсовая.DataBase.Repositories;
using Курсовая.DataBase.RepositoryInterfaces;
using Курсовая.Views.UiCommon.Dto;

namespace Курсовая.Views.UserWindow
{
    public class UserViewModel : BaseViewModel
    {
        private ITeachersRepository _teachersRepository;
        private IStudentsRepository _studentsRepository;
        private IDepartmentRepository _departmentRepository;
        private IRegistrationRepository _registrationRepository;

        private Регистрация CurrentUser;
        private Преподаватели Teacher;
        private Учащиеся Student;

        #region Props

        public String UserName
        {
            get
            {
                if (CurrentUser.Роли == (int)UserRole.Teacher)
                {
                    return Teacher.Имя_преподавателя;
                }
                else
                {
                    return Student.Имя_Учащегося;
                }
            }
            set
            {
                if (CurrentUser.Роли == (int)UserRole.Teacher)
                {
                    Teacher.Имя_преподавателя = value;
                }
                else
                {
                    Student.Имя_Учащегося = value;
                }
                OnPropertyChanged(nameof(UserName));
            }
        }

        public string UserSurname
        {
            get
            {
                if (CurrentUser.Роли == (int)UserRole.Teacher)
                {
                    return Teacher.Фамилия_преподавателя;
                }
                else
                {
                    return Student.Фамилия_Учащегося;
                }
            }
            set
            {
                if (CurrentUser.Роли == (int)UserRole.Teacher)
                {
                    Teacher.Фамилия_преподавателя = value;
                }
                else
                {
                    Student.Фамилия_Учащегося = value;
                }
                OnPropertyChanged(nameof(UserSurname));
            }
        }

        public string UserPatronymic
        {
            get
            {
                if (CurrentUser.Роли == (int)UserRole.Teacher)
                {
                    return Teacher.Отчество_преподавателя;
                }
                else
                {
                    return Student.Отчество_Учащегося;
                }
            }
            set
            {
                if (CurrentUser.Роли == (int)UserRole.Teacher)
                {
                    Teacher.Отчество_преподавателя = value;
                }
                else
                {
                    Student.Отчество_Учащегося = value;
                }
                OnPropertyChanged(nameof(UserPatronymic));
            }
        }

        public string UserPassword
        {
            get { return CurrentUser.Пароль; }
            set
            {
                CurrentUser.Пароль = value;
                OnPropertyChanged(nameof(UserPassword));
            }
        }

        public Sex Sex
        {
            get
            {
                if (CurrentUser.Роли == (int)UserRole.Teacher)
                {
                    return Teacher.Пол == null ? Sex.Male : (Sex)Teacher.Пол;
                }
                else
                {
                    return Student.Пол == null ? Sex.Male : (Sex)Student.Пол;
                }
            }
            set
            {
                if (CurrentUser.Роли == (int)UserRole.Teacher)
                {
                    Teacher.Пол = (int)value;
                }
                else
                {
                    Student.Пол = (int)value;
                }
                OnPropertyChanged(nameof(Sex));
            }
        }

        public string Email
        {
            get
            {
                return CurrentUser.Логин;
            }
            set
            {
                CurrentUser.Логин = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        private ObservableCollection<DepartmentDto> _departments;
        public ObservableCollection<DepartmentDto> Departments
        {
            get { return _departments; }
            set
            {
                _departments = value;
                OnPropertyChanged(nameof(Departments));
            }
        }

        private DepartmentDto _selectedDepartment;
        public DepartmentDto SelectedDepartment
        {
            get { return _selectedDepartment; }
            set
            {

                _selectedDepartment = value;
                if (Teacher != null && value != null)
                {
                    Teacher.Код_кафедры = value.ShortName;
                }
                
                OnPropertyChanged(nameof(SelectedDepartment));
            }
        }


        public bool SetPasswordProp(string password)
        {
            //проверка на правильность пароля
            //если что-то не так то возвращаем False

            UserPassword = password;
            return true;
        }

        public Func<string> GetPasswordValue;
        public Action<string> SetPassword;

        public ObservableCollection<string> SexList { get; set; } = new ObservableCollection<string>(EnumHelper.SexTypeList);

        #endregion


        #region Commands

        private RelayCommand _newDepartmentCommand;
        public RelayCommand NewDepartmentCommand => _newDepartmentCommand ?? (_newDepartmentCommand = new RelayCommand(OnNewDepartment));

        private RelayCommand _saveCommand;
        public RelayCommand SaveCommand => _saveCommand ?? (_saveCommand = new RelayCommand(OnSave));





        private void OnNewDepartment(object obj)
        {
            OpenDialogWindow<NewDepartmentWindow>((window) =>
            {
                NewDepartmentViewModel viewModel = new NewDepartmentViewModel();
                viewModel.RequestClose += result => window.Close();
                window.DataContext = viewModel;
            });

            Departments = new ObservableCollection<DepartmentDto>(DtoHelper.ConvertToDepartmentDtos(_departmentRepository.FindAll().ToList()));
            if (_selectedDepartment != null)
            {
                SelectedDepartment = Departments.FirstOrDefault(item => item.ShortName.Equals(_selectedDepartment.ShortName));
            }
        }

        private void OnSave(object obj)
        {
            _registrationRepository.Update(CurrentUser);
            if (CurrentUser.Роли == (int) UserRole.Teacher)
            {
                _teachersRepository.Update(Teacher);
            }
        }

        #endregion

        public UserViewModel(Регистрация currentUser)
        {
            CurrentUser = currentUser;
            _teachersRepository = new TeachersRepository(App.DataBaseContext);
            _departmentRepository = new DepartmentRepository(App.DataBaseContext);
            _studentsRepository = new StudentsRepository(App.DataBaseContext);
            _registrationRepository = new RegistrationRepository(App.DataBaseContext);

            Departments = new ObservableCollection<DepartmentDto>(DtoHelper.ConvertToDepartmentDtos(_departmentRepository.FindAll().ToList()));

            if (CurrentUser.Роли == (int) UserRole.Teacher)
            {
                Teacher = _teachersRepository.FindById(CurrentUser.Ид_пользователя.Value);
                SelectedDepartment = Departments.FirstOrDefault(item => item.ShortName.Equals(Teacher.Код_кафедры));
            }
            else
            {
                Student = _studentsRepository.FindById(CurrentUser.Ид_пользователя.Value);
            }

            
        }

        public void InitControl()
        {
            SetPassword?.Invoke(CurrentUser.Пароль);
        }
    }
}
