using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf.parser;
using iTextSharp.text.pdf;
using PdfSharp.Charting;
using PdfSharp.Drawing;
using System.Diagnostics;

namespace FlipEBookCreatorApplication
{
    public partial class MergeFilesTest : Form
    {
        public MergeFilesTest()
        {
            InitializeComponent();
         

            
        }
        private static string [] GetFiles()
        {
            //https://www.youtube.com/watch?v=FHAPcPxY4So
            DirectoryInfo di = new DirectoryInfo(@"C: \Users\Arfa\Desktop\MyBooks\test");

            FileInfo []files = di.GetFiles("*.pdf");

            int i = 0;
            string[] names = new string[files.Length];
            foreach (var r in files)
            {
                names[i] = r.FullName;
                i = i + 1;

            }
            return names;







        }



        private void button1_Click(object sender, EventArgs e)
        {
            string[] files = GetFiles();
            PdfSharp.Pdf.PdfDocument outputDoc = new PdfSharp.Pdf.PdfDocument();
            foreach (string file in files)
            {
                PdfSharp.Pdf.PdfDocument inputDoc = PdfSharp.Pdf.IO.PdfReader.Open(file, PdfDocumentOpenMode.Import);
                int count = inputDoc.PageCount;
                for (int idx = 0; idx < count; idx++)
                {
                    PdfSharp.Pdf.PdfPage page = inputDoc.Pages[idx];
                    outputDoc.AddPage(page);

                }

            }
            const string b = "testBook.pdf";
            const string filename = @"C: \Users\Arfa\Desktop\MyBooks\test"+"\\"+b;
            outputDoc.Save(filename);//save the file

            MessageBox.Show("Book is created ");

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //extract files
            StringBuilder sb = new StringBuilder();
            string file = @"file:///C:/Users/Arfa/Desktop/MyBooks/test/test12.pdf";
            using (iTextSharp.text.pdf.PdfReader reader1 =new iTextSharp.text.pdf.PdfReader(file)) {
                for (int pageno=1;pageno<=reader1.NumberOfPages;pageno++)
                {

                    ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                    string text = PdfTextExtractor.GetTextFromPage(reader1,pageno,strategy);
                    text = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default,Encoding.UTF8,Encoding.Default.GetBytes(text)));

                    sb.Append(text);


                }

            }

            MessageBox.Show(sb.ToString());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //https://www.youtube.com/watch?v=VzbhLnJYkcA

            Document doc = new Document();

            



            string path = @"C:/Users/Arfa/Desktop/MyBooks/test/test11.pdf";
            
            PdfWriter.GetInstance(doc, new FileStream(path, FileMode.Create));
            Boolean flag = true;

            doc.Open();
            Paragraph p1 = new Paragraph("image here");
            doc.Add(p1);
            //choose image
            OpenFileDialog open = new OpenFileDialog();
            // image filters  
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                
                string filePath = open.FileName;



                //Add the Image file to the PDF document object.
                iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(filePath);
                doc.Add(img);
                doc.Close();
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
           extractFiles();
            
        }
        private void extractFiles()
        {

            int count = 7;
            int i = 1;
            //extract files
            StringBuilder sb = new StringBuilder();
            while (i <= count)
            {



                string file = @"file:///C:/Users/Arfa/Desktop/MyBooks/test/test1" + i + ".pdf";
                using (iTextSharp.text.pdf.PdfReader reader1 = new iTextSharp.text.pdf.PdfReader(file))
                {
                    for (int pageno = 1; pageno <= reader1.NumberOfPages; pageno++)
                    {

                        ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                        string text = PdfTextExtractor.GetTextFromPage(reader1, pageno, strategy);
                        text = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(text)));

                        sb.Append("\n "+text+"\n ");
                        MessageBox.Show("Merge file "+i);

                    }

                }


                i++;

            }//end while

            MessageBox.Show(sb.ToString());
        }


        private void WriteImage()
        {

            Document doc = new Document();

            int count = 7;
            int i = 1;
            //extract files
            StringBuilder sb = new StringBuilder();
            string path = @"C:/Users/Arfa/Desktop/MyBooks/test/test1123.pdf";

            PdfWriter.GetInstance(doc, new FileStream(path, FileMode.Create));
            Boolean flag = true;
            OpenFileDialog open = new OpenFileDialog();
            doc.Open();
           

                //   string filePath = @"C:/Users/Arfa/Desktop/MyBooks/test/test1" + i + ".pdf";//wrong must image
                open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
                if (open.ShowDialog() == DialogResult.OK)
                {

                    string filePath = open.FileName;



                    //Add the Image file to the PDF document object.
                    iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(filePath);
                    doc.Add(img);
                }
                
                
               

            
            doc.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            PdfSharp.Pdf.PdfDocument document = new PdfSharp.Pdf.PdfDocument();
            document.Info.Title = "Created with PDFsharp";
            PdfSharp.Pdf.PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont font = new XFont("Verdana", 20, XFontStyle.BoldItalic);
            gfx.DrawString("Hello, World!", font, XBrushes.Black,
            new XRect(0, 0, page.Width, page.Height),

            XStringFormats.Center);
            Chart chart = CombinationChart();

        
            ChartFrame chartFrame = new ChartFrame();
            chartFrame.Location = new XPoint(30, 30);
            chartFrame.Size = new XSize(500, 200);
            chartFrame.Add(chart);
            chartFrame.Draw(gfx);

            const string filename = "HelloWorld.pdf";

            document.Save(filename);

            Process.Start(filename);


          

        }

        public static Chart CombinationChart()
        {
            Chart chart = new Chart();
            Series series = chart.SeriesCollection.AddSeries();
            series.ChartType = ChartType.Column2D;
            series.Add(new double[] { 1, 17, 45, 5, 3, 20, 11, 23, 8, 19 });
            series.HasDataLabel = true;

            series = chart.SeriesCollection.AddSeries();
            series.ChartType = ChartType.Line;
            series.Add(new double[] { 41, 7, 5, 45, 13, 10, 21, 13, 18, 9 });

            XSeries xseries = chart.XValues.AddXSeries();
            xseries.Add("A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N");

            chart.XAxis.MajorTickMark = TickMarkType.Outside;
            chart.XAxis.Title.Caption = "X-Axis";

            chart.YAxis.MajorTickMark = TickMarkType.Outside;
            chart.YAxis.HasMajorGridlines = true;

            chart.PlotArea.LineFormat.Color = XColors.DarkGray;
            chart.PlotArea.LineFormat.Width = 1;
            chart.PlotArea.LineFormat.Visible = true;

            chart.Legend.Docking = DockingType.Left;
            chart.Legend.LineFormat.Visible = true;

            return chart;
        }
    }
}
