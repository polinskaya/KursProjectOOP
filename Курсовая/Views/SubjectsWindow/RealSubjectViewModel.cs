using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NavigationDrawerPopUpMenu2;
using Курсовая.DataBase;
using Курсовая.DataBase.Repositories;
using Курсовая.DataBase.RepositoryInterfaces;
using Курсовая.Views.UiCommon.Dto;

namespace Курсовая.Views.SubjectsWindow
{
    public class RealSubjectViewModel :BaseViewModel
    {
        private readonly ISubjectsRepository _subjectsRepository;
        private readonly IDepartmentRepository _departmentRepository;


        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private ObservableCollection<DepartmentDto> _departmentDtos;

        public ObservableCollection<DepartmentDto> DepartmentDtos
        {
            get { return _departmentDtos; }
            set
            {
                _departmentDtos = value;
                OnPropertyChanged(nameof(DepartmentDtos));
            }
        }

        private DepartmentDto _selectedDepartment;

        public DepartmentDto SelectedDepartmentDto
        {
            get { return _selectedDepartment; }
            set
            {
                _selectedDepartment = value;
                OnPropertyChanged(nameof(SelectedDepartmentDto));
            }
        }


        private RelayCommand _saveCommand;
        public RelayCommand SaveCommand => _saveCommand ?? (_saveCommand = new RelayCommand(OnSave));

        private void OnSave(object obj)
        {
            Дисциплины newd = new Дисциплины()
            {
                Название_дисциплины = Name, Код_кафедры = SelectedDepartmentDto.ShortName
            };

            _subjectsRepository.Create(newd);
            CloseWindow(true);
        }

        public RealSubjectViewModel()
        {
            _subjectsRepository = new SubjectRepository(App.DataBaseContext);
            _departmentRepository = new DepartmentRepository(App.DataBaseContext);
            DepartmentDtos = new ObservableCollection<DepartmentDto>(DtoHelper.ConvertToDepartmentDtos(_departmentRepository.FindAll().ToList()));
        }
    }
}
