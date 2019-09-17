using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Курсовая
{
    //базовый класс для всех Viewmodel
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        private RelayCommand _showDescription;

        /// <summary>
        ///     Событие оповещения об изменении свойства
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Метод котоый будет вызван для закрытия окна
        /// </summary>
        public event Action<bool> RequestClose;

        /// <summary>
        ///     Метод котоый будет вызван для сокрытия окна
        /// </summary>
        public event Action RequestHide;

        /// <summary>
        ///     Метод котоый будет вызван для возврата окна на экран после сокрытия(HideWindow)
        /// </summary>
        public event Action RequestShow;

        public event Action RequsetHideParent;

        /// <summary>
        ///     Оповещает все связонные со свойством контроллы для того чтобы они обновились и отобразили актуальное значение
        ///     свойства
        /// </summary>
        /// <param name="propertyName">Имя свойства которое изменилось</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// Открывает новое окно и устанавливает как владельца последнее открытое окно
        /// <param name="initWindow">это метод инициализации окна, установка ViewModel и других свойств для корректной работы окна</param>
        public bool? OpenDialogWindow<TWindow>(Action<TWindow> initWindow) where TWindow : Window
        {
            var window = (TWindow) Activator.CreateInstance(typeof(TWindow));
            initWindow(window);

            window.Owner = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);

            return window.ShowDialog();
        }

        /// <summary>
        ///     Метод для закрытия окна прямо из ViewModel, для этого нужно задать свойство RequestClose
        /// </summary>
        public void CloseWindow(bool result)
        {
            RequestClose?.Invoke(result);
        }

        /// <summary>
        ///     Метод для сокрытия окна из ViewModel, для этого нужно задать свойство RequestHide
        /// </summary>
        public void HideWindow()
        {
            RequestHide?.Invoke();
        }

        /// <summary>
        ///     Метод для возврата на экран окна из ViewModel, для этого нужно задать свойство RequestShow
        /// </summary>
        public void ShowWindow()
        {
            RequestShow?.Invoke();
        }

        public void HideParentWindow()
        {
            RequsetHideParent?.Invoke();
        }

        protected bool ShowConfirmMessage()
        {
            return MessageBox.Show("Вы уверены?", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes;
        }

        public WindowState PrevWindowState { get; set; }
    }
}