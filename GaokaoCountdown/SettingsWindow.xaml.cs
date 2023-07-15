using System;
using System.Collections.Generic;
using System.Diagnostics;
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


namespace GaokaoCountdown
{
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
        }

        //DateTime selectedDate = Properties.Settings.Default.Date;

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DatePicker datePicker = sender as DatePicker;
            DateTime selectedDate = datePicker.SelectedDate ?? DateTime.Now;
            // 设置新的
            Properties.Settings.Default.Date = selectedDate.Date;
            Properties.Settings.Default.Year = selectedDate.Year.ToString();
            Properties.Settings.Default.Save();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            //DatePicker datePicker = sender as DatePicker;
            DateTime selectedDate = datePicker.SelectedDate ?? DateTime.Now;
            // 设置新的
            Properties.Settings.Default.Date = selectedDate.Date;
            Properties.Settings.Default.Year = selectedDate.Year.ToString();
            Properties.Settings.Default.Save();
            // 返回主界面
            MainWindow MainWindow = Application.Current.MainWindow as MainWindow;
            if (MainWindow != null)
            {

                // this.Close();
                Application.Current.Shutdown();
            }
        }
    }
}
