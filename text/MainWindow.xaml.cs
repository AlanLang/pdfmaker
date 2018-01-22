using QRCoder;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace text
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            NameValueCollection nav = new NameValueCollection();
            nav.Add("img1", "https://qr.alipay.com/tsx04136zdm6mxp7jvv4s2f");
            nav.Add("text1", "改需求推荐使用支付宝");
            string path = AppDomain.CurrentDomain.BaseDirectory + "config.yml";

            pdfmaker.pdfmaker make = new pdfmaker.pdfmaker();
            make.MakePdf(nav, path, "测试1.pdf");
        }
    }
}
