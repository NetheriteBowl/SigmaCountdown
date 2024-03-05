using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;


namespace SigmaCountdown
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
            //this.SizeToContent = SizeToContent.WidthAndHeight;
            Left = SystemParameters.PrimaryScreenWidth - (this.Width);
            event_level.Text = Properties.Settings.Default.EventLevel_Text;
            if (Properties.Settings.Default.TopRight == false)
            { Top = SystemParameters.WorkArea.Height - (this.Height); }
            else
            { Top = 1;}
        }

        private void StartCountdown()
        {
            DateTime deadline = Properties.Settings.Default.Date + new TimeSpan(9, 00, 0);
            DateTime deadlineOfDay = DateTime.Now.Date.AddDays(1).AddHours(9).AddMinutes(0).AddSeconds(0).AddMilliseconds(0);
            TimeSpan timeLeft = deadline - DateTime.Now;
            TimeSpan dotSpan = deadlineOfDay - DateTime.Now;
            int daysLeft = (int)timeLeft.TotalDays;
            DispatcherTimer timer = new DispatcherTimer();
             timer.Interval = TimeSpan.FromSeconds(1);
             timer.Tick += (sender, e) =>
            {
                string Numdecimal = (dotSpan.TotalSeconds/86400.0).ToString(".000");
                if (Numdecimal == "1.000")
                { decimalNum.Text = ".999"; }
                else { decimalNum.Text = Numdecimal.ToString();}
                    countdown.Text = daysLeft.ToString();

            };

             timer.Start();
        }

        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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
