using System.Windows;

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
            //在构造中设置Hide或者Show之后立即设置Hide，均会导致关机阻止失败
            Hide();

            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Loaded -= MainWindow_Loaded;
            //Loaded之后Hide没问题
            //Hide();
            //设置Visibility也没问题
            //Visibility=Visibility.Collapsed;
        }
    }
}
