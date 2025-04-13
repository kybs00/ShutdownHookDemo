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
                var executeSuccess = ExecuteShutdownWork();
                e.Cancel = !executeSuccess;
            }
        }
        private bool ExecuteShutdownWork()
        {
            //Test
            Thread.Sleep(TimeSpan.FromSeconds(200));
            return false;
            try
            {
                // XXX
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
