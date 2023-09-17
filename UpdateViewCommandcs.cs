using SnakeGameWPF.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SnakeGameWPF
{
    public class UpdateViewCommand : ICommand
    {
        private MainViewModel viewModel;

        public UpdateViewCommand(MainViewModel VM)
        {
            viewModel = VM;
        }

        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {

            if (parameter.ToString() == "Menu")
            {
                viewModel.CurrentView = new MenuViewModel();
            }
            else if (parameter.ToString() == "Game")
            {
                viewModel.CurrentView = new GameViewModel();
            }
            else
            {
                return;
            }
        }
    }
}
