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
            SizeChanged += WindowSizeChanged;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetWindowPositionAndSize();
            string Eventyear = Properties.Settings.Default.Date.Year.ToString();
            string EventName = Properties.Settings.Default.EventName_Text;
            event_level.Text = Properties.Settings.Default.EventLevel_Text;
            if (Properties.Settings.Default.EventYear_Visibility == false)
            {
                event_nameandyear.Text = "距" + EventName;
            }
            else
            {
                event_nameandyear.Text = "距" + Eventyear + EventName;
            }
        }
        private void WindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            SetWindowPositionAndSize();
        }
        private void SetWindowPositionAndSize()
        {
            Left = SystemParameters.PrimaryScreenWidth - (this.ActualWidth);
            if (Properties.Settings.Default.TopRight == false)
            { Top = SystemParameters.WorkArea.Height - (this.ActualHeight); }
            else
            { Top = 1; }
        }

        private void StartCountdown()
        {
            DateTime deadline = Properties.Settings.Default.Date + new TimeSpan(9, 00, 0);
            TimeSpan timeLeft = deadline - DateTime.Now;
            int daysLeft = (int)timeLeft.TotalDays;
            DispatcherTimer timer = new DispatcherTimer();
             timer.Interval = TimeSpan.FromSeconds(1);
             timer.Tick += (sender, e) =>
            {
                timeLeft = deadline - DateTime.Now;
                daysLeft = (int)timeLeft.TotalDays;
                string Numdecimal = (timeLeft.TotalDays - timeLeft.Days).ToString(".000");
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
