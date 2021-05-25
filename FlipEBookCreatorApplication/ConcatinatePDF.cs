using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace FlipEBookCreatorApplication
{
    public partial class ConcatinatePDF : Form
    {
        public ConcatinatePDF()
        {
            InitializeComponent();
        }
        //http://pdfsharp.com/PDFsharp/index.php?option=com_content&task=view&id=52&Itemid=60
        //http://pdfsharp.com/PDFsharp/index.php?option=com_content&task=view&id=24&Itemid=35
        //http://www.pdfsharp.net/
        private void button1_Click(object sender, EventArgs e)
        {
            Variant1();
            Variant2();
            Variant3();
            Variant4();
        }

        /// <summary>
        /// Put your own code here to get the files to be concatenated.
        /// </summary>
        static string[] GetFiles()
        {
            DirectoryInfo dirInfo = new DirectoryInfo(@"C:\Users\Arfa\Desktop\MyBooks\Programming with C");
            FileInfo[] fileInfos = dirInfo.GetFiles("*.pdf");
            ArrayList list = new ArrayList();
            foreach (FileInfo info in fileInfos)
            {
                // HACK: Just skip the protected samples file...
                if (info.Name.IndexOf("protected") == -1)
                    list.Add(info.FullName);
            }
            return (string[])list.ToArray(typeof(string));
        }

        /// <summary>
        /// Imports all pages from a list of documents.
        /// </summary>
        static void Variant1()
        {
            // Get some file names
            string[] files = GetFiles();

            // Open the output document
            PdfDocument outputDocument = new PdfDocument();

            // Iterate files
            foreach (string file in files)
            {
                // Open the document to import pages from it.
                PdfDocument inputDocument = PdfReader.Open(file, PdfDocumentOpenMode.Import);

                // Iterate pages
                int count = inputDocument.PageCount;
                for (int idx = 0; idx < count; idx++)
                {
                    // Get the page from the external document...
                    PdfPage page = inputDocument.Pages[idx];
                    // ...and add it to the output document.
                    outputDocument.AddPage(page);
                }
            }

            // Save the document...
            string filename = "ConcatenatedDocument1.pdf";
            outputDocument.Save(filename);
            // ...and start a viewer.
            Process.Start(filename);
        }

        /// <summary>
        /// This sample adds each page twice to the output document. The output document
        /// becomes only a little bit larger because the content of the pages is reused 
        /// and not duplicated.
        /// </summary>
        static void Variant2()
        {
            // Get some file names
            string[] files = GetFiles();

            // Open the output document
            PdfDocument outputDocument = new PdfDocument();

            // Show consecutive pages facing. Requires Acrobat 5 or higher.
            outputDocument.PageLayout = PdfPageLayout.TwoColumnLeft;
           // outputDocument.PageLayout = PdfPageLayout.OneColumn;
            // Iterate files
            foreach (string file in files)
            {
                // Open the document to import pages from it.
                PdfDocument inputDocument = PdfReader.Open(file, PdfDocumentOpenMode.Import);

                // Iterate pages
                int count = inputDocument.PageCount;
                for (int idx = 0; idx < count; idx++)
                {
                    // Get the page from the external document...
                    PdfPage page = inputDocument.Pages[idx];
                    // ...and add them twice to the output document.
                    outputDocument.AddPage(page);
                    outputDocument.AddPage(page);
                }
            }

            // Save the document...
            string filename = "ConcatenatedDocument2.pdf";
            outputDocument.Save(filename);
            // ...and start a viewer.
            Process.Start(filename);
        }

        /// <summary>
        /// This sample adds a consecutive number in the middle of each page.
        /// It shows how you can add graphics to an imported page.
        /// </summary>
        static void Variant3()
        {
            // Get some file names
            string[] files = GetFiles();

            // Open the output document
            PdfDocument outputDocument = new PdfDocument();

            // Note that the output document may look significant larger than in Variant1.
            // This is because adding graphics to an imported page causes the 
            // uncompression of its content if it was compressed in the external document.
            // To compare file sizes you should either run the sample as Release build
            // or uncomment the following line.
            //outputDocument.Options.CompressContentStreams = true;

            PdfSharp.Drawing.XFont font = new XFont("Verdana", 40, XFontStyle.Bold);
            XStringFormat format = XStringFormat.Center;
            int number = 0;

            // Iterate files
            foreach (string file in files)
            {
                // Open the document to import pages from it.
                PdfDocument inputDocument = PdfReader.Open(file, PdfDocumentOpenMode.Import);

                // Iterate pages
                int count = inputDocument.PageCount;
                for (int idx = 0; idx < count; idx++)
                {
                    // Get the page from the external document...
                    PdfPage page = inputDocument.Pages[idx];
                    // ...and add it to the output document.
                    // Note that the PdfPage instance returned by AddPage is a
                    // different object.
                    page = outputDocument.AddPage(page);

                    // Create a graphics object for this page. To draw beneath the existing
                    // content set 'Append' to 'Prepend'.
                    XGraphics gfx =
                      XGraphics.FromPdfPage(page, XGraphicsPdfPageOptions.Append);
                   // DrawNumber(gfx, font, ++number);
                }
            }

            // Save the document...
            string filename = "ConcatenatedDocument3.pdf";
            outputDocument.Save(filename);
            // ...and start a viewer.
            Process.Start(filename);
        }

        /// <summary>
        /// This sample is the combination of Variant2 and Variant3. It shows that you 
        /// can add external pages more than once and still add individual graphics on
        /// each page. The external content is shared among the pages, the new graphics
        /// are unique to each page. You can check this by comparing the file size
        /// of Variant3 and Variant4.
        /// </summary>
        static void Variant4()
        {
            // Get some file names
            string[] files = GetFiles();

            // Open the output document
            PdfDocument outputDocument = new PdfDocument();

            // For checking the file size uncomment next line.
            //outputDocument.Options.CompressContentStreams = true;

            XFont font = new XFont("Verdana", 40, XFontStyle.Bold);
            XStringFormat format = XStringFormat.Center;
            int number = 0;

            // Iterate files
            foreach (string file in files)
            {
                // Open the document to import pages from it.
                PdfDocument inputDocument = PdfReader.Open(file, PdfDocumentOpenMode.Import);

                // Show consecutive pages facing. Requires Acrobat 5 or higher.
                outputDocument.PageLayout = PdfPageLayout.TwoColumnLeft;

                // Iterate pages
                int count = inputDocument.PageCount;
                for (int idx = 0; idx < count; idx++)
                {
                    // Get the page from the external document...
                    PdfPage page = inputDocument.Pages[idx];
                    // ...and add it twice to the output document.
                    PdfPage page1 = outputDocument.AddPage(page);
                    PdfPage page2 = outputDocument.AddPage(page);

                    PdfSharp.Drawing.XGraphics gfx =
                      XGraphics.FromPdfPage(page1, XGraphicsPdfPageOptions.Append);
                  //  DrawNumber(gfx, font, ++number);

                    gfx = XGraphics.FromPdfPage(page2, XGraphicsPdfPageOptions.Append);
                   // DrawNumber(gfx, font, ++number);
                }
            }

            // Save the document...
            string filename = "ConcatenatedDocument4.pdf";
            outputDocument.Save(filename);
            // ...and start a viewer.
            Process.Start(filename);
        }

        ///// <summary>
        ///// Draws the number in the middle of the page.
        ///// </summary>
        //static void DrawNumber(XGraphics gfx, XFont font, int number)
        //{
        //    double width = 130;
        //    gfx.DrawEllipse(new XPen(XColors.DarkBlue, 7), XBrushes.DarkOrange,
        //      new XRect((gfx.PageSize.ToXPoint() - new XSize(width, width)) / 2,
        //                new XSize(width, width)));
        //    gfx.DrawString(number.ToString(), font, XBrushes.Firebrick,
        //      new XRect(XPoint.Empty, gfx.PageSize), XStringFormat.Center);
        //}






    }
}
