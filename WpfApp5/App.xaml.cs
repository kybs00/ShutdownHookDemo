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
            // 在应用程序启动时调用
            // 0x4FF表示最高优先级，确保你的程序最后被关闭
            SetProcessShutdownParameters(0x4FF, 0);
        }

        private void Current_Exit(object sender, ExitEventArgs e)
        {
            SystemEvents.SessionEnding -= SystemEvents_SessionEnding;
        }

        private async void SystemEvents_SessionEnding(object sender, SessionEndingEventArgs e)
        {
            if (e.Reason == SessionEndReasons.SystemShutdown)
            {

                var currentMainWindow = Application.Current.MainWindow;
                var handle = new WindowInteropHelper(currentMainWindow).Handle;
                ShutdownBlockReasonCreate(handle, "应用保存数据中，请等待...");
                //Thread.Sleep(TimeSpan.FromSeconds(10));
                await Task.Delay(10000);
                ShutdownBlockReasonCreate(handle, "应用已保存数据");
                Thread.Sleep(TimeSpan.FromSeconds(10));
                ShutdownBlockReasonDestroy(handle);
                e.Cancel = false;
            }
        }
        [DllImport("kernel32.dll")]
        static extern bool SetProcessShutdownParameters(uint dwLevel, uint dwFlags);

        [DllImport("user32.dll")]
        private static extern bool ShutdownBlockReasonCreate(IntPtr hWnd, [MarshalAs(UnmanagedType.LPWStr)] string reason);

        [DllImport("user32.dll")]
        private static extern bool ShutdownBlockReasonDestroy(IntPtr hWnd);
    }
}
