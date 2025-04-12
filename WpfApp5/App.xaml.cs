using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using Microsoft.Win32;

namespace SessionEndDemo
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            SystemEvents.SessionEnding += SystemEvents_SessionEnding;
            Application.Current.Exit += Current_Exit;
        }
        private void Current_Exit(object sender, ExitEventArgs e)
        {
            SystemEvents.SessionEnding -= SystemEvents_SessionEnding;
        }
        private void SystemEvents_SessionEnding(object sender, SessionEndingEventArgs e)
        {
            if (e.Reason == SessionEndReasons.SystemShutdown)
            {
                var canShutDown = PerformShutdownWork();
                e.Cancel = !canShutDown;
            }
        }
        private bool PerformShutdownWork()
        {
            Thread.Sleep(TimeSpan.FromSeconds(70));
            return true;
        }
    }
}
