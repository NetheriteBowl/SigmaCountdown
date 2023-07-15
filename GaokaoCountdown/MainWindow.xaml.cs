using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace GaokaoCountdown
{


    public partial class MainWindow : Window
    {
            public MainWindow()
        {
            InitializeComponent();
            StartCountdown();
        }
    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
            this.Height = SystemParameters.WorkArea.Height / 4;
            this.Width = 3.14*this.Height;
            Left = SystemParameters.PrimaryScreenWidth - (this.Width);
            Top = 3*SystemParameters.WorkArea.Height / 4;
        }
        private void StartCountdown()
        {
            DateTime deadline = Properties.Settings.Default.Date + new TimeSpan(8, 30, 0);
            TimeSpan timeLeft = deadline - DateTime.Now;
            int daysLeft = (int)timeLeft.TotalDays;

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += (sender, e) =>
            {
                timeLeft = deadline - DateTime.Now;
                daysLeft = (int)timeLeft.TotalDays;

                countdown.Text = daysLeft.ToString();
                pCountdown.Text = daysLeft.ToString();
                year.Text = Properties.Settings.Default.Year;
            };

            timer.Start();
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        private void MenuItem_Settings_Click(object sender, RoutedEventArgs e)
        {
            new SettingsWindow().Show();
        }

        private void MenuItem_Close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
