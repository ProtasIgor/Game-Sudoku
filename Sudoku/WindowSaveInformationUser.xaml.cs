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
    /// Логика взаимодействия для WindowSaveInformationUser.xaml
    /// </summary>
    public partial class WindowSaveInformationUser : Window
    {
        private WindowFinishGame window;
        public WindowSaveInformationUser()
        {
            InitializeComponent();
            window = new();
        }
        public WindowSaveInformationUser(WindowFinishGame wg)
        {
            InitializeComponent();
            this.window = wg;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Text_Box_Input.Text == string.Empty)
            {
                MessageBox.Show("У вас незаполненное поле!");
                return;
            }
            this.window.nameUser = Text_Box_Input.Text;
            this.Close();
        }
    }
}
