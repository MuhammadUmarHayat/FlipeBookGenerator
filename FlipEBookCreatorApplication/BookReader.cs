using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Font = iTextSharp.text.Font;
using System.IO;



namespace FlipEBookCreatorApplication
{
    public partial class BookReader : Form
    {
        public BookReader()
        {
            InitializeComponent();
        }

        private void BookReader_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            loadFile();

        }


        private void loadFile()
        {
            String source_file = "";
           
            OpenFileDialog open = new OpenFileDialog();
            // image filters  
            //open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            open.Filter = "pdf files (*.pdf) |*.pdf;";
            if (open.ShowDialog() == DialogResult.OK)
            {

                source_file = open.FileName;




            }
            
            // variables
            
            String result = @"C:\Users\Arfa\Desktop\MyBooks\result.pdf";
            //create PdfReader object to read from the source file
            iTextSharp.text.pdf.PdfReader reader = new PdfReader(source_file);
            //create PdfStamper object to add content to the pdf file
            PdfStamper stamper = new PdfStamper(reader, new FileStream(result, FileMode.Create));
            //show navigation symbols in a table
            Font symbol = new Font();
            PdfPTable table = new PdfPTable(4);
            table.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            table.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            Chunk first = new Chunk("\u00AB", symbol);
            first.SetAction(new PdfAction(PdfAction.FIRSTPAGE));
            table.AddCell(new Phrase(first));
            Chunk previous = new Chunk("\u003c", symbol);
            previous.SetAction(new PdfAction(PdfAction.PREVPAGE));
            table.AddCell(new Phrase(previous));
            // Chunk next = new Chunk(((char)174).ToString(), symbol);//"\u25C4"
            Chunk next = new Chunk("\u003e", symbol);//"\u25C4"
            next.SetAction(new PdfAction(PdfAction.NEXTPAGE));
            table.AddCell(new Phrase(next));
            Chunk last = new Chunk("\u00BB", symbol);
            last.SetAction(new PdfAction(PdfAction.LASTPAGE));
            table.AddCell(new Phrase(last));
            table.TotalWidth = 120;
            PdfContentByte pb;
            for (int i = 1; i <= reader.NumberOfPages; i++)
            {
                //Get PdfContentByte object for every page of pdf file
                pb = stamper.GetOverContent(i);
                //add the table of navigation symbols at the bottom of every page
                table.WriteSelectedRows(0, 1, reader.GetPageSize(1).Width / 2, table.TotalHeight + 20, pb);
            }
            stamper.Close();




        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            // image filters  
            //open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {

                axAcroPDF1.src  = open.FileName;




            }
        }


        private void pdfToHTML()
        {


            string source_file = "";

            OpenFileDialog open = new OpenFileDialog();
            // image filters  
            //open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {

                source_file = open.FileName;




            }

            SautinSoft.PdfFocus f = new SautinSoft.PdfFocus();
            f.OpenPdf(source_file);

            if (f.PageCount > 0)
            {
                int result = f.ToHtml(@"C:\Users\Arfa\Desktop\MyBooks\result.html");

                //Open HTML document
                if (result == 0)
                {
                    System.Diagnostics.Process.Start(source_file);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pdfToHTML();
        }
    }
}
