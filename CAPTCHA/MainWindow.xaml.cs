using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
using System.Windows.Threading;

namespace CAPTCHA
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        public static int count = 0;
        String numberFive;
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        DispatcherTimer diTi = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();

            if (count == 0)
            {
                count++;
            }
            else if (count == 1)
            {
                buttonLogin.IsEnabled = false;
                dispatcherTimer.Interval = new TimeSpan(0, 0, 60);
                diTi.Interval = TimeSpan.FromSeconds(1);
                diTi.Tick += timer1;
                dispatcherTimer.Tick += new EventHandler(DisTimer_Tick1);
                diTi.Start();
                dispatcherTimer.Start();
                count++;
            }
            else
            {
                wpLog.Visibility = Visibility.Collapsed;
                wpCap.Visibility = Visibility.Visible;    
                CAPTCHA();
            }
            
        }
        private int i = 58;
        private void timer1(object sender, EventArgs e)
        {
            if (i != 0)
            {
                labelTIMER.Content = "Получить новый код можно будет через " + i.ToString() + " секунд" ;
                i--;
            }
            else
            {
                MessageBox.Show("Время вышло");
                labelTIMER.Visibility = Visibility.Hidden;
                diTi.Stop();
            }

        }
        private void DisTimer_Tick1(object sender, EventArgs e)
        {
            dispatcherTimer.Tick -= new EventHandler(DisTimer_Tick1);
            buttonLogin.IsEnabled = true;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            String label = (string)labelCaptcha.Content;
            String textBoxCAPTHA = inputCAPTCHA.Text;
            if (label == textBoxCAPTHA)
            {
                MessageBox.Show("Верно");
                wpLog.Visibility = Visibility.Visible;
                wpCap.Visibility = Visibility.Collapsed;
            }
            else
            {
                MessageBox.Show("Неверно");
                CAPTCHA();
            }
        }
        private void buttonLogin_Click(object sender, RoutedEventArgs e)
        {
                if (tbLogin.Text == "admin" && tbPassword.Password == "12345")
                {
                    RANDOM();
                    Window1 window1 = new Window1(numberFive);
                    window1.Show();
                    this.Close();
                }
           
        }
        public void RANDOM()
        {
            Random random = new Random();
            Random rA = new Random();
            for (int i = 0; i < 5; i++)
            {
                numberFive += random.Next(0,9);
            }
            MessageBox.Show(numberFive);
        }
        private void CAPTCHA()
        {
            String allowchar = " ";
            allowchar = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
            allowchar += "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,y,z";
            allowchar += "1,2,3,4,5,6,7,8,9,0";
            char[] a = { ',' };
            String[] ar = allowchar.Split(a);
            String pwd = "";
            string temp = "";
            Random randomSym = new Random();
            Random rA = new Random();
            for (int i = 0; i < rA.Next(5, 10); i++)
            {
                temp = ar[randomSym.Next(0, ar.Length)];
                pwd += temp;
            }
            Random randonColor = new Random();
            Brush brush = new SolidColorBrush(Color.FromRgb((byte)randonColor.Next(1, 255), (byte)randonColor.Next(1, 255), (byte)randonColor.Next(1, 233)));
            labelCaptcha.Content = pwd;
            labelCaptcha.Foreground = brush;
        }


    }

}
