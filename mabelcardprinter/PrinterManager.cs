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

    public class NFCEventArgs : EventArgs
    {
        public String rfidToken;
        public MabelCard card;
        public NFCEventArgs(String rfidToken, MabelCard card)
        {
            this.rfidToken = rfidToken;
            this.card = card;
        }
    }

    public delegate void PrinterEventHandler(object sender, PrinterEventArgs e);
    public delegate void NFCEventHandler(object sender, NFCEventArgs e);
    public delegate void DebugEventHander(object sender, DebugEventArgs e);

    class PrinterManager
    {
        private RFIDReader rfid;
        private MabelAPI mabel_api;
        private MabelCard nextCard;
        private int printer_id;
        private string printer_location;
        private string printer_model;
        private string printer_name;
        private MagiCardAPI magi_api;
        private PrinterInfo _printerInfo;
        private bool isRunning;
        public event PrinterEventHandler ReadyCard;
        public event PrinterEventHandler EncodingCard;
        public event PrinterEventHandler FeedingCard;
        public event PrinterEventHandler EjectingCard;
        public event PrinterEventHandler PrintingCard;
        public event PrinterEventHandler WaitingCard;
        public event PrinterEventHandler PrintedCard;
        public event PrinterEventHandler FinishedCard;
        public event PrinterEventHandler ErrorCard;
        public event PrinterEventHandler Registered;
        public event PrinterEventHandler Unregistered;
        public event PrinterEventHandler Checking;
        public event PrinterEventHandler UpdateInfo;
        public event NFCEventHandler NFCRead;

        public event DebugEventHander Debug;

        protected virtual void OnNFCRead(NFCEventArgs e)
        {
            NFCRead?.Invoke(this, e);
        }

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

        protected virtual void OnReadyCard(PrinterEventArgs e)
        {
            ReadyCard?.Invoke(this, e);
        }

        protected virtual void OnDebug(DebugEventArgs e)
        {
            Debug?.Invoke(this, e);
        }

        protected virtual void OnFinishedCard(PrinterEventArgs e)
        {
            FinishedCard?.Invoke(this, e);
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


            MabelResponse resp = mabel_api.RegisterPrinter(printer_id, selectedPrinter, printer_location, printer_model);
            if (resp.isError)
            {
                OnError(new PrinterEventArgs(null, "Register failed " + resp.message, _printerInfo));
                return false;
            }
            else
            {
                OnRegistered(new PrinterEventArgs(null, "Registered successfully", _printerInfo));
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
            this.rfid = new RFIDReader();
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
                OnDebug(new DebugEventArgs(e.request.buildURL(), "url: " + e.request.buildURL() + ":" + e.response._raw));
            } else
            {
                OnDebug(new DebugEventArgs(e.request.buildURL(), "url: " + e.request.buildURL() + ": no response"));
            }
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

        public void processNextCard()
        {
            MabelCard card;
            isRunning = true;
            updateInfo();
            OnChecking(new PrinterEventArgs(null, "Checking", _printerInfo));
            card = mabel_api.GetNextJob(printer_id);
            if (card == null)
            {
                isRunning = false;
                return;
            }
            this.nextCard = card;
            OnReadyCard(new PrinterEventArgs(this.nextCard, "ready", _printerInfo));
        }

        public void printNextCard()
        {
            String rfidToken;
            mabel_api.SetCardStatus(printer_id, this.nextCard, "printing");
            PrintableCard printedCard = new PrintableCard(this.nextCard);
            updateInfo();
            OnPrinting(new PrinterEventArgs(this.nextCard, "Printing", _printerInfo));
            if (!Properties.Settings.Default.DontPrint) // if we've not disabled printing...
            { 
                try
                {
                    if (Properties.Settings.Default.PrinterType.Equals("Magicard"))
                    {
                        magi_api.Feed();
                        magi_api.Wait();
                        if (Properties.Settings.Default.MagstripeEnabled)
                        {
                            magi_api.SendEncodeMag(this.nextCard.mag_token);
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
                        {
                            magi_api.Wait();
                        }
                        OnPrinted(new PrinterEventArgs(this.nextCard, "Printed", _printerInfo));
                        mabel_api.SetCardStatus(printer_id, this.nextCard, "PRINTED");
                        mabel_api.SetCardPrinted(printer_id, this.nextCard);
                        OnWaiting(new PrinterEventArgs(this.nextCard, "Waiting for RFID", _printerInfo));
                        try
                        {
                            rfidToken = rfid.getNFCToken(15);
                            OnNFCRead(new NFCEventArgs(rfidToken,this.nextCard));
                            mabel_api.SetToken(rfidToken , this.printer_id);
                        } catch (Exception ex)
                        {
                            rfidToken = ""; // no RFID token?
                        }
                    }
                    OnFinishedCard(new PrinterEventArgs(this.nextCard, "Finished processing card", _printerInfo));
                    updateInfo();

                }
                catch (Exception e)
                {
                    updateInfo();
                    OnError(new PrinterEventArgs(this.nextCard, "ERROR: " + e.Message + magi_api.GetLastError(), _printerInfo));
                    mabel_api.SetCardStatus(printer_id, this.nextCard, "PRINTER_ERROR|" + e.Message + magi_api.GetLastError());
                }
            } else
            {
                System.Threading.Thread.Sleep(5000);
                mabel_api.SetCardStatus(printer_id, this.nextCard, "NOTREALLYPRINTED");
                mabel_api.SetCardPrinted(printer_id, this.nextCard);
                OnPrinted(new PrinterEventArgs(this.nextCard, "Not really printed", _printerInfo));
            }
            isRunning = false;
        }
    }
}
