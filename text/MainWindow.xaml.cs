using QRCoder;
using System;
using System.Collections.Generic;
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
            pdfmaker.PdfDatas data = new pdfmaker.PdfDatas();

            List<pdfmaker.texts> list = new List<pdfmaker.texts>();
            List<string> lines = new List<string>() { "line1" };
            List<pdfmaker.imgs> imgs = new List<pdfmaker.imgs>();

            pdfmaker.texts text = new pdfmaker.texts();
            text.name = "text1";
            text.value = "改需求请使用支付宝";
            list.Add(text);
            pdfmaker.texts text1 = new pdfmaker.texts();
            text1.name = "text2";
            text1.value = "测试2";
            list.Add(text1);

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode("48646513843435468", QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);

            System.Drawing.Bitmap qrCodeImage = qrCode.GetGraphic(20);

            MemoryStream ms = new MemoryStream();
            qrCodeImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            ms.Flush();
            pdfmaker.imgs img = new pdfmaker.imgs();
            img.name = "img1";
            img.img = qrCodeImage;
            imgs.Add(img);
            data.texts = list;
            data.lines = lines;
            data.imgs = imgs;
            string path = AppDomain.CurrentDomain.BaseDirectory + "config.yml";
            pdfmaker.pdfmaker make = new pdfmaker.pdfmaker();
            make.MakePdf(data, path, "测试.pdf");
        }
    }
}
