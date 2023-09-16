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
    /// Логика взаимодействия для StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        public StartWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_ToEazyLevel(object sender, RoutedEventArgs e)
        {
           
           MainWindow window = new MainWindow(4);this.Close();
           window.ShowDialog();
        }

        private void Button_Click_ToHardLevel(object sender, RoutedEventArgs e)
        {
            
            MainWindow window = new MainWindow(9);this.Close();
            window.ShowDialog();
        }
    }
}
