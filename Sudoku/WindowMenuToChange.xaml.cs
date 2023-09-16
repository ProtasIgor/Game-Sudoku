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
using System.Windows.Shapes;

namespace Sudoku
{
    /// <summary>
    /// Логика взаимодействия для WindowMenuToChange.xaml
    /// </summary>
    public partial class WindowMenuToChange : Window
    {
        MainWindow mainWindow;
        public WindowMenuToChange()
        {
            InitializeComponent();
            mainWindow = new();
        }
        public WindowMenuToChange(MainWindow mw)
        {
            InitializeComponent();
            mainWindow = mw;
        }
        private void Button_Click_StartWindow(object sender, RoutedEventArgs e)
        {
            StartWindow window = new();
            this.Owner.Close();
            window.Show();
        }
        private void Button_Click_Back(object sender, RoutedEventArgs e)
        {
            this.mainWindow.timer.Start();
            this.Close();          
        }
    }
}
