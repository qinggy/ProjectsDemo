using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace OperationPdf
{
    class Program
    {
        static float pageWidth = 594.0f;
        static float pageDepth = 828.0f;
        static float pageMargin = 30.0f;
        static float fontSize = 20.0f;
        static float leadSize = 10.0f;
        static StreamWriter pPDF = new StreamWriter(@"D:\12.pdf");
        static MemoryStream mPDF = new MemoryStream();

        static void Main(string[] args)
        {
            //ConvertExcelTextToPDF(@"D:\12.xlsx", @"D:\12.pdf");
            ArrayList xRefs = new ArrayList();
            float yPos = 0f;
            long streamStart = 0;
            long streamEnd = 0;
            long streamLen = 0;
            string strPDFMessage = null;
            //PDF文档头信息
            strPDFMessage = "%PDF-1.1\n";
            ConvertToByteAndAddtoStream(strPDFMessage);
            xRefs.Add(mPDF.Length);
            strPDFMessage = "1 0 obj\n";
            ConvertToByteAndAddtoStream(strPDFMessage);
            strPDFMessage = "<< /Length 2 0 R >>\n";
            ConvertToByteAndAddtoStream(strPDFMessage);
            strPDFMessage = "stream\n";
            ConvertToByteAndAddtoStream(strPDFMessage);
            ////////PDF文档描述
            streamStart = mPDF.Length;
            //字体
            strPDFMessage = "BT\n/F0 " + fontSize + " Tf\n";
            ConvertToByteAndAddtoStream(strPDFMessage);
            //PDF文档实体高度
            yPos = pageDepth - pageMargin;
            strPDFMessage = pageMargin + " " + yPos + " Td\n";
            ConvertToByteAndAddtoStream(strPDFMessage);
            strPDFMessage = leadSize + " TL\n";
            ConvertToByteAndAddtoStream(strPDFMessage);
            //实体内容
            strPDFMessage = "(" + GetExcelContent(@"D:\用电能耗月报表模板.xlsx") + ")Tj\n";
            ConvertToByteAndAddtoStream(strPDFMessage);
            strPDFMessage = "ET\n";
            ConvertToByteAndAddtoStream(strPDFMessage);
            streamEnd = mPDF.Length;
            streamLen = streamEnd - streamStart;
            strPDFMessage = "endstream\nendobj\n";
            ConvertToByteAndAddtoStream(strPDFMessage);
            //PDF文档的版本信息
            xRefs.Add(mPDF.Length);
            strPDFMessage = "2 0 obj\n" + streamLen + "\nendobj\n";
            ConvertToByteAndAddtoStream(strPDFMessage);
            xRefs.Add(mPDF.Length);
            strPDFMessage = "3 0 obj\n<</Type/Page/Parent 4 0 R/Contents 1 0 R>>\nendobj\n";
            ConvertToByteAndAddtoStream(strPDFMessage);
            xRefs.Add(mPDF.Length);
            strPDFMessage = "4 0 obj\n<</Type /Pages /Count 1\n";
            ConvertToByteAndAddtoStream(strPDFMessage);
            strPDFMessage = "/Kids[\n3 0 R\n]\n";
            ConvertToByteAndAddtoStream(strPDFMessage);
            strPDFMessage = "/Resources<</ProcSet[/PDF/Text]/Font<</F0 5 0 R>> >>\n";
            ConvertToByteAndAddtoStream(strPDFMessage);
            strPDFMessage = "/MediaBox [ 0 0 " + pageWidth + " " + pageDepth + " ]\n>>\nendobj\n";
            ConvertToByteAndAddtoStream(strPDFMessage);
            xRefs.Add(mPDF.Length);
            strPDFMessage = "5 0 obj\n<</Type/Font/Subtype/Type1/BaseFont/Courier/Encoding/WinAnsiEncoding>>\nendobj\n";
            ConvertToByteAndAddtoStream(strPDFMessage);
            xRefs.Add(mPDF.Length);
            strPDFMessage = "6 0 obj\n<</Type/Catalog/Pages 4 0 R>>\nendobj\n";
            ConvertToByteAndAddtoStream(strPDFMessage);
            streamStart = mPDF.Length;
            strPDFMessage = "xref\n0 7\n0000000000 65535 f \n";
            for (int i = 0; i < xRefs.Count; i++)
            {
                strPDFMessage += xRefFormatting((long)xRefs[i]) + " 00000 n \n";
            }

            ConvertToByteAndAddtoStream(strPDFMessage);
            strPDFMessage = "trailer\n<<\n/Size " + (xRefs.Count + 1) + "\n/Root 6 0 R\n>>\n";
            ConvertToByteAndAddtoStream(strPDFMessage);
            strPDFMessage = "startxref\n" + streamStart + "\n%%EOF\n";
            ConvertToByteAndAddtoStream(strPDFMessage);
            mPDF.WriteTo(pPDF.BaseStream);
            mPDF.Close();
            pPDF.Close();

            Console.ReadKey();
        }

        static string GetExcelContent(string sourceUri)
        {
            using (var fs = new FileStream(sourceUri, FileMode.Open, FileAccess.Read))
            {
                try
                {
                    byte[] buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, (int)fs.Length);

                    string excelContent = Encoding.UTF8.GetString(buffer);

                    return excelContent;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        static void ConvertExcelTextToPDF(string sourceUri, string targetUri)
        {
            var content = GetExcelContent(sourceUri);
            if (string.IsNullOrEmpty(content))
            {
                return;
            }

            MemoryStream outputStream = new MemoryStream();//要把PDF写到哪个串流
            byte[] data = Encoding.UTF8.GetBytes(content);//字串转成byte[]
            MemoryStream msInput = new MemoryStream(data);
            Document doc = new Document();//要写PDF的文件，建构子没填的话预设直式A4
            PdfWriter writer = PdfWriter.GetInstance(doc, outputStream);
            //指定文件预设开档时的缩放为100%

            PdfDestination pdfDest = new PdfDestination(PdfDestination.XYZ, 0, doc.PageSize.Height, 1f);
            //开启Document文件 
            doc.Open();

            //使用XMLWorkerHelper把Html parse到PDF档里
            XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, msInput, null, Encoding.UTF8, new UnicodeFontFactory());
            //将pdfDest设定的资料写到PDF档
            PdfAction action = PdfAction.GotoLocalPage(1, pdfDest, writer);
            writer.SetOpenAction(action);
            doc.Close();
            msInput.Close();
            outputStream.Close();

            File.WriteAllBytes(targetUri, outputStream.ToArray());
        }

        //设置字体类
        public class UnicodeFontFactory : FontFactoryImp
        {
            private static readonly string arialFontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arialuni.ttf");//arial unicode MS是完整的unicode字型。
            private static readonly string 标楷体Path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "KAIU.TTF");//标楷体

            public override Font GetFont(string fontname, string encoding, bool embedded, float size, int style, BaseColor color, bool cached)
            {
                BaseFont bfChiness = BaseFont.CreateFont(@"C:\Windows\Fonts\simhei.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                //可用Arial或标楷体，自己选一个
                BaseFont baseFont = BaseFont.CreateFont(标楷体Path, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                return new Font(baseFont, size, style, color);
            }
        }

        static void ConvertToByteAndAddtoStream(string strMsg)
        {
            Byte[] buffer = null;
            buffer = ASCIIEncoding.ASCII.GetBytes(strMsg);
            mPDF.Write(buffer, 0, buffer.Length);
            buffer = null;
        }

        static string xRefFormatting(long xValue)
        {
            string strMsg = xValue.ToString();
            int iLen = strMsg.Length;
            if (iLen < 10)
            {
                StringBuilder s = new StringBuilder();
                int i = 10 - iLen;
                s.Append('0', i);
                strMsg = s.ToString() + strMsg;
            }

            return strMsg;
        }
    }
}
