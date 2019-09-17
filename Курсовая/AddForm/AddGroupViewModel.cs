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
using Курсовая.Views.UserWindow;

namespace Курсовая.AddForm
{
    public class AddGroupViewModel : BaseViewModel
    {
        private readonly IFacultyRepository _facultyRepository;
        private readonly IGroupRepository _groupRepository;



        public List<string> GroupNumbers { get; set; } = new List<string>(EnumHelper.GroupNumbersTypeList);
        public List<string> GroupTypes { get; set; } = new List<string>(EnumHelper.GroupTypeList);
        public List<string> Specialtys { get; set; } = new List<string>(EnumHelper.SpecTypeList);

        private string _selectedGroupNumber;
        public string SelectedGroupNumber
        {
            get { return _selectedGroupNumber; }
            set
            {
                _selectedGroupNumber = value;
                OnPropertyChanged(nameof(SelectedGroupNumber));
            }
        }

        private GroupType _selectedGroupType;
        public GroupType SelectedGroupType
        {
            get { return _selectedGroupType; }
            set
            {
                _selectedGroupType = value;
                OnPropertyChanged(nameof(SelectedGroupType));
            }
        }

        private string _selectedSpec;
        public string SelectedSpec
        {
            get { return _selectedSpec; }
            set
            {
                _selectedSpec = value;
                OnPropertyChanged(nameof(SelectedSpec));
            }
        }

        private ObservableCollection<FacultyDto> _facultyDtos;
        public ObservableCollection<FacultyDto> FacultyDtos
        {
            get { return _facultyDtos; }
            set
            {
                _facultyDtos = value;
                OnPropertyChanged(nameof(FacultyDtos));
            }
        }

        private FacultyDto _selectedFacultyDto;
        public FacultyDto SelectedFaculty
        {
            get { return _selectedFacultyDto; }
            set
            {
                _selectedFacultyDto = value;
                OnPropertyChanged(nameof(SelectedFaculty));
            }
        }

        private DateTime _selectedStartDateTime;
        public DateTime SelectedStartDateTime
        {
            get { return _selectedStartDateTime; }
            set
            {
                _selectedStartDateTime = value;
                OnPropertyChanged(nameof(SelectedStartDateTime));
            }
        }

        private RelayCommand _addFaculty;
        public RelayCommand AddFaculty => _addFaculty ?? (_addFaculty = new RelayCommand(OnAddFaculty));

        private void OnAddFaculty(object obj)
        {
            OpenDialogWindow<NewFacultyWindow>(window =>
            {
                NewFacultyViewMode viewMode = new NewFacultyViewMode();
                viewMode.RequestClose += b => window.Close();
                window.DataContext = viewMode;
            });
            FacultyDtos = new ObservableCollection<FacultyDto>(DtoHelper.ConvertToFacultyDtos(_facultyRepository.FindAll().ToList()));
            UpdateFaculties();
        }


        private RelayCommand _saveCommandFaculty;
        public RelayCommand SaveCommand => _saveCommandFaculty ?? (_saveCommandFaculty = new RelayCommand(OnSave));

        private void OnSave(object obj)
        {
            Группы NewGroup = new Группы()
            {
                Код_специальности = SelectedSpec, Код_факультета = SelectedFaculty.ShortName, Номер_группы = SelectedGroupNumber,
                Тип = (int)SelectedGroupType, Год_поступления = SelectedStartDateTime.Ticks
            };

            _groupRepository.Create(NewGroup);

            CloseWindow(true);
        }

        public AddGroupViewModel()
        {
            _facultyRepository = new FacultyRepository(App.DataBaseContext);
            _groupRepository = new GroupRepository(App.DataBaseContext);
            UpdateFaculties();
            SelectedStartDateTime = DateTime.Today;
        }

        private void UpdateFaculties()
        {
            FacultyDtos = new ObservableCollection<FacultyDto>(DtoHelper.ConvertToFacultyDtos(_facultyRepository.FindAll().ToList()));
            if (_selectedFacultyDto != null)
            {
                SelectedFaculty = FacultyDtos.FirstOrDefault(item => item.ShortName.Equals(_selectedFacultyDto.ShortName));
            }
        }

    }
}
