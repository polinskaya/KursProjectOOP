using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using NavigationDrawerPopUpMenu2;
using Курсовая.DataBase;
using Курсовая.DataBase.Repositories;
using Курсовая.DataBase.RepositoryInterfaces;
using Курсовая.Views.UiCommon.Dto;

namespace Курсовая.Views.UserWindow
{
    public class NewDepartmentViewModel : BaseViewModel
    {

        private readonly IDepartmentRepository _departmentRepository;
        private readonly IFacultyRepository _facultyRepository;

        private string _shortName;
        public string ShortName
        {
            get { return _shortName; }
            set
            {
                _shortName = value;
                OnPropertyChanged(nameof(ShortName));
            }
        }

        private string _fullName;
        public string FullName
        {
            get { return _fullName; }
            set
            {
                _fullName = value;
                OnPropertyChanged(nameof(FullName));
            }
        }

        private ObservableCollection<FacultyDto> _faculties;
        public ObservableCollection<FacultyDto> Facultieas
        {
            get { return _faculties; }
            set
            {
                _faculties = value;
                OnPropertyChanged(nameof(Facultieas));
            }
        }

        private FacultyDto _selectedFaculty;
        public FacultyDto SelectedFaculty
        {
            get { return _selectedFaculty; }
            set
            {
                _selectedFaculty = value;
                OnPropertyChanged(nameof(SelectedFaculty));
            }
        }

        private RelayCommand _saveCommand;
        public RelayCommand SaveCommand => _saveCommand ?? (_saveCommand = new RelayCommand(OnSave));

        private RelayCommand _newFacultyCommand;
        public RelayCommand NewFacultyCommand => _newFacultyCommand ?? (_newFacultyCommand = new RelayCommand(OnNewFaculty));

        private void OnSave(object obj)
        {
            Кафедры newDep = new Кафедры()
            {
                Код_кафедры = ShortName,
                Наименование_кафедры = FullName,
                Код_факультета = SelectedFaculty.ShortName
            };

            if (_departmentRepository.FindAll().Any(item => item.Код_кафедры.Equals(newDep.Код_кафедры) && item.Код_факультета.Equals(newDep.Код_факультета)))
            {
                MessageBox.Show($"Такая кафедра уже есть в факультете {SelectedFaculty.ShortName}.");
                return;
            }

            _departmentRepository.Create(newDep);
            CloseWindow(true);
        }

        private void OnNewFaculty(object obj)
        {
            OpenDialogWindow<NewFacultyWindow>(window =>
            {
                NewFacultyViewMode viewMode = new NewFacultyViewMode();
                viewMode.RequestClose += b => window.Close();
                window.DataContext = viewMode;
            });
            Facultieas = new ObservableCollection<FacultyDto>(DtoHelper.ConvertToFacultyDtos(_facultyRepository.FindAll().ToList()));
        }


        public NewDepartmentViewModel()
        {
            _departmentRepository = new DepartmentRepository(App.DataBaseContext);
            _facultyRepository = new FacultyRepository(App.DataBaseContext);

            Facultieas = new ObservableCollection<FacultyDto>(DtoHelper.ConvertToFacultyDtos(_facultyRepository.FindAll().ToList()));
        }
    }
}
