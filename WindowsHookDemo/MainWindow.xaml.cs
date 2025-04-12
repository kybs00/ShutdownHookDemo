using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
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

namespace WindowsHookDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            Loaded -= OnLoaded;
            var source = PresentationSource.FromVisual(this) as HwndSource;
            source?.AddHook(WndProc);

            var handle = new WindowInteropHelper(this).Handle;
            ShutdownBlockReasonCreate(handle, "应用保存数据中，请等待...");

            //窗口Hide，并不影响上面的ShutdownBlockReasonDestroy
            Hide();
        }
        const int WM_QUERYENDSESSION = 0x11;
        const int WM_ENDSESSION = 0x16;
        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == WM_QUERYENDSESSION)
            {
                // 在这里执行你的业务逻辑
                bool canShutdown = PerformShutdownWork();

                // 返回0表示阻止关机，1表示允许关机
                handled = true;
                return canShutdown ? (IntPtr)1 : (IntPtr)0;
            }
            return IntPtr.Zero;
        }

        private bool PerformShutdownWork()
        {
            Thread.Sleep(TimeSpan.FromSeconds(20));
            return true;
        }

        [DllImport("user32.dll")]
        private static extern bool ShutdownBlockReasonCreate(IntPtr hWnd, [MarshalAs(UnmanagedType.LPWStr)] string reason);
        [DllImport("user32.dll")]
        private static extern bool ShutdownBlockReasonDestroy(IntPtr hWnd);
    }
}
