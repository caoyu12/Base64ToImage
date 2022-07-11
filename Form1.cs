using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Base64ToImage
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.richTextBox1.Text))
            {
                this.pictureBox1.Image = Base64ToImg(this.richTextBox1.Text.Trim());
            }
            else
            {
                this.richTextBox1.Text = "Please input base64 string here";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.textBox1.Text))
            {
                this.richTextBox1.Text = FileToBase64(this.textBox1.Text);
            }
            else
            {
                this.textBox1.Text = "Please select a file first";
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            this.textBox1.Text = ofd.FileName;
        }

        private string FileToBase64(string filename)
        {
            string result = string.Empty;

            try
            {
                using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
                {
                    byte[] byteArray = new byte[fs.Length];
                    fs.Read(byteArray, 0, byteArray.Length);
                    result = Convert.ToBase64String(byteArray);
                }
            }
            catch (Exception ex)
            {
                result = $"Error: {ex.Message}";
            }

            return result;
        }
        private Image Base64ToImg(string base64str)
        {
            try
            {
                byte[] arr = Convert.FromBase64String(base64str);
                MemoryStream ms = new MemoryStream(arr);
                Bitmap bmp = new Bitmap(ms);
                this.label3.Text = $"[{DateTime.Now.ToString("yyyyMMdd HH:mm:ss")}] Image convert success:";
                this.label3.ForeColor = Color.Black;

                return bmp;
            }
            catch (Exception ex)
            {
                this.label3.Text = $"Error: {ex.Message}";
                this.label3.ForeColor = Color.Red;
            }

            return null;
        }
    }
}
