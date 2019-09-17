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

namespace Курсовая.AddForm
{
    public class AddStudentViewModel : BaseViewModel
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IStudentsRepository _studentsRepository;


        private string userName;
        public string UserName
        {
            get => userName;
            set
            {
                userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }


        private string userSurname;
        public string UserSurname
        {
            get => userSurname;
            set
            {
                userSurname = value;
                OnPropertyChanged(nameof(UserSurname));
            }
        }

        private string userPatronymic;

        public string UserPatronymic
        {
            get => userPatronymic;
            set
            {
                userPatronymic = value;
                OnPropertyChanged(nameof(UserPatronymic));
            }
        }


        public List<string> SexTypes { get; set; } = new List<string>(EnumHelper.SexTypeList);

        private Sex _selectedSex;
        public Sex Sex
        {
            get { return _selectedSex; }
            set
            {
                _selectedSex = value;
                OnPropertyChanged(nameof(Sex));
            }
        }

        public List<string> Specialtys { get; set; } = new List<string>(EnumHelper.SpecTypeList);

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

        private ObservableCollection<GroupDto> _groups;

        public ObservableCollection<GroupDto> Groups
        {
            get { return _groups; }
            set
            {
                _groups = value;
                OnPropertyChanged(nameof(Groups));
            }
        }

        private GroupDto _selectedGroupDto;

        public GroupDto SelectedGroup
        {
            get { return _selectedGroupDto; }
            set
            {
                _selectedGroupDto = value;
                OnPropertyChanged(nameof(SelectedGroup));
            }
        }


        private RelayCommand _newGroupCommand;
        public RelayCommand NewGroupCommand => _newGroupCommand ?? (_newGroupCommand = new RelayCommand(OnNewGroup));

        private void OnNewGroup(object obj)
        {
            OpenDialogWindow<AddGroupWindow>((window) =>
            {
                AddGroupViewModel viewModel = new AddGroupViewModel();
                viewModel.RequestClose += b => window.Close();
                window.DataContext = viewModel;
            });

            UpdateGroups();
        }

        private RelayCommand _saveCommand;
        public RelayCommand SaveCommand => _saveCommand ?? (_saveCommand = new RelayCommand(OnSave));

        private void OnSave(object obj)
        {
            Учащиеся student = new Учащиеся()
            {
                ID_группы = SelectedGroup.Id, Пол = (int)Sex, Имя_Учащегося = UserName, Отчество_Учащегося = UserPatronymic, Фамилия_Учащегося = UserSurname
            };

            _studentsRepository.Create(student);
            CloseWindow(true);
        }


        public AddStudentViewModel()
        {
            _groupRepository = new GroupRepository(App.DataBaseContext);
            _studentsRepository = new StudentsRepository(App.DataBaseContext);

            UpdateGroups();

        }

        private void UpdateGroups()
        {
            Groups = new ObservableCollection<GroupDto>(DtoHelper.ConvertToGroupDtos(_groupRepository.FindAll().ToList()));
            if (_selectedGroupDto != null)
            {
                SelectedGroup = Groups.FirstOrDefault(item => item.Id == _selectedGroupDto.Id);
            }
        }
    }
}
