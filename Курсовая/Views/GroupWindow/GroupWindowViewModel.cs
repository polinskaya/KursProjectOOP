using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using NavigationDrawerPopUpMenu2;
using Курсовая.AddForm;
using Курсовая.Common;
using Курсовая.DataBase;
using Курсовая.DataBase.Repositories;
using Курсовая.DataBase.RepositoryInterfaces;
using Курсовая.Views.UiCommon.Dto;
using Application = Microsoft.Office.Interop.Excel.Application;
using MessageBox = System.Windows.MessageBox;

namespace Курсовая.Views.GroupWindow
{
    public class GroupWindowViewModel : BaseViewModel
    {
        private readonly IStudentsRepository _studentsRepository;
        private readonly IGroupRepository _groupRepository;



        #region Props

        private ObservableCollection<StudentDto> _selectedGroupStudentDtos;
        public ObservableCollection<StudentDto> SelectedGroupStudentDtos
        {
            get { return _selectedGroupStudentDtos; }
            set
            {
                _selectedGroupStudentDtos = value;
                OnPropertyChanged(nameof(SelectedGroupStudentDtos));
            }
        }

        private StudentDto _selectedStudentDto;

        public StudentDto SelectedStudent
        {
            get { return _selectedStudentDto; }
            set
            {
                _selectedStudentDto = value;
                OnPropertyChanged(nameof(SelectedStudent));
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

        private GroupDto _selectedGroup;

        public GroupDto SelectedGroup
        {
            get { return _selectedGroup; }
            set
            {
                _selectedGroup = value;

                SelectedGroupStudentDtos = new ObservableCollection<StudentDto>(DtoHelper.ConvertToStudentDtos(_studentsRepository.FindAll(item => item.ID_группы == _selectedGroup.Id).ToList()));
                OnPropertyChanged(nameof(SelectedGroup));
            }
        }

        #endregion


        #region Commands

        private RelayCommand _newGroupCommand;
        public RelayCommand NewGroupCommand => _newGroupCommand ?? (_newGroupCommand = new RelayCommand(OnNewGroup));

        private RelayCommand _newStudentCommand;
        public RelayCommand NewStudentCommand => _newStudentCommand ?? (_newStudentCommand = new RelayCommand(OnNewStudent));

        private RelayCommand _removeStudentCommand;
        public RelayCommand RemoveStudentCommand => _removeStudentCommand ?? (_removeStudentCommand = new RelayCommand(OnRemoveStudent));

        private RelayCommand _exportCommand;
        public RelayCommand ExportCommand => _exportCommand ?? (_exportCommand = new RelayCommand(OnExport));

        private void OnExport(object obj)
        {
            if (SelectedGroup == null)
            {
                MessageBox.Show("Надо выбрать группу");
                return;
            }

            if (SelectedGroupStudentDtos.Count == 0)
            {
                MessageBox.Show("В группе нету студентов. Добавте студентов.");
                return;
            }


            Application ExcelApp;
            try
            {
                ExcelApp = new Application();
                Workbook ExcelWorkBook;
                Worksheet ExcelWorkSheet;
                //Книга.
                ExcelWorkBook = ExcelApp.Workbooks.Add(Missing.Value);
                //Таблица
                ExcelWorkSheet = (Worksheet)ExcelWorkBook.Worksheets.get_Item(1);
            }
            catch (Exception e)
            {
                MessageBox.Show($"Что-то пошло не так: {e}");
                return;
            }
            ExcelApp.Cells[1][1] = "Группа";
            ExcelApp.Cells[1][2] = "Факультет";
            ExcelApp.Cells[1][3] = "Форма обучения";
            ExcelApp.Cells[1][4] = "Специальность";
            ExcelApp.Cells[1][5] = "Год поступления";

            ExcelApp.Cells[2][1] = SelectedGroup.Name;
            ExcelApp.Cells[2][2] = SelectedGroup.Faculty;
            ExcelApp.Cells[2][3] = EnumHelper.GroupTypeList[(int)SelectedGroup.Type];
            ExcelApp.Cells[2][4] = SelectedGroup.Specialty;
            ExcelApp.Cells[2][5] = SelectedGroup.StartDate;

            ExcelApp.Cells[1][7] = "Фамилия";
            ExcelApp.Cells[2][7] = "Имя";
            ExcelApp.Cells[3][7] = "Отчество";
            ExcelApp.Cells[4][7] = "Пол";

            int rowOffset = 8;
            for (int i = 0; i < SelectedGroupStudentDtos.Count; i++)
            {
                ExcelApp.Cells[1][i+ rowOffset] = SelectedGroupStudentDtos[i].Surname;
                ExcelApp.Cells[2][i+ rowOffset] = SelectedGroupStudentDtos[i].Name;
                ExcelApp.Cells[3][i+ rowOffset] = SelectedGroupStudentDtos[i].Patronymic;
                ExcelApp.Cells[4][i+ rowOffset] = EnumHelper.SexTypeList[(int)SelectedGroupStudentDtos[i].Sex];
            }

            //Вызываем нашу созданную эксельку.
            ExcelApp.Visible = true;
            ExcelApp.UserControl = true;
        }


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

        private void OnNewStudent(object obj)
        {
            OpenDialogWindow<StudentAdd>(window =>
            {
                AddStudentViewModel viewModel = new AddStudentViewModel();
                viewModel.RequestClose += b => window.Close();
                window.DataContext = viewModel;
            });
            UpdateGroupStudents();
        }

        private void OnRemoveStudent(object obj)
        {
            if (SelectedStudent == null)
            {
                MessageBox.Show("Надо выбрать студента.");
                return;
            }

            _studentsRepository.Remove(_studentsRepository.FindById(SelectedStudent.Id));
            SelectedStudent = null;
            UpdateGroupStudents();
        }

        #endregion


        public GroupWindowViewModel()
        {
            _studentsRepository = new StudentsRepository(App.DataBaseContext);
            _groupRepository = new GroupRepository(App.DataBaseContext);

            UpdateGroups();
        }

        private void UpdateGroups()
        {
            Groups = new ObservableCollection<GroupDto>(DtoHelper.ConvertToGroupDtos(_groupRepository.FindAll().ToList()));
            if (_selectedGroup != null)
            {
                SelectedGroup = Groups.FirstOrDefault(item => item.Id.Equals(_selectedGroup.Id));
            }
        }

        private void UpdateGroupStudents()
        {
            if (SelectedGroup != null)
            {
                SelectedGroup = SelectedGroup;
            }
        }
    }
}
