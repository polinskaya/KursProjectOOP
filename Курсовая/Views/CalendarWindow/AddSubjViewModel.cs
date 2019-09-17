using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using NavigationDrawerPopUpMenu2;
using Курсовая.Common;
using Курсовая.DataBase;
using Курсовая.DataBase.Repositories;
using Курсовая.DataBase.RepositoryInterfaces;
using Курсовая.Views.UiCommon.Dto;

namespace Курсовая.Views.CalendarWindow
{
    public class AddSubjViewModel : BaseViewModel
    {

        private readonly ISubjectsRepository _subjectsRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IRoomsRepository _roomsRepository;
        private readonly IClassesRepository _classesRepository;



        private ObservableCollection<SubjectDto> _subjects;
        public ObservableCollection<SubjectDto> Subjects
        {
            get { return _subjects; }
            set
            {
                _subjects = value;
                OnPropertyChanged(nameof(Subjects));
            }
        }

        private SubjectDto _selctedSubjectDto;

        public SubjectDto SelectedSubject
        {
            get { return _selctedSubjectDto; }
            set
            {
                _selctedSubjectDto = value;
                OnPropertyChanged(nameof(SelectedSubject));
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

        private GroupDto _selectedGoup;

        public GroupDto SelectedGroup
        {
            get { return _selectedGoup; }
            set
            {
                _selectedGoup = value;
                OnPropertyChanged(nameof(SelectedGroup));
            }
        }

        private ObservableCollection<RoomDto> _rooms;
        public ObservableCollection<RoomDto> Rooms
        {
            get { return _rooms; }
            set
            {
                _rooms = value;
                OnPropertyChanged(nameof(Rooms));
            }
        }

        private RoomDto _selectedRoom;

        public RoomDto SelectedRoom
        {
            get { return _selectedRoom; }
            set
            {
                _selectedRoom = value;
                OnPropertyChanged(nameof(SelectedRoom));
            }
        }


        public List<string> Weeks { get; set; } = EnumHelper.weekTypeList;
        private DateTime dateTime;

        public DateTime DateTime
        {
            get { return dateTime; }
            set
            {
                dateTime = value;
                OnPropertyChanged(nameof(DateTime));
            }
        }

        public string SelctedWeek { get; set; }

        private RelayCommand _saveCommand;
        public RelayCommand SaveCommand => _saveCommand ?? (_saveCommand = new RelayCommand(OnSave));

        private void OnSave(object obj)
        {
            if (SelectedGroup == null || SelectedSubject == null)
            {
                MessageBox.Show("Нужно все быбрать.");
                return;
            }

            _classesRepository.Create(new Занятие()
            {
                C_ID_Расписания = SchID, ID_Группы = SelectedGroup.Id, ID_Дисциплины = SelectedSubject.Id,
                ДатаИВремя = DateTime, Неделя = EnumHelper.weekTypeList.IndexOf(SelctedWeek)
            });

            CloseWindow(true);
        }


        private int SchID;
        public AddSubjViewModel(int schId)
        {
            SchID = schId;
            _groupRepository = new GroupRepository(App.DataBaseContext);
            _classesRepository =new ClassesRepository(App.DataBaseContext);
            _roomsRepository = new RoomsRepository(App.DataBaseContext);
            _subjectsRepository = new SubjectRepository(App.DataBaseContext);

            Subjects = new ObservableCollection<SubjectDto>(DtoHelper.ConvertToSubjectDtos(_subjectsRepository.FindAll().ToList()));
            Groups = new ObservableCollection<GroupDto>(DtoHelper.ConvertToGroupDtos(_groupRepository.FindAll().ToList()));
            Rooms = new ObservableCollection<RoomDto>();
            DateTime =  DateTime.Today;
        }
    }
}
