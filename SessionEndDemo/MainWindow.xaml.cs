using System.Runtime.InteropServices;
using System;
using System.Windows;
using System.Windows.Interop;

namespace SessionEndDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Loaded -= MainWindow_Loaded;
            var handle = new WindowInteropHelper(this).Handle;
            ShutdownBlockReasonDestroy(handle);
            ShutdownBlockReasonCreate(handle, "应用保存数据中，请等待...");

            //窗口Hide不影响上面的ShutdownBlockReasonDestroy
            Hide();
        }
        [DllImport("user32.dll")]
        private static extern bool ShutdownBlockReasonCreate(IntPtr hWnd, [MarshalAs(UnmanagedType.LPWStr)] string reason);
        [DllImport("user32.dll")]
        private static extern bool ShutdownBlockReasonDestroy(IntPtr hWnd);
    }
}
