using System;
using System.Collections.Generic;
using System.IO;
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
using static System.Net.Mime.MediaTypeNames;

namespace Sudoku
{
    /// <summary>
    /// Логика взаимодействия для WindowFinishGame.xaml
    /// </summary>
    public partial class WindowFinishGame : Window
    {
        internal string nameUser;
        private readonly string timeToPlayGame;
        private bool isCouldHasValue;
        public WindowFinishGame ()
        {
            InitializeComponent();
            timeToPlayGame = string.Empty;
            nameUser = string.Empty;
            isCouldHasValue = true;
        }
        public WindowFinishGame (TimeOnly timeOnly)
        {
            InitializeComponent();
            this.timeToPlayGame = timeOnly.ToLongTimeString();
            this.nameUser = string.Empty;
            isCouldHasValue = true;
        }
        private void Button_Click_Check_TableRecords (object sender, RoutedEventArgs e)
        {
            string[] textAllFile;

            using (StreamReader streamReader = new(@".\TableScore\TopResult.txt"))
            {
                textAllFile = streamReader.ReadToEnd().Split("\r\n");

                if (string.IsNullOrEmpty(textAllFile[0]))
                {
                    MessageBox.Show("На данный момент, лучших результатов нет\nСтаньте первым, сохраните ваши результаты.");
                    return;
                }

                for (int i = 1; i < textAllFile.Length; i++)
                {
                    if (i % 2 != 0)
                        textAllFile[i - 1] = "\tВремя:\t" + textAllFile[i - 1] + "\t";
                    if (i % 2 == 0)
                        textAllFile[i - 1] = "\tНикнейм:\t" + textAllFile[i - 1] + "\t";
                }

                string? text = string.Empty;
                foreach (var item in textAllFile)
                {
                    text += item + "\r\n";
                }
                MessageBox.Show(text, "Table Records");

                streamReader.Close();
            }
        }
        private void Button_Click_Create_New_Game (object sender, RoutedEventArgs e)
        {
            StartWindow window = new();
            this.Close();
            window.Show();
        }
        private void Button_Click_Save_Result (object sender, RoutedEventArgs e)
        {
            if (this.isCouldHasValue)
            {
                WindowSaveInformationUser window = new(this);
                window.Owner = this;
                window.ShowDialog();
                
                using (StreamWriter streamWriter = new(@".\TableScore\TopResult.txt", true))
                {
                    streamWriter.WriteLine(this.timeToPlayGame);
                    streamWriter.WriteLine(this.nameUser);
                    streamWriter.Close();
                }

                this.isCouldHasValue = false;
            }
            else
            {
                MessageBox.Show("Вы уже сохранили свой рекорд.");
            }
        }
        private void Button_Click_ToRemoveTableRecords (object sender, RoutedEventArgs e)
        {
            FileStream fs = File.Open(@".\TableScore\TopResult.txt", FileMode.Open, FileAccess.ReadWrite);
            fs.SetLength(0);
            fs.Close();
        }
    }
}
