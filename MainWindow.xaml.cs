using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SnakeGameWPF.MVVM.ViewModel;

namespace SnakeGameWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = ((App)Application.Current).MainViewModel;
        }

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (KeyPressedManager.CanRegister)
            {
                switch (e.Key)
                {
                    case Key.Up:
                        KeyPressedManager.PressedKey = Key.Up;
                        break;
                    case Key.Right:
                        KeyPressedManager.PressedKey = Key.Right;
                        break;
                    case Key.Left:
                        KeyPressedManager.PressedKey = Key.Left;
                        break;
                    case Key.Down:
                        KeyPressedManager.PressedKey = Key.Down;
                        break;
                }
            }
        }
    }
}
