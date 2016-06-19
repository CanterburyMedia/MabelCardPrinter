using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Printing;


namespace MabelCardPrinter
{
    class PrintableCard
    {
        public MabelCard card;
        public MagiCardAPI MagiCardAPI;

        private int pagesPrinted = 0;
        public PrintableCard(MabelCard inCard, MagiCardAPI api)
        {
            this.card = inCard;
            MagiCardAPI = api;
        }

        public bool PrintCard()
        {
            if  ( (card.GetCardBackImage() == null) || (card.GetCardFrontImage() == null) ) 
            {
                throw new Exception("IMAGE_NULL");
            }
            PrintDocument printDoc = new PrintDocument();

            
            string selectedPrinter = "";
            foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                System.Console.WriteLine(printer);
                if (printer.Contains("Enduro"))
                {
                    selectedPrinter = printer;
                }
            }
            System.Console.Out.WriteLine("Selected printer: " + selectedPrinter);
            if (selectedPrinter.Equals(""))
            {
                System.Console.Out.WriteLine("No Enduro printer installed!");
                return false;
            }
            printDoc.PrinterSettings.PrinterName = selectedPrinter;
            if (!printDoc.PrinterSettings.CanDuplex)
            {
                System.Console.Out.WriteLine("Printer can't duplex, oh noo!");
                //return false;
            }
            printDoc.PrinterSettings.Duplex = Duplex.Default;
            printDoc.DefaultPageSettings.PaperSize = new PaperSize("CR80", 213, 337);
            printDoc.DefaultPageSettings.Landscape = true;
            printDoc.DocumentName = "Card-" + card.card_id;

            PrinterResolution pkResolution;
            PrinterResolution targetRes = null;
            for (int i = 0; i < printDoc.PrinterSettings.PrinterResolutions.Count; i++)
            {
                pkResolution = printDoc.PrinterSettings.PrinterResolutions[i];
                System.Console.Out.WriteLine("Resolution : " + pkResolution.Kind + " X : " + pkResolution.X + " Y: " + pkResolution.Y);
                if (pkResolution.Kind.Equals("Custom"))
                {
                    targetRes = pkResolution;
                }
            }
            PrinterInfo info = MagiCardAPI.GetEnduroStatus();
            if (!info.bPrinterConnected)
            {
                throw new Exception("Printer not connected!");
            }
            if (targetRes != null)
            {
                printDoc.DefaultPageSettings.PrinterResolution = targetRes;
            }
            else
            {
                targetRes = printDoc.DefaultPageSettings.PrinterResolution;
            }
            System.Console.Out.WriteLine("Selected : " + targetRes.Kind + " X : " + targetRes.X + " Y: " + targetRes.Y);

            printDoc.PrintPage += new PrintPageEventHandler(printDoc_PrintPage);
            if (printDoc.PrinterSettings.IsValid) {
                printDoc.Print();
                return true;
            } else {
                System.Console.Out.WriteLine("Erp, couldn't set printer settings properly :/");
                return false;
            }
            
        }

        private void printDoc_PrintDone(Object sender, PrintEventArgs e)
        {
            System.Console.Out.WriteLine("Printing done!");
        }

        private void printDoc_PrintPage(Object sender, PrintPageEventArgs e)
        {
            
            if (pagesPrinted == 0)
            {

                // draw the image 
                e.Graphics.DrawImage(card.GetCardFrontImage(),0, 0, 337, 213);//new Rectangle(0, 0, 639, 101));//, 213, 337);
                //e.Graphics.DrawImage(card.GetCardFrontImage(), 0, 0, 1016, 642);
                pagesPrinted++;
                e.HasMorePages = true;
            }
            else
            {
                e.Graphics.DrawImage(card.GetCardBackImage(),0, 0, 337, 213);
                //e.Graphics.DrawImage(card.GetCardBackImage(), 0, 0, 1016, 642);//new Rectangle(0, 0, 213, 101));//, 213, 337);
                // Write the string to encode onto the mag strip
                Font magFont = new Font("Arial", 10, FontStyle.Regular);

                e.Graphics.DrawString("~2,BPI75,MPC5,MGVON,COEH,;" + card.mag_token + "?", magFont, Brushes.Black, 0, 0);
                e.HasMorePages = false;
            }
            
        }
    }
}
