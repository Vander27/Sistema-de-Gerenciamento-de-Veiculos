using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using iTextSharp.tool.xml.html;
using iTextSharp.tool.xml.parser;
using iTextSharp.tool.xml.pipeline.css;
using iTextSharp.tool.xml.pipeline.end;
using iTextSharp.tool.xml.pipeline.html;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Projeto.Presentation.Utils
{
    public class ReportsUtil
    {
        //método para receber um conteudo string
        //e retorna-lo em formato de dados PDF
        public byte[] GetPDF(string conteudo)
        {
            byte[] pdf = null;

            MemoryStream ms = new MemoryStream();
            TextReader reader = new StringReader(conteudo);

            Document doc = new Document(PageSize.A4, 50, 50, 50, 50);
            PdfWriter writer = PdfWriter.GetInstance(doc, ms);
            HTMLWorker html = new HTMLWorker(doc);

            doc.Open();
            html.StartDocument();
            html.Parse(reader);

            html.EndDocument();
            html.Close();
            doc.Close();

            pdf = ms.ToArray();
            return pdf;
        }

        //método para receber um conteudo string
        //e retorna-lo em formato de dados PDF
        public byte[] GetPDF(string conteudo, string cssFile)
        {
            Document doc = new Document(PageSize.A4, 50, 50, 50, 50);

            using (var ms = new MemoryStream())
            {
                var writer = PdfWriter.GetInstance(doc, ms);
                writer.CloseStream = false;

                doc.Open();

                var htmlContext = new HtmlPipelineContext(null);
                htmlContext.SetTagFactory(Tags.GetHtmlTagProcessorFactory());

                var cssResolver = XMLWorkerHelper.GetInstance().GetDefaultCssResolver(false);
                cssResolver.AddCssFile(cssFile, true);

                var pipeline = new CssResolverPipeline(cssResolver, new HtmlPipeline(htmlContext,
                                new PdfWriterPipeline(doc, writer)));

                var worker = new XMLWorker(pipeline, true);
                var xmlParser = new XMLParser(worker);
                xmlParser.Parse(new MemoryStream(Encoding.UTF8.GetBytes(conteudo)));

                doc.Close();

                return ms.GetBuffer();
            }
        }
    }
}