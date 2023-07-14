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

        private void StartCountdown()
        {
            DateTime deadline = new DateTime(2024, 6, 7, 8, 30, 0);
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
            };

            timer.Start();
        }
    }
}
