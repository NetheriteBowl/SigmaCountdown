﻿using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32.TaskScheduler;

namespace SigmaCountdown
{
    public partial class SettingsWindow : Window
    {

        public SettingsWindow()
        {
            InitializeComponent();
            DateTime selectedDate = Properties.Settings.Default.Date;
            datePicker.SelectedDate = selectedDate;
            bool AutoSIsonoff = Properties.Settings.Default.AutoS;
            AutoStartCheckbox.IsChecked = AutoSIsonoff;
            bool TopLeftIsonoff = Properties.Settings.Default.TopLeft;
            TopLeftCheckbox.IsChecked = TopLeftIsonoff;
            event_Level_TB.Text = Properties.Settings.Default.EventLevel_Text;
        }

        //日期设定
        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DatePicker datePicker = sender as DatePicker;
            DateTime selectedDate = datePicker.SelectedDate ?? DateTime.Now;
            // 设置新的
            Properties.Settings.Default.Date = selectedDate.Date;
            Properties.Settings.Default.Year = selectedDate.Year.ToString();
        }

        //编辑提醒级别文本
        private void LevelTextChangedEventHandler(object sender, TextChangedEventArgs args)
        {
            Properties.Settings.Default.EventLevel_Text = event_Level_TB.Text;
        }

        //左上角显示
        private void TopLeftChecked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.TopLeft = true;
        }

        private void TopLeftUnchecked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.TopLeft = false;
        }

        //自启动
        private void AutoStartChecked(object sender, RoutedEventArgs e)
        {
            AutoStartManager.SetAutoStart(true);
        }

        private void AutoStartUnchecked(object sender, RoutedEventArgs e)
        {
            AutoStartManager.SetAutoStart(false);
        }
        public static class AutoStartManager
        {
            private const string TaskName = "Sigma Countdown - 倒计时自启动";

            public static void SetAutoStart(bool enable)
            {
                if (enable)
                {
                    RegisterAutoStart();
                    Properties.Settings.Default.AutoS = true;
                }
                else
                {
                    UnregisterAutoStart();
                    Properties.Settings.Default.AutoS = false;
                }
            }

            private static void RegisterAutoStart()
            {
                using (TaskService taskService = new TaskService())
                {
                    // 定义计划任务名称和描述
                    string taskName = "Sigma Countdown - 倒计时自启动";
                    string taskDescription = "可在右键菜单的设置中关闭";
                    string appPath = Process.GetCurrentProcess().MainModule.FileName;
                    // 创建计划任务
                    TaskDefinition taskDefinition = taskService.NewTask();

                    // 设置任务触发器：系统启动
                    taskDefinition.Triggers.Add(new LogonTrigger());

                    // 设置启动路径
                    taskDefinition.Actions.Add(new ExecAction(appPath));

                    // 注册计划任务
                    taskService.RootFolder.RegisterTaskDefinition(taskName, taskDefinition);

                    // 设置任务描述
                    taskService.GetTask(taskName).Definition.RegistrationInfo.Description = taskDescription;
                }
            }
            private static void UnregisterAutoStart()
            {
                // 删除计划任务
                using (TaskService taskService = new TaskService())
                {
                    // 设置计划任务的名称
                    string taskName = "Sigma Countdown - 倒计时自启动";

                    // 删除计划任务
                    taskService.RootFolder.DeleteTask(taskName, false);
                }
            }
        }

        //保存按钮
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Save();
            // 返回主界面
            MainWindow MainWindow = Application.Current.MainWindow as MainWindow;
            if (MainWindow != null)
            {
                System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                Application.Current.Shutdown();
            }
        }
    }
}