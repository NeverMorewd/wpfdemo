using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Thunisoft.Demo.Forms
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
            this.Load += TestForm_Load;
            axGWQ1.OnAfterGWQ_SignName += AxGWQ1_OnAfterGWQ_SignName;
            axGWQ1.OnAfterGWQ_ReadFingerprint += AxGWQ1_OnAfterGWQ_ReadFingerprint;
        }

        private void AxGWQ1_OnAfterGWQ_ReadFingerprint(object sender, AxGWQLib._DGWQEvents_OnAfterGWQ_ReadFingerprintEvent e)
        {
            if (e.errorCode == 0)
            {
                MessageBox.Show("指纹采集成功！");
            }
        }

        private void AxGWQ1_OnAfterGWQ_SignName(object sender, AxGWQLib._DGWQEvents_OnAfterGWQ_SignNameEvent e)
        {
            if (e.errorCode == 0)
            {
                MessageBox.Show("签名成功！");
            }
            axGWQ1.DoGWQ_ReadFingerprint(60,"finger.png");
        }

        private void TestForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var re = axGWQ1.DoGWQ_SignName("h3w", "sign.png","reserve");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var re = axGWQ1.DoGWQ_ReadFingerprint(60, "fingerprint.png");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            axGWQ1.GWQ_CancelOperate();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Image img1 = Image.FromFile("sign.png");
            Image img2 = Image.FromFile("fingerprint.png");
            CombinImage2(img1, img2);
        }
        static private void CombinImage2(Image Img1, Image Img2)
        {
#if FALSE  
            //控制台调用
             const string folder = @"F:\测试图片";
            Image img1 = Image.FromFile(Path.Combine(folder, "测试1.png"));
            Image img2 = Image.FromFile(Path.Combine(folder, "测试2.png"));
            JoinImage(img1, img2);
#endif
            int imgHeight = 0, imgWidth = 0;
            imgWidth = Img1.Width + Img2.Width;
            imgHeight = Math.Max(Img1.Height, Img2.Height);
            Bitmap joinedBitmap = new Bitmap(imgWidth, imgHeight);
            Graphics graph = Graphics.FromImage(joinedBitmap);
            graph.DrawImage(Img1, 0, 0, Img1.Width, Img1.Height);
            graph.DrawImage(Img2, Img1.Width, 0, Img2.Width, Img2.Height);
            Image img = joinedBitmap;
            //保存
            img.Save("result.png");
            img.Dispose();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string re = axGWQ1.GWQ_GetIP();
            label_ip.Text = string.IsNullOrEmpty(re) ? string.Empty : re;
        }
    }
}
