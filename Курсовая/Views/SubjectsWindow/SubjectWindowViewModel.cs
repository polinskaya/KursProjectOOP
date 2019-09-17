using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NavigationDrawerPopUpMenu2;
using Курсовая.DataBase.Repositories;
using Курсовая.DataBase.RepositoryInterfaces;
using Курсовая.Views.UiCommon.Dto;

namespace Курсовая.Views.SubjectsWindow
{
    public class SubjectWindowViewModel : BaseViewModel
    {

        private ISubjectsRepository _subjectsRepository;

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

        private RelayCommand _addSubjectCommand;

        public RelayCommand AddSubjectCommand => _addSubjectCommand ?? (_addSubjectCommand = new RelayCommand(OnAddSubject));

        private void OnAddSubject(object obj)
        {
            OpenDialogWindow<AddRealSubjectWindow>(window =>
            {
                RealSubjectViewModel viewModel = new RealSubjectViewModel();
                viewModel.RequestClose += b => window.Close();
                window.DataContext = viewModel;
            });

            Subjects = new ObservableCollection<SubjectDto>(DtoHelper.ConvertToSubjectDtos(_subjectsRepository.FindAll().ToList()));
        }


        public SubjectWindowViewModel()
        {
            _subjectsRepository = new SubjectRepository(App.DataBaseContext);
            Subjects = new ObservableCollection<SubjectDto>(DtoHelper.ConvertToSubjectDtos(_subjectsRepository.FindAll().ToList()));
        }
    }
}
