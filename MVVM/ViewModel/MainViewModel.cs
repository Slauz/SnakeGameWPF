using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SnakeGameWPF.MVVM.ViewModel;

namespace SnakeGameWPF.MVVM.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public UpdateViewCommand UpdateViewCommand { get; set; }
        private BaseViewModel currentView = new MenuViewModel();
        public BaseViewModel CurrentView 
        {
            get
            {
                return currentView;
            }
            set
            {
                currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }

        public MainViewModel()
        {
            UpdateViewCommand = new UpdateViewCommand(this);
        }
    }
}

