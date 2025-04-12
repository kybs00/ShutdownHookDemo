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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HighLevelProcessShutdownDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // 在应用程序启动时调用
            // 0x4FF表示最高优先级，确保你的程序最后被关闭
            // 0x100表示最低优先级，确保你的程序最先被关闭
            SetProcessShutdownParameters(0x4FF, 0);
        }
        [DllImport("kernel32.dll")]
        static extern bool SetProcessShutdownParameters(uint dwLevel, uint dwFlags);
    }
}
