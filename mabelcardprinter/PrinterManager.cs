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
        public String Status;
        public PrinterInfo Info;
        public PrinterEventArgs(MabelCard _card, String _status, PrinterInfo _info)
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
        private int pages_printed;
        private string printer_model;
        private string printer_name;
        private MagiCardAPI magi_api;
        private PrinterInfo _printerInfo;
        private bool isRunning;
        /*
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
        */
        public event PrinterEventHandler CardRequest;
        public event PrinterEventHandler CardReady;
        public event PrinterEventHandler CardLoad;
        public event PrinterEventHandler CardLoadSuccess;
        public event PrinterEventHandler CardLoadFailed;
        public event PrinterEventHandler MagEncode;
        public event PrinterEventHandler MagEncodeSuccess;
        public event PrinterEventHandler MagEncodeFailed;
        public event PrinterEventHandler Print;
        public event PrinterEventHandler PrintSuccess;
        public event PrinterEventHandler PrintFail;
        public event PrinterEventHandler Eject;
        public event PrinterEventHandler EjectSuccess;
        public event PrinterEventHandler EjectFail;
        public event PrinterEventHandler RFIDRead;
        public event PrinterEventHandler RFIDReadSuccess;
        public event PrinterEventHandler RFIDReadTimeout;
        public event PrinterEventHandler RFIDRemoved;
        public event PrinterEventHandler Enrol;
        public event PrinterEventHandler EnrolSuccess;
        public event PrinterEventHandler EnrolFail;
        public event PrinterEventHandler Update;


        public event DebugEventHander Debug;

        protected virtual void OnDebug(DebugEventArgs e)
        {
            Debug?.Invoke(this, e);
        }

        protected virtual void OnUpdate(PrinterEventArgs e)
        {
            Update?.Invoke(this, e);
        }

        protected virtual void OnRFIDReadSuccess(PrinterEventArgs e)
        {
            RFIDReadSuccess?.Invoke(this, e);
        }

        protected virtual void OnRFIDReadTimeout(PrinterEventArgs e)
        {
            RFIDReadTimeout?.Invoke(this, e);
        }

        protected virtual void OnRFIDRemoved(PrinterEventArgs e)
        {
            RFIDRemoved?.Invoke(this, e);
        }

        protected virtual void OnEnrol(PrinterEventArgs e)
        {
            Enrol?.Invoke(this, e);
        }     

        protected virtual void OnEnrolSuccess(PrinterEventArgs e)
        {
            EnrolSuccess?.Invoke(this, e);
        }

        protected virtual void OnEnrolFail(PrinterEventArgs e)
        {
            EnrolFail?.Invoke(this, e);
        }

        protected virtual void OnEject(PrinterEventArgs e)
        {
            Eject?.Invoke(this, e);
        }

        protected virtual void OnEjectSuccess(PrinterEventArgs e)
        {
            EjectSuccess?.Invoke(this, e);
        }

        protected virtual void OnEjectFail(PrinterEventArgs e)
        {
            EjectFail?.Invoke(this, e);
        }

        protected virtual void OnPrint(PrinterEventArgs e)
        {
            Print?.Invoke(this, e);
        }

        protected virtual void OnPrintSuccess(PrinterEventArgs e)
        {
            PrintSuccess?.Invoke(this, e);
        }

        protected virtual void OnPrintFail(PrinterEventArgs e)
        {
            PrintFail?.Invoke(this, e);
        }

        protected virtual void OnMagEncode(PrinterEventArgs e)
        {
            MagEncode?.Invoke(this, e);
        }

        protected virtual void OnMagEncodeSuccess(PrinterEventArgs e)
        {
            MagEncodeSuccess?.Invoke(this, e);
        }

        protected virtual void OnMagEncodeFail(PrinterEventArgs e)
        {
            MagEncodeFailed?.Invoke(this, e);
        }

        protected virtual void OnCardLoad(PrinterEventArgs e)
        {
            CardLoad?.Invoke(this, e);
        }

        protected virtual void OnCardLoadFail(PrinterEventArgs e)
        {
            CardLoadFailed?.Invoke(this, e);
        }

        protected virtual void OnCardLoadSuccess(PrinterEventArgs e)
        {
            CardLoadSuccess?.Invoke(this, e);
        }

        protected virtual void OnCardReady(PrinterEventArgs e)
        {
            CardReady?.Invoke(this, e);
        }

        protected virtual void OnCardRequest(PrinterEventArgs e)
        {
            CardRequest?.Invoke(this, e);
        }

        public void updateInfo()
        {
            if (Properties.Settings.Default.PrinterType.Equals("Magicard"))
            {
                try
                {
                   // magi_api.EnableReporting();
                    _printerInfo = magi_api.GetPrinterInfoA();
                    // magi_api.DisableReporting();
                    OnUpdate(new PrinterEventArgs(null, "Update", _printerInfo));
                }
                catch (Exception e)
                {
                    //OnError(new PrinterEventArgs(null, "Error updating printer status: " + e.Message, null));
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
                //OnError(new PrinterEventArgs(null, "Register failed " + resp.message, _printerInfo));
                return false;
            }
            else
            {
                //OnRegistered(new PrinterEventArgs(null, "Registered successfully", _printerInfo));
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
            this.pages_printed = 0;
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
            OnCardRequest(new PrinterEventArgs(null, "Checking", _printerInfo));
            card = mabel_api.GetNextJob(printer_id);
            if (card == null)
            {
                isRunning = false;
                return;
            }
            this.nextCard = card;
            OnCardReady(new PrinterEventArgs(this.nextCard, "ready", _printerInfo));
        }

        public void printNextCard()
        {
            String rfidToken;
            mabel_api.SetCardStatus(printer_id, this.nextCard, "printing");
            magi_api.EnableReporting();
            updateInfo();
            if (!Properties.Settings.Default.DontPrint) // if we've not disabled printing...
            { 
                try
                {
                    if (Properties.Settings.Default.PrinterType.Equals("Magicard"))
                    {
                        magi_api.Feed();
                        updateInfo();
                        OnCardLoad(new PrinterEventArgs(this.nextCard, "loading", _printerInfo));
                        MagiCardAPI.MagiCardReturnVal ret  = magi_api.Wait();
                        updateInfo();
                        if (ret == MagiCardAPI.MagiCardReturnVal.MAGICARD_ERROR)
                        {
                            
                            OnCardLoadFail(new PrinterEventArgs(this.nextCard, "Loading failed", _printerInfo));
                            // go to the fail/retry state?
                        }
                        OnCardLoadSuccess(new PrinterEventArgs(this.nextCard, "Loading succeeded", _printerInfo));
                        if (Properties.Settings.Default.MagstripeEnabled)
                        {
                            updateInfo();
                            OnMagEncode(new PrinterEventArgs(this.nextCard, "Encoding mag stripe", _printerInfo));
                            magi_api.SendEncodeMag(this.nextCard.mag_token);
                            MagiCardAPI.MagiCardReturnVal magWaitRet = magi_api.Wait();
                            updateInfo();
                            if (ret == MagiCardAPI.MagiCardReturnVal.ERROR_SUCCESS)
                            {
                                OnMagEncodeFail(new PrinterEventArgs(this.nextCard, "Mag encoding failure", _printerInfo));
                                // exit somehow?
                            }
                            OnMagEncodeSuccess(new PrinterEventArgs(this.nextCard, "Mag encoding success", _printerInfo));
                        }
                    }
                    updateInfo();
                    OnPrint(new PrinterEventArgs(this.nextCard, "Printing card", _printerInfo));
                    this.PrintCard(this.nextCard);
                    updateInfo();
                    if (Properties.Settings.Default.PrinterType.Equals("Magicard"))
                    {
                        MagiCardAPI.MagiCardReturnVal retPrint = magi_api.Wait();
                        updateInfo();
                        if (retPrint == MagiCardAPI.MagiCardReturnVal.ERROR_SUCCESS)
                        {
                            // printed OK
                            OnPrintSuccess(new PrinterEventArgs(this.nextCard, "Magicard printed", _printerInfo));
                        }
                    }
                    updateInfo();
                    if (Properties.Settings.Default.PrinterType.Equals("Magicard"))
                    {
                        OnEject(new PrinterEventArgs(this.nextCard, "Ejecting card", _printerInfo));
                        magi_api.Eject();
                        MagiCardAPI.MagiCardReturnVal retEject = magi_api.Wait();
                        if (retEject == MagiCardAPI.MagiCardReturnVal.ERROR_SUCCESS)
                        {
                            // worked OK
                            updateInfo();
                            OnEjectSuccess(new PrinterEventArgs(this.nextCard, "Ejected OK", _printerInfo));
                        }
                    }
                    
                    
                    if (Properties.Settings.Default.RFIDEnabled)
                    {
                        mabel_api.SetCardStatus(printer_id, this.nextCard, "PRINTED");
                        mabel_api.SetCardPrinted(printer_id, this.nextCard);
                        updateInfo();
                        OnRFIDRead(new PrinterEventArgs(this.nextCard, "Waiting for RFID", _printerInfo));
                        try
                        {
                            rfidToken = rfid.getNFCToken(15);
                            OnRFIDReadSuccess(new PrinterEventArgs(this.nextCard,rfidToken,_printerInfo));
                            OnEnrol(new PrinterEventArgs(this.nextCard, "Enrolling", _printerInfo));
                            mabel_api.SetToken(rfidToken , this.printer_id);
                            OnEnrolSuccess(new PrinterEventArgs(this.next_card, "Enrolled successfully", _printerInfo));
                        } catch (Exception ex)
                        {
                            rfidToken = ""; // no RFID token?
                            OnRFIDReadTimeout(new PrinterEventArgs(this.nextCard, "RFID Timeout", _printerInfo));
                        }
                    }
                    
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
        public bool PrintCard(MabelCard card)
        {
            if ((card.GetCardBackImage() == null) || (card.GetCardFrontImage() == null))
            {
                //throw new Exception("IMAGE_NULL"); // 
            }
            PrintDocument printDoc = new PrintDocument();
            this.pages_printed = 0;
            printDoc.PrinterSettings.PrinterName = Properties.Settings.Default.LocalPrinter;
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

            if (targetRes != null)
            {
                printDoc.DefaultPageSettings.PrinterResolution = targetRes;
            }
            else
            {
                targetRes = printDoc.DefaultPageSettings.PrinterResolution;
            }
            //System.Console.Out.WriteLine("Selected : " + targetRes.Kind + " X : " + targetRes.X + " Y: " + targetRes.Y);

            printDoc.PrintPage += new PrintPageEventHandler(printDoc_PrintPage);
            if (printDoc.PrinterSettings.IsValid)
            {
                printDoc.Print();
                return true;
            }
            else
            {
                System.Console.Out.WriteLine("Erp, couldn't set printer settings properly :/");
                return false;
            }

        }

        private void printDoc_PrintDone(Object sender, PrintEventArgs e)
        {
        }

        private void printDoc_PrintPage(Object sender, PrintPageEventArgs e)
        {
            if (pages_printed == 0)
            {
                // draw the image 
                e.Graphics.DrawImage(this.nextCard.GetCardFrontImage(), 0, 0, 337, 213);//new Rectangle(0, 0, 639, 101));//, 213, 337);
                //e.Graphics.DrawImage(card.GetCardFrontImage(), 0, 0, 1016, 642);
                pages_printed++;
                e.HasMorePages = true;
            }
            else
            {
                e.Graphics.DrawImage(this.nextCard.GetCardBackImage(), 0, 0, 337, 213);
                //e.Graphics.DrawImage(card.GetCardBackImage(), 0, 0, 1016, 642);//new Rectangle(0, 0, 213, 101));//, 213, 337);
                // Write the string to encode onto the mag strip
               // Font magFont = new Font("Arial", 10, FontStyle.Regular);

                //e.Graphics.DrawString("~2,BPI75,MPC5,MGVON,COEH,;" + card.mag_token + "?", magFont, Brushes.Black, 0, 0);
                e.HasMorePages = false;
            }

        }
    }
}
