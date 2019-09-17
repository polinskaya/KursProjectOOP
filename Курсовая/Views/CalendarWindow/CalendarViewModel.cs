using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using NavigationDrawerPopUpMenu2;
using Курсовая.DataBase;
using Курсовая.DataBase.Repositories;
using Курсовая.DataBase.RepositoryInterfaces;
using Курсовая.Views.UiCommon.Dto;

namespace Курсовая.Views.CalendarWindow
{
    public class CalendarViewModel : BaseViewModel
    {

        private readonly Преподаватели CurrentTeacher;
        private readonly Расписание schedObject;


        private readonly ITeachersScheduleRepository _teachersScheduleRepository;
        private readonly ITeachersJournalRepository _teachersJournalRepository;
        private readonly IClassesRepository _classesRepository;


        private ObservableCollection<ClassDto> _classes;

        public ObservableCollection<ClassDto> Classes
        {
            get { return _classes; }
            set
            {
                _classes = value;
                OnPropertyChanged(nameof(Classes));
            }
        }

        private ClassDto _selctedClassDto;

        public ClassDto SelectedClass
        {
            get
            {
                return _selctedClassDto;
            }
            set
            {
                _selctedClassDto = value;
                OnPropertyChanged(nameof(SelectedClass));
            }
        }


        private RelayCommand _addClassCommand;
        public RelayCommand AddClassCommand => _addClassCommand ?? (_addClassCommand = new RelayCommand(OnAddClass));

        private RelayCommand _removeClassCommand;
        public RelayCommand RemoveClassCommand => _removeClassCommand ?? (_removeClassCommand = new RelayCommand(OnRemoveClass));

        private void OnRemoveClass(object obj)
        {
            if (SelectedClass == null)
            {
                MessageBox.Show("Надо выбрать занятие.");
                return;
            }

            _classesRepository.Remove(_classesRepository.FindById(SelectedClass.Id));
            Classes = new ObservableCollection<ClassDto>(DtoHelper.ConvertToClassDtos(_classesRepository.FindAll(item => item.C_ID_Расписания.Equals(schedObject.ID_Расписания)).ToList()));
        }

        private void OnAddClass(object obj)
        {
            OpenDialogWindow<SubjAdd>(window =>
            {
                AddSubjViewModel viewModel = new AddSubjViewModel(schedObject.ID_Расписания);
                viewModel.RequestClose += b => window.Close();
                window.DataContext = viewModel;
            });

            Classes = new ObservableCollection<ClassDto>(DtoHelper.ConvertToClassDtos(_classesRepository.FindAll(item => item.C_ID_Расписания.Equals(schedObject.ID_Расписания)).ToList()));
        }


        public CalendarViewModel(Преподаватели currentTeacher)
        {
            CurrentTeacher = currentTeacher;
            _teachersScheduleRepository = new TeachersScheduleRepository(App.DataBaseContext);
            _teachersJournalRepository = new TeachersJournalRepository(App.DataBaseContext);
            _classesRepository = new ClassesRepository(App.DataBaseContext);

            var sdf = _teachersScheduleRepository.FindAll().ToList();
            if (sdf.Count == 0)
            {
                schedObject = _teachersScheduleRepository.Create(new Расписание() {ID_Преподавателя = CurrentTeacher.ID_преподавателя});
            }
            else
            {
                schedObject = sdf.First();
            }

            Classes = new ObservableCollection<ClassDto>(DtoHelper.ConvertToClassDtos(_classesRepository.FindAll(item=>item.C_ID_Расписания.Equals(schedObject.ID_Расписания)).ToList()));
        }

    }
}
