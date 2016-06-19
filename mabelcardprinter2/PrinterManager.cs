using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Printing;
using System.Threading;

namespace MabelCardPrinter
{
    public class PrinterEventArgs : EventArgs
    {
        public MabelCard Card;
        public string Status;
        public PrinterInfo Info;
        public PrinterEventArgs(MabelCard _card, string _status, PrinterInfo _info)
        {
            this.Card = _card;
            this.Status = _status;
            this.Info = _info;
        }
    }

    public delegate void PrinterEventHander(object sender, PrinterEventArgs e);

    class PrinterManager
    {
        private MabelAPI mabel_api;
        private int printer_id;
        private string printer_location;
        private string printer_model;
        private string printer_name;
        private MagiCardAPI magi_api;
        private PrinterInfo info;
        private bool isRunning;
        public event PrinterEventHander PrintingCard;
        public event PrinterEventHander ErrorCard;
        public event PrinterEventHander WaitingCard;
        public event PrinterEventHander PrintedCard;
        public event PrinterEventHander Registered;
        public event PrinterEventHander Unregistered;
        public event PrinterEventHander Checking;

        protected virtual void OnPrinting(PrinterEventArgs e)
        {
            if (PrintingCard != null)
            {
                PrintingCard(this, e);
            }
        }
        protected virtual void OnChecking(PrinterEventArgs e)
        {
            if (Checking != null)
            {
                Checking(this, e);
            }
        }
        protected virtual void OnRegistered(PrinterEventArgs e)
        {
            if (Registered != null)
            {
                Registered(this, e);
            }
        }

        protected virtual void OnUnregistered(PrinterEventArgs e)
        {
            if (Unregistered != null)
            {
                Unregistered(this, e);
            }
        }
        protected virtual void OnError(PrinterEventArgs e)
        {
            if (ErrorCard != null)
            {
                ErrorCard(this, e);
            }
        }

        protected virtual void OnWaiting(PrinterEventArgs e)
        {
            if (WaitingCard != null)
            {
                WaitingCard(this, e);
            }
        }

        protected virtual void OnPrinted(PrinterEventArgs e)
        {
            if (PrintedCard != null)
            {
                PrintedCard(this, e);
            }
        }

        public bool register()
        {
            
            PrintDocument printDoc = new PrintDocument();

            string selectedPrinter = "";
            foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                if (printer.Contains("Enduro"))
                {
                    selectedPrinter = printer;
                }
            }
            printDoc.PrinterSettings.PrinterName = selectedPrinter;

            try
            {
                magi_api = new MagiCardAPI(printDoc.PrinterSettings.CreateMeasurementGraphics().GetHdc());
                magi_api.EnableReporting();
                info = magi_api.GetEnduroStatus();
                this.printer_model = new string(info.sModel);
            }
            catch (Exception e)
            {
                OnError(new PrinterEventArgs(null, "Error connected to card: " + e.Message, null));
            }
            
            MabelResponse resp = mabel_api.RegisterPrinter(printer_id, printer_name, printer_location, printer_model);
            if (resp.isError)
            {
                OnError(new PrinterEventArgs(null, "Register failed " + resp.message, info));
                return false;
            }
            else
            {
                OnRegistered(new PrinterEventArgs(null,"Registered successfully",info));
                return true;
            }
        }
        
        public bool unregister()
        {
            MabelResponse resp = mabel_api.UnregisterPrinter(printer_id);
            if (resp.isError)
            {
                OnError(new PrinterEventArgs(null, "Unegister failed " + resp.message, info));
                return false;
            }
            else
            {
                OnUnregistered(new PrinterEventArgs(null, "Unregistered Successfully", info));
                return true;
            }
        }

        public PrinterManager(int printer_id, string printer_name, string printer_location)
        {
            this.isRunning = false;
            this.printer_id = printer_id;
            this.printer_name = printer_name;
            this.printer_location = printer_location;
            mabel_api = new MabelAPI();
        }

        public PrinterInfo GetPrinterInfo()
        {
            return magi_api.GetEnduroStatus();
        }

        public void doWork()
        {
            if (!isRunning)
            {
                runBatch();
            }
        }


        private void runBatch()
        {
            MabelCard card;
            String rfidToken;
            mabel_api = new MabelAPI();
            isRunning = true;
            OnChecking(new PrinterEventArgs(null, "Checking", info));
            do
            {
                card = mabel_api.GetNextJob(printer_id);
                if (card != null)
                {
                    mabel_api.SetCardStatus(printer_id, card, "PRINTING");
                    PrintableCard printedCard = new PrintableCard(card, magi_api);
                    OnPrinting(new PrinterEventArgs(card, "Printing", magi_api.GetEnduroStatus()));
                    try
                    {
                        printedCard.PrintCard();
                        if (Properties.Settings.Default.RFIDEnabled)
                        {
                            OnWaiting(new PrinterEventArgs(card, "Waiting", magi_api.GetEnduroStatus()));
                            magi_api.Wait();
                            rfidToken = Console.ReadLine();
                            mabel_api.SetCardRfid(printer_id, card, rfidToken);
                        }
                        else
                        {
                            //System.Threading.Thread.Sleep(45000);
                            OnWaiting(new PrinterEventArgs(card, "Waiting", magi_api.GetEnduroStatus()));
                            magi_api.Wait();
                        }
                        System.Console.WriteLine("Done waiting");
                        OnPrinted(new PrinterEventArgs(card, "Printed", magi_api.GetEnduroStatus()));
                        mabel_api.SetCardStatus(printer_id, card, "PRINTED");
                        mabel_api.SetCardPrinted(printer_id, card);
                    }
                    catch (Exception e)
                    {
                        OnError(new PrinterEventArgs(card, "ERROR: " + e.Message + magi_api.GetLastError(), magi_api.GetEnduroStatus()));
                        mabel_api.SetCardStatus(printer_id, card, "PRINTER_ERROR|" +  e.Message + magi_api.GetLastError());
                    }
                }
            } while (card != null);
            isRunning = false;
        }
    }
}
