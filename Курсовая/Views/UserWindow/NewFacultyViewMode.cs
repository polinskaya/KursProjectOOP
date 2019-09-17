using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using NavigationDrawerPopUpMenu2;
using Курсовая.DataBase;
using Курсовая.DataBase.Repositories;
using Курсовая.DataBase.RepositoryInterfaces;

namespace Курсовая.Views.UserWindow 
{
    public class NewFacultyViewMode : BaseViewModel
    {
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

        private RelayCommand _saveCommand;
        public RelayCommand SaveCommand => _saveCommand ?? (_saveCommand = new RelayCommand(OnSave));

        private void OnSave(object obj)
        {
            var fac = new Факультет
            {
                Код_факультета = ShortName,
                Наименование_факультета = FullName
            };
            if (_facultyRepository.FindAll().Any(item => item.Код_факультета.Equals(fac.Код_факультета)))
            {
                MessageBox.Show("Такой Факультет уже есть.");
                return;
            }
            _facultyRepository.Create(fac);
            CloseWindow(true);
        }


        public NewFacultyViewMode()
        {
            _facultyRepository = new FacultyRepository(App.DataBaseContext);
        }
    }
}
