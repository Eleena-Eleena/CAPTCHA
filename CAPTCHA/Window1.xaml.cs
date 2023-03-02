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
using System.Windows.Threading;

namespace CAPTCHA
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        String nmr;
        DispatcherTimer disTimer = new DispatcherTimer();
        DispatcherTimer dT = new DispatcherTimer();
        public Window1(String numberrandom)
        {
            InitializeComponent();
            nmr = numberrandom.ToString();
            disTimer.Interval = new TimeSpan(0, 0, 10);
            dT.Interval = TimeSpan.FromSeconds(1);
            dT.Tick += timer;
            disTimer.Tick += new EventHandler(DisTimer_Tick);
            dT.Start();
            disTimer.Start();

        }
        private int i = 9;
        private void timer(object sender, EventArgs e)
        {
            if (i != 0)
            {
                labelTIMER.Content = i.ToString();
                i--;
            }
            else
            {
                MessageBox.Show("Время вышло");
                dT.Stop();
            }

        }
        private void DisTimer_Tick( object sender, EventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
            disTimer.Tick -= new EventHandler(DisTimer_Tick);
        }

        private void ButtonRANDOM_Click(object sender, RoutedEventArgs e)
        {
            if (inputRANDOM.Text == nmr)
            {
                MessageBox.Show("Успешно");
                disTimer.Stop();
                dT.Stop();
            }
            else
            {
                MessageBox.Show("Не успешно");
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
                disTimer.Tick -= new EventHandler(DisTimer_Tick);
            }
        }
    }
}
