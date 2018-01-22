# pdfmaker
根据配置生成pdf
## 1 编辑配置文件，类型为 `yml`，例如：
```
# 文档高度
width: 180
# 文档宽度
height: 90

basefont: simsun.ttc

img: 
  - name: img1
    # type: img/qrcode/brcode
    type: qrcode
    width: 70
    height: 70
    x: 55
    y: 15

line:
  - name: line1
    sx: 0
    sy: 0
    ex: 180
    ey: 90

text: 
  - name: text1
    x: 40
    y: 5
    #size: 
    #spacing:
```
## 2 引用pdfmaker
添加显示的内容和配置文件的地址，（namevalue里的name要和配置文件里的name对应），例如：

```C#
NameValueCollection nav = new NameValueCollection();
nav.Add("img1", "https://qr.alipay.com/tsx04136zdm6mxp7jvv4s2f");
nav.Add("text1", "改需求推荐使用支付宝");
string path = AppDomain.CurrentDomain.BaseDirectory + "config.yml";

pdfmaker.pdfmaker make = new pdfmaker.pdfmaker();
make.MakePdf(nav, path, "测试1.pdf");
```

## 3 生成的pdf文件如下：
![](http://oqdzx28cd.bkt.clouddn.com/18-1-22/17978729.jpg)