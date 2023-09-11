using FellowOakDicom;
using FellowOakDicom.Imaging;
using FellowOakDicom.Imaging.NativeCodec;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace DXApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                new DicomSetupBuilder()
                    .RegisterServices(s => 
                        s.AddFellowOakDicom()
                            .AddImageManager<WinFormsImageManager>()
                            .AddTranscoderManager<NativeTranscoderManager>())
                    .SkipValidation()
                    .Build();

                var image = new DicomImage(
                    Path.Combine(
                        AppDomain.CurrentDomain.BaseDirectory,
                        "dcm\\02.DCM"));

                var bitmap = image.RenderImage().As<Bitmap>();

                pictureBox1.Image = bitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
