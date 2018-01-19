using iTextSharp.text;
using iTextSharp.text.pdf;
using SharpYaml.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pdfmaker
{
    public class pdfmaker
    {
        public pdfmaker()
        {

        }

        public bool MakePdf(PdfDatas data, string configPath, string pdfPath)
        {
            List<PdfDatas> datas = new List<PdfDatas>();
            datas.Add(data);
            return MakePdf(datas, configPath, pdfPath);
        }

        public bool MakePdf(List<PdfDatas> datas, string configPath, string pdfPath)
        {
            if (string.IsNullOrEmpty(configPath))
            {
                _code = 1;
                _err = "未定义配置文件";
                return false;
            }
            FileInfo fi = new FileInfo(configPath);
            if (!fi.Exists)
            {
                _code = 1;
                _err = $"在路径{configPath}找不到配置文件";
                return false;
            }
            using (FileStream fs = new FileStream(configPath, FileMode.Open, FileAccess.Read))
            {
                try
                {
                    var serializer = new Serializer();
                    docConfig = serializer.Deserialize<DocConfig>(fs);
                }
                catch (Exception ex)
                {
                    _code = 1;
                    _err = "配置文件解析失败：" + ex.Message;
                    return false;
                }
            }
            if (datas.Count == 0)
            {
                _code = 1;
                _err = "数据集合为空，无法生成pdf";
                return false;
            }
            else
            {
                pdfDatas = datas;
            }

            using(Document document = new Document(new Rectangle(docConfig.width, docConfig.height), 0, 0, 0, 0)){
                string path = System.Threading.Thread.GetDomain().BaseDirectory + docConfig.basefont + ",0";
                BaseFont basefont = BaseFont.CreateFont(path, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                NewPath(pdfPath);

                PdfWriter writer = PdfWriter.GetInstance(document, new System.IO.FileStream(pdfPath, System.IO.FileMode.Create));
                document.Open();
                int pageNum = pdfDatas.Count;
                int thisPage = 0;
                bool NoWrite = true;
                foreach (var pdfdata in pdfDatas)
                {
                    thisPage++;
                    if (pdfdata.lines != null && pdfdata.lines.Count > 0)
                    {
                        foreach (var lineName in pdfdata.lines)
                        {
                            var line = docConfig.line.Where(a => a.name == lineName).ToList();
                            if (line.Count > 0)
                            {
                                PdfContentByte cb = writer.DirectContent;
                                cb.MoveTo(line[0].sx, line[0].sy);
                                cb.LineTo(line[0].ex, line[0].ey);
                                cb.Stroke();
                                NoWrite = false;
                            }
                        }

                    }
                    if (pdfdata.texts != null && pdfdata.texts.Count > 0)
                    {
                        foreach (var td in pdfdata.texts)
                        {
                            if (!string.IsNullOrEmpty(td.name) && !string.IsNullOrEmpty(td.value))
                            {
                                var text = docConfig.text.Where(a => a.name == td.name).ToList();
                                if (text.Count > 0)
                                {
                                    int size = 0;
                                    int spacing = 0;
                                    var ctext = text[0];
                                    size = ctext.size > 0 ? ctext.size : fontsize;
                                    spacing = ctext.spacing > 0 ? ctext.spacing : fontspacing;
                                    PdfContentByte cb = writer.DirectContent;
                                    cb.BeginText();
                                    cb.SetFontAndSize(basefont, size);//设定字号 
                                    cb.SetCharacterSpacing(spacing);//设定字间距 

                                    cb.SetRGBColorFill(0, 0, 0);//设定文本颜色 
                                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, td.value, ctext.x, ctext.y, 0);
                                    cb.EndText();
                                    NoWrite = false;

                                }

                            }
                        }
                    }
                    if (pdfdata.imgs != null && pdfdata.imgs.Count > 0)
                    {
                        foreach (var dimg in pdfdata.imgs)
                        {
                            if (!string.IsNullOrEmpty(dimg.name) && dimg.img != null)
                            {
                                var vimg = docConfig.img.Where(a => a.name == dimg.name).ToList();
                                if (vimg.Count > 0)
                                {
                                    var imgconfig = vimg[0];
                                    var immg = iTextSharp.text.Image.GetInstance(dimg.img, BaseColor.BLACK);
                                    immg.SetAbsolutePosition(imgconfig.x, imgconfig.y);//图片位置
                                    immg.ScaleAbsolute(imgconfig.width, imgconfig.height);//图片长宽
                                    document.Add(immg);
                                    NoWrite = false;

                                }
                            }
                        }
                    }
                    if (thisPage < pageNum)
                    {
                        document.NewPage();
                    }
                }
                if (NoWrite)
                {
                    _code = 0;
                    _err = "未填充任何内容";
                    return false;
                }
                if (document.IsOpen())
                {
                    document.Close();
                }
                _pdfpath = pdfpath;
                return true;
            };
            
        }


        private void NewPath(string pDF_PATH)
        {
            FileInfo fi = new FileInfo(pDF_PATH);
            var di = fi.Directory;
            if (!di.Exists)
                di.Create();
            if (fi.Exists)
            {
                fi.Delete();
            }
        }
        #region 属性


        int fontsize = 10;
        int fontspacing = 1;


        private int _code;
        private string _err;
        private string _pdfpath;
        private DocConfig docConfig;
        private List<PdfDatas> pdfDatas;


        public int code
        {
            get
            {
                return _code;
            }
        }
        public string err
        {
            get
            {
                return _err;
            }
        }
        public string pdfpath
        {
            get
            {
                return _pdfpath;
            }
        }
        #endregion

    }
}
