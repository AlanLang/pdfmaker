using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pdfmaker
{
    class DocConfig
    {
        public string basefont { get; set; }
        public float width { get; set; }
        public float height { get; set; }

        public List<img> img { get; set; }
        public List<text> text { get; set; }
        public List<line> line { get; set; }
    }

    class img
    {
        public string name { get; set; }
        public string type { get; set; }
        public float width { get; set; }
        public float height { get; set; }
        public float x { get; set; }
        public float y { get; set; }
    }

    class text
    {
        public string name { get; set; }
        public int size { get; set; }
        public int spacing { get; set; }
        public float x { get; set; }
        public float y { get; set; }
    }

    class line
    {
        public string name { get; set; }
        public float sx { get; set; }
        public float sy { get; set; }
        public float ex { get; set; }
        public float ey { get; set; }
    }
}
