using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace pdfmaker
{
    public class PdfDatas
    {
        public List<string> lines { get; set; }
        public List<texts> texts { get; set; }
        public List<imgs> imgs { get; set; }
    }

    public class texts
    {
        public string name { get; set; }
        public string value { get; set; }
    }

    public class imgs
    {
        public string name { get; set; }
        public Image img { get; set; }
    }
}
