using System;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace Sudoku
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly private Pole pole;

        internal DispatcherTimer timer;

        private TimeOnly time;

        private int helpVariable;
        public MainWindow ()
        {
            InitializeComponent();

            helpVariable = 0;

            pole = new Pole();
            timer = new();
            time = new();
        }
        public MainWindow (int count)
        {
            InitializeComponent();

            {
                helpVariable = count;
                Text_box_helpValue.Text = $"Количество подсказок: {helpVariable}";
            }

            pole = new Pole(count);

            Init();

            Remove();

            // create timer
            {
                timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(1);
                timer.Tick += Timer_Tick;
                timer.Start();

                time = new(0,0,0);
            }
        }
        private void Init ()
        {
            for (int i = 0; i < pole.dimension; i++)
            {
                MainGrid.ColumnDefinitions.Add(new ColumnDefinition());

                MainGrid.RowDefinitions.Add(new RowDefinition());
            }

            for (int i = 0; i < pole.dimension; i++)
            {
                for (int j = 0; j < pole.dimension; j++)
                {
                    TextBox textbox = new();

                    textbox.Name = $"Text_Box{i}{j}";
                    textbox.Text = pole.Array[i, j].ToString();
                    textbox.IsReadOnly = true;
                    textbox.Focusable = false;
                    textbox.Background = Brushes.Silver;

                    textbox.SetValue(Grid.RowProperty, i);
                    textbox.SetValue(Grid.ColumnProperty, j);

                    textbox.TextChanged += Textbox_TextChanged;

                    MainGrid.Children.Add(textbox);
                    pole.ArrayTextBoxes[i, j] = textbox;
                }
            }
            MainGrid.Children.Add(new GridSplitter());
        }
        private void Remove ()
        {
            Random random = new();

            int procentrandom = random.Next((int)(Math.Pow(pole.dimension, 2) * 0.4), (int)(Math.Pow(pole.dimension, 2) * 0.5));
            
            for (int i = 0; i < procentrandom; i++)
            {
                int x = random.Next(0, pole.dimension);
                int y = random.Next(0, pole.dimension);
                pole.ArrayTextBoxes[x, y].Text = "";
                pole.ArrayTextBoxes[x, y].IsReadOnly = false;
                pole.ArrayTextBoxes[x, y].Background = Brushes.MintCream;
                pole.ArrayTextBoxes[x, y].Focusable = true;
            }
        }
        private void Textbox_TextChanged (object sender, TextChangedEventArgs e)
        {
            TextBox textbox = (TextBox)sender;

            try
            {
                if (textbox.Text.ToString() == "0")
                {
                    throw new Exception();
                }
                if (!int.TryParse(textbox.Text, out int x) && !string.IsNullOrEmpty(textbox.Text))
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                textbox.Text = "";
                MessageBox.Show(@"Пожалуйста, проверьте значение.
Корректным является число от 1 до 9");
            }
        }
        private void Button_Click (object sender, RoutedEventArgs e)
        {
            int countCorrectElement = 0;
            for (int i = 0; i < pole.dimension; i++)
            {
                for (int j = 0; j < pole.dimension; j++)
                {
                    if (pole.ArrayTextBoxes[i, j].Text == pole.Array[i, j].ToString())
                    {
                        countCorrectElement++;
                    }
                }
            }
            if (countCorrectElement == pole.dimension * pole.dimension)
            {
                this.timer.Stop();

                WindowFinishGame window = new(time);this.Close();
                window.ShowDialog();
            }
            else
            {
                MessageBox.Show("Неверно, попробуйте снова.");
            }
        }
        private void Timer_Tick (object? sender, EventArgs e)
        {
            this.time = this.time.Add(new TimeSpan(0,0,1));
            Text_Timer.Content = time.ToLongTimeString();
        }
        private void Button_Click_ToStopTimer (object sender, RoutedEventArgs e)
        {
            this.timer.Stop();
        }
        private void Button_Click_ToContinueTimer (object sender, RoutedEventArgs e)
        {
            this.timer.Start();
        }
        private void Button_Click_BackToMenu (object sender, RoutedEventArgs e)
        {
            timer.Stop();

            WindowMenuToChange window = new(this);
            window.Owner = this;
            window.ShowDialog();
        }
        private void Button_Click_GetHelpValue (object sender, RoutedEventArgs e)
        {
            if (helpVariable == 0)
            {
                MessageBox.Show("Подсказки закончились.");
            }
            else
            {
                this.helpVariable--;
                Text_box_helpValue.Text = $"Количество подсказок: {helpVariable}";

                Random random = new();

                int x, y;

                do
                {
                    x = random.Next(pole.dimension);
                    y = random.Next(pole.dimension);

                } while (pole.ArrayTextBoxes[x, y].IsReadOnly == true);

                pole.ArrayTextBoxes[x, y].Text = pole.Array[x, y].ToString();
                pole.ArrayTextBoxes[x, y].IsReadOnly = true;
                pole.ArrayTextBoxes[x, y].Focusable = false;
            }
        }

        #region Change Color
        private void Button_Click_ChooseGreen(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < this.pole.dimension; i++)
            {
                for (int j = 0; j < this.pole.dimension; j++)
                {
                    if (this.pole.ArrayTextBoxes[i, j].IsReadOnly == true)
                        this.pole.ArrayTextBoxes[i, j].Background = BackgroundGreen.Background;
                    else
                        this.pole.ArrayTextBoxes[i, j].Background = Brushes.GreenYellow;
                    this.pole.ArrayTextBoxes[i, j].Foreground = Brushes.Black;
                }
            }
        }
        private void Button_Click_ChooseOrange(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < this.pole.dimension; i++)
            {
                for (int j = 0; j < this.pole.dimension; j++)
                {
                    if (this.pole.ArrayTextBoxes[i, j].IsReadOnly == true)
                        this.pole.ArrayTextBoxes[i, j].Background = BackgroundOrange.Background;
                    else
                        this.pole.ArrayTextBoxes[i, j].Background = Brushes.IndianRed;
                    this.pole.ArrayTextBoxes[i, j].Foreground = Brushes.Black;
                }
            }
        }
        private void Button_Click_ChooseBlue(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < this.pole.dimension; i++)
            {
                for (int j = 0; j < this.pole.dimension; j++)
                {
                    if (this.pole.ArrayTextBoxes[i, j].IsReadOnly == true)
                        this.pole.ArrayTextBoxes[i, j].Background = BackgroundBlue.Background;
                    else
                        this.pole.ArrayTextBoxes[i, j].Background = Brushes.Aqua;
                    this.pole.ArrayTextBoxes[i, j].Foreground = Brushes.WhiteSmoke;
                }
            }
        }
        private void Button_Click_ChooseDefault(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < this.pole.dimension; i++)
            {
                for (int j = 0; j < this.pole.dimension; j++)
                {
                    if (this.pole.ArrayTextBoxes[i, j].IsReadOnly == true)
                        this.pole.ArrayTextBoxes[i, j].Background = BackgroundDefault.Background;
                    else
                        this.pole.ArrayTextBoxes[i, j].Background = Brushes.WhiteSmoke;
                    this.pole.ArrayTextBoxes[i, j].Foreground = Brushes.BurlyWood;
                }
            }
        }
        #endregion
    }
}
