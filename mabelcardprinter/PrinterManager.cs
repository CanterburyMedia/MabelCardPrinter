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

    public class DebugEventArgs : EventArgs
    {
        public String url;
        public String message;
        public DebugEventArgs(String url, String message)
        {
            this.url = url;
            this.message = message;
        }
    }

    public delegate void PrinterEventHander(object sender, PrinterEventArgs e);
    public delegate void DebugEventHander(object sender, DebugEventArgs e);
    class PrinterManager
    {
        private MabelAPI mabel_api;
        private int printer_id;
        private string printer_location;
        private string printer_model;
        private string printer_name;
        private MagiCardAPI magi_api;
        private PrinterInfo _printerInfo;
        private bool isRunning;
        public event PrinterEventHander EncodingCard;
        public event PrinterEventHander FeedingCard;
        public event PrinterEventHander EjectingCard;
        public event PrinterEventHander PrintingCard;
        public event PrinterEventHander ErrorCard;
        public event PrinterEventHander WaitingCard;
        public event PrinterEventHander PrintedCard;
        public event PrinterEventHander Registered;
        public event PrinterEventHander Unregistered;
        public event PrinterEventHander Checking;
        public event PrinterEventHander UpdateInfo;
        public event DebugEventHander Debug;

        protected virtual void OnPrinting(PrinterEventArgs e)
        {
            PrintingCard?.Invoke(this, e);
        }

        protected virtual void OnChecking(PrinterEventArgs e)
        {
             Checking?.Invoke(this, e);
        }

        protected virtual void OnRegistered(PrinterEventArgs e)
        {
            Registered?.Invoke(this, e);
        }

        protected virtual void OnUnregistered(PrinterEventArgs e)
        {
            Unregistered?.Invoke(this, e);
        }

        protected virtual void OnError(PrinterEventArgs e)
        {
            ErrorCard?.Invoke(this, e);
        }

        protected virtual void OnWaiting(PrinterEventArgs e)
        {
            WaitingCard?.Invoke(this, e);
        }

        protected virtual void OnPrinted(PrinterEventArgs e)
        {
            PrintedCard?.Invoke(this, e);
        }

        protected virtual void OnUpdateInfo(PrinterEventArgs e)
        {
            UpdateInfo?.Invoke(this, e);
        }

        protected virtual void OnEncodingCard(PrinterEventArgs e)
        {
            EncodingCard?.Invoke(this, e);
        }

        protected virtual void OnFeedingCard(PrinterEventArgs e)
        {
            FeedingCard?.Invoke(this, e);
        }

        protected virtual void OnEjectingCard(PrinterEventArgs e)
        {
            EjectingCard?.Invoke(this, e);
        }

        protected virtual void OnDebug(DebugEventArgs e)
        {
            Debug?.Invoke(this, e);
        }

        public void updateInfo()
        {
            if (Properties.Settings.Default.PrinterType.Equals("Magicard"))
            { 
                PrintDocument printDoc = new PrintDocument();
                try
                {
                    magi_api.EnableReporting();
                    _printerInfo = magi_api.GetPrinterInfoA();
                    magi_api.DisableReporting();
                }
                catch (Exception e)
                {
                    OnError(new PrinterEventArgs(null, "Error updating printer status: " + e.Message, null));
                }
            }
        }

        public bool register()
        {
            updateInfo();
            string selectedPrinter = "";
            if (Properties.Settings.Default.LocalPrinter.Equals(""))
            {
                // if we don't have a selected printer, pick one
                foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
                {
                    if (printer.Contains("Enduro"))
                    {
                        selectedPrinter = printer;
                        Properties.Settings.Default.LocalPrinter = printer;
                        Properties.Settings.Default.Save();
                    }
                }
            }
            else
            {
                selectedPrinter = Properties.Settings.Default.LocalPrinter;
            }


            MabelResponse resp = mabel_api.RegisterPrinter(printer_id, printer_name, printer_location, printer_model);
            if (resp.isError)
            {
                OnError(new PrinterEventArgs(null, "Register failed " + resp.message, _printerInfo));
                return false;
            }
            else
            {
                OnRegistered(new PrinterEventArgs(null,"Registered successfully", _printerInfo));
                return true;
            }
        }
        
        public bool unregister()
        {
            MabelResponse resp = mabel_api.UnregisterPrinter(printer_id);
            if (resp.isError)
            {
                OnError(new PrinterEventArgs(null, "Unregister failed " + resp.message, _printerInfo));
                return false;
            }
            else
            {
                OnUnregistered(new PrinterEventArgs(null, "Unregistered Successfully", _printerInfo));
                return true;
            }
        }

        public PrinterManager(int printer_id, string printer_name, string printer_location)
        {
            this.isRunning = false;
            this.printer_id = printer_id;
            this.printer_name = printer_name;
            this.printer_location = printer_location;
            this.printer_model = "";
            mabel_api = new MabelAPI();
            mabel_api.Debug += MabelDebug;
            // if magicard API enabled
            if (Properties.Settings.Default.PrinterType.Equals("Magicard"))
            { 
                PrintDocument printDoc = new PrintDocument();
                magi_api = new MagiCardAPI(printDoc.PrinterSettings.CreateMeasurementGraphics().GetHdc());
            }
        }

        private void MabelDebug(object sender, MabelEventArgs e)
        {
            if (e.response != null)
            { 
            OnDebug(new DebugEventArgs(e.URL, e.response.message));
            } else
            {
                OnDebug(new DebugEventArgs(e.URL, null));
            }
            //OnDebug(new DebugEventArgs(e.URL, e.res));
        }

        public PrinterInfo GetPrinterInfo()
        {
            return magi_api.GetPrinterInfoA();
        }

        public void doWork()
        {
            if (!isRunning)
            {
                processNextCard();
            }
            updateInfo();
        }

        private void processNextCard()
        {
            MabelCard card;
            String rfidToken;
            mabel_api = new MabelAPI();
            isRunning = true;
            updateInfo();
            OnChecking(new PrinterEventArgs(null, "Checking", _printerInfo));
            card = mabel_api.GetNextJob(printer_id);
            if (card == null)
            {
                isRunning = false;
                return;
            }
            updateInfo();
            OnPrinting(new PrinterEventArgs(card, "Printing", _printerInfo));
            mabel_api.SetCardStatus(printer_id, card, "PRINTING");
            PrintableCard printedCard = new PrintableCard(card);
            updateInfo();
            OnPrinting(new PrinterEventArgs(card, "Printing", _printerInfo));
            try
            {
                if (Properties.Settings.Default.PrinterType.Equals("Magicard"))
                {
                    magi_api.Feed();
                    magi_api.Wait();
                    if (Properties.Settings.Default.MagstripeEnabled)
                    {
                        magi_api.SendEncodeMag(card.mag_token);
                    }
                }

                printedCard.PrintCard();
                updateInfo();
                if (Properties.Settings.Default.PrinterType.Equals("Magicard"))
                {
                    magi_api.Wait();
                    updateInfo();
                }

                if (Properties.Settings.Default.RFIDEnabled)
                {
                    if (Properties.Settings.Default.PrinterType.Equals("Magicard"))
                        magi_api.Wait();
                    //rfidToken = Console.ReadLine();
                    // read the token from the ACR122 reader here
                    rfidToken = "1234";
                    mabel_api.SetCardRfid(printer_id, card, rfidToken);
                }

                //System.Console.WriteLine("Done waiting");
                updateInfo();
                OnPrinted(new PrinterEventArgs(card, "Printed", _printerInfo));
                mabel_api.SetCardStatus(printer_id, card, "PRINTED");
                mabel_api.SetCardPrinted(printer_id, card);
            }
            catch (Exception e)
            {
                updateInfo();
                OnError(new PrinterEventArgs(card, "ERROR: " + e.Message + magi_api.GetLastError(), _printerInfo));
                mabel_api.SetCardStatus(printer_id, card, "PRINTER_ERROR|" + e.Message + magi_api.GetLastError());
            }
            isRunning = false;
        }
    }
}
