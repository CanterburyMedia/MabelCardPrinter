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

    public class PrinterManager
    {
        private RFIDReader rfid;
        private MabelAPI mabel_api;
        private MabelCard nextCard;
        private int printer_id;
        private string printer_location;
        private int pages_printed;
        private string printer_name;
        private MagiCardAPI magi_api;
        private PrinterInfo _printerInfo;
        private bool _registered;
        private bool _requestPrint = false;

        private bool _abortPressed = false;
        private bool _retryPressed = false;
        private String _currentRfidToken;
        private bool _requestRegister = false;
        private bool _running = false;

        public event PrinterEventHandler Registered;
        public event PrinterEventHandler RegisterError;
        public event PrinterEventHandler Unregistered;
        public event PrinterEventHandler UnregisterError;
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
        public event PrinterEventHandler PrinterUpdate;
        public event PrinterEventHandler Aborted;

        public PrinterState _state = PrinterState.START;
        private String lastError;
        public enum PrinterState
        {
            START,
            UNREGISTERED,
            IDLE,
            READY,
            REQUESTING,
            CARD_READY,
            LOADING,
            FAILURE,
            ENCODING,
            PRINTING,
            EJECTING_FAIL,
            EJECTING_SUCCESS,
            RFIDREAD,
            RFIDREADTIMEOUT,
            ENROLLING,
            RFIDWAITREMOVE
        }

        public event DebugEventHander Debug;

        protected virtual void OnAborted(PrinterEventArgs e)
        {
            Aborted?.Invoke(this, e);
        }
        protected virtual void OnRegistered(PrinterEventArgs e)
        {
            Registered?.Invoke(this, e);
        }

        protected virtual void OnRegisterError(PrinterEventArgs e)
        {
            RegisterError?.Invoke(this, e);
        }

        protected virtual void OnUnregistered(PrinterEventArgs e)
        {
            Unregistered?.Invoke(this, e);
        }

        protected virtual void OnUnregisterError(PrinterEventArgs e)
        {
            UnregisterError?.Invoke(this, e);
        }

        protected virtual void OnDebug(DebugEventArgs e)
        {
            Debug?.Invoke(this, e);
        }

        protected virtual void OnPrinterUpdate(PrinterEventArgs e)
        {
            PrinterUpdate?.Invoke(this, e);
        }

        protected virtual void OnRFIDRead(PrinterEventArgs e)
        {
            RFIDRead?.Invoke(this, e);
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

        public void Retry()
        {
            _retryPressed = true;
        }

        public void Abort()
        {
            _abortPressed = true;
            if (_state == PrinterState.RFIDREAD)
            {
                rfid.CancelWait();
            } 
        }

        public void RequestRegister()
        {
            _requestRegister = true;
        }

        public void RequestPrint()
        {
            _requestPrint = true;
        }

        public int GetPrinterId()
        {
            return this.printer_id;
        }

        public void UpdatePrinterInfo()
        {
            if (Properties.Settings.Default.PrinterType.Equals("Magicard"))
            {
                OnDebug(new DebugEventArgs("", "Updating magicard printer info"));

                //bool temporarilyEnableReporting = false;
                // enable reporting for states where reporting isn't normally enabled
                //if (_state == PrinterState.UNREGISTERED || _state == PrinterState.IDLE || _state == PrinterState.REQUESTING || _state == PrinterState.READY)
                //{
                //    temporarilyEnableReporting = true;

                //}
                //if (temporarilyEnableReporting)
                /*{
                    try {
                        OnDebug(new DebugEventArgs("", "Temporarily enabling status reporting"));
                        magi_api.EnableReporting();
                    } catch (Exception e)
                    {
                        OnDebug(new DebugEventArgs("", "Magicard error: " + e.Message + magi_api.GetLastError()));
                    }
                }*/
                try
                {
                    _printerInfo = magi_api.GetPrinterInfoA();
                    OnPrinterUpdate(new PrinterEventArgs(null, "Update", _printerInfo));

                }
                catch (Exception ex)
                {
                    OnDebug(new DebugEventArgs("", "Magicard UpdatePrinterInfo Error: " + ex.Message));
                }
                /*if (temporarilyEnableReporting)
                {
                    try
                    {
                        OnDebug(new DebugEventArgs("", "Temporarily disabling reporting again"));
                        magi_api.DisableReporting();
                    }
                    catch (Exception e)
                    {
                        OnDebug(new DebugEventArgs("", "Magicard error: " + e.Message + magi_api.GetLastError()));
                    }
                }*/
            } else
            {
                _printerInfo = new PrinterInfo();
                _printerInfo.sModel = Properties.Settings.Default.LocalPrinter.ToCharArray();
                _printerInfo.bPrinterConnected = true;
                OnPrinterUpdate(new PrinterEventArgs(null, "Update", _printerInfo));
            }
        }



        public bool Register()
        {
            string selectedPrinter = Properties.Settings.Default.LocalPrinter; 
            if (selectedPrinter.Equals(""))
            {
                // no selected printer
                lastError = "No selected printer";
                return false;
            }
            UpdatePrinterInfo();
            MabelResponse resp;
            try { 
                resp = mabel_api.RegisterPrinter(printer_id, selectedPrinter, printer_location, _printerInfo);
            } catch (Exception e)
            {
                OnDebug(new DebugEventArgs("", e.Message));
                return false;
            }

            if (resp.isError)
            {
                OnRegisterError(new PrinterEventArgs(null, "Register failed " + resp.message, _printerInfo));
                _state = PrinterState.UNREGISTERED;
                _registered = false;
                return false;
            }
            else
            {
                OnRegistered(new PrinterEventArgs(null, "Registered successfully", _printerInfo));
                _state = PrinterState.IDLE;
                _registered = true;
                return true;
            }
        }

        public MagiCardAPI GetMagicardAPI()
        {
            return magi_api;
        }

        public bool Unregister()
        {
            MabelResponse resp = mabel_api.UnregisterPrinter(printer_id);
            _registered = false;
            if (resp.isError)
            {
                OnUnregisterError(new PrinterEventArgs(null, "Unregister failed " + resp.message, _printerInfo));
                return false;
            }
            else
            {
                OnUnregistered(new PrinterEventArgs(null, "Unregistered Successfully", _printerInfo));
                _state = PrinterState.UNREGISTERED;
                return true;
            }
        }

        public PrinterManager()
        {
            _state = PrinterState.UNREGISTERED;
 
            this.pages_printed = 0;

            mabel_api = new MabelAPI();
            mabel_api.Debug += MabelDebug;
            _running = false;
            // if magicard API enabled
        }


         ~PrinterManager()
        {
            if (Properties.Settings.Default.PrinterType.Equals("Magicard"))
            {
                if (magi_api != null)
                    magi_api.DisableReporting();
            }
        }

        public void StartUp(int printer_id, string printer_name, string printer_location)
        {
            this.printer_id = printer_id;
            this.printer_name = printer_name;
            this.printer_location = printer_location;
            if (Properties.Settings.Default.PrinterType.Equals("Magicard"))
            {
                PrintDocument printDoc = new PrintDocument();
                printDoc.PrinterSettings.PrinterName = Properties.Settings.Default.LocalPrinter;
                try
                {
                    if (magi_api != null)
                    {
                        magi_api.DisableReporting();
                    }
                    magi_api = new MagiCardAPI(printDoc.PrinterSettings.CreateMeasurementGraphics().GetHdc());
                    magi_api.EnableReporting();
                    OnDebug(new DebugEventArgs("", "Enabling status reporting"));
                }
                catch (Exception e)
                {
                    OnDebug(new DebugEventArgs("", "Magicard EnableReporting error: " + e.Message + magi_api.GetLastError()));
                }
                OnDebug(new DebugEventArgs("", "Magicard API Version " + magi_api.GetAPIVersionA().Major));
            }
            if (Properties.Settings.Default.RFIDEnabled)
            {
                this.rfid = new RFIDReader();
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
                return _printerInfo;                   
        }

        public void DoWork()
        {
            UpdatePrinterInfo();
            // operate the state machine here
            if (_running)
                return;
            _running = true;

            switch (_state)
            {
                case (PrinterState.UNREGISTERED):
                    if (_requestRegister)
                    {
                        _requestRegister = false;
                        if (Register())
                        {
                            // successfully registered
                            _state = PrinterState.IDLE;
                        }
                    }
                    break;

                case (PrinterState.IDLE):
                    // if we are idle, then we should go go into a requesting state
                    _abortPressed = false;
                    _retryPressed = false;
                    _state = PrinterState.REQUESTING;
                    break;

                case (PrinterState.REQUESTING):
                    OnCardRequest(new PrinterEventArgs(null, "Checking", _printerInfo));
                    _abortPressed = false;
                    _retryPressed = false;
                    if (RequestNextCard())
                    {
                        // if there is a card available to process
                        _state = PrinterState.CARD_READY;

                        OnCardReady(new PrinterEventArgs(this.nextCard, "Card available to print", _printerInfo));
                    } else
                    {
                        // otherwise go back to Idle.
                        _state = PrinterState.IDLE;
                    }
                    break;

                case (PrinterState.CARD_READY):
                    // if there is a card ready, should we print it?
                    if (_abortPressed)
                    {
                        // how do we abort from this situation?
                        _abortPressed = false;
                        _state = PrinterState.IDLE;
                        break;
                    }
                    if (_requestPrint)
                    {
                        _requestPrint = false; // turn off the switch
                        if (Properties.Settings.Default.PrinterType.Equals("Magicard"))
                        {
                            _state = PrinterState.LOADING;
                        } else
                        {
                            _state = PrinterState.PRINTING;
                        }
                    }
                    break;

                case (PrinterState.LOADING):
                    if (Properties.Settings.Default.PrinterType.Equals("Magicard"))
                        try { 
                             magi_api.EnableReporting();
                        } catch (Exception e)
                        {
                            OnDebug(new DebugEventArgs("", "Magicard Error: " + e.Message + magi_api.GetLastError()));
                        }
                    OnCardLoad(new PrinterEventArgs(this.nextCard, "loading", _printerInfo));
                    if (_abortPressed)
                    { 
                        if (Properties.Settings.Default.PrinterType.Equals("Magicard"))
                        {
                            
                            try {
                                EjectCard();
                                magi_api.DisableReporting();
                            } catch (Exception e)
                            {
                                OnDebug(new DebugEventArgs("", "Magicard Error: " + e.Message + magi_api.GetLastError()));
                            }
                    }
                        _state = PrinterState.IDLE;
                        OnAborted(new PrinterEventArgs(this.nextCard, "Loading Aborted", _printerInfo));
                        break;
                    }
   
                    if (LoadCard())
                    {
                        // card loaded successfully, progress to Encoding
                        OnCardLoadSuccess(new PrinterEventArgs(this.nextCard, "Loading succeeded", _printerInfo));
                        if (Properties.Settings.Default.MagstripeEnabled)
                        {
                            _state = PrinterState.ENCODING;
                        } else
                        {
                            _state = PrinterState.PRINTING;
                        }
                    }
                    else
                    {
                        // card wasn't loaded successfully, go to FAILURe
                        OnCardLoadFail(new PrinterEventArgs(this.nextCard, "Loading failed: " + magi_api.GetLastEnduroMessage() + " : " + magi_api.GetLastError(), _printerInfo));
                        _state = PrinterState.FAILURE;
                    }
                    break;

                case (PrinterState.ENCODING):
                    // we are encoding the card
                    if (_abortPressed)
                    {
                        EjectCard();
                        magi_api.DisableReporting();
                        _state = PrinterState.IDLE;
                        OnAborted(new PrinterEventArgs(this.nextCard, "Aborted encoding", _printerInfo));
                        break;
                    }
                    OnMagEncode(new PrinterEventArgs(this.nextCard, "Encoding mag stripe", _printerInfo));
                    if (EncodeCard())
                    {
                        // encoding completed successfully, progress to Printing
                        OnMagEncodeSuccess(new PrinterEventArgs(this.nextCard, "Mag encoding success", _printerInfo));
                        _state = PrinterState.PRINTING;
                    } else
                    {
                        // encoding did not complete successfully, eject the card;
                        OnMagEncodeFail(new PrinterEventArgs(this.nextCard, "Mag encoding failure: " + magi_api.GetLastEnduroMessage() + " : " + magi_api.GetLastError(), _printerInfo));
                        _state = PrinterState.EJECTING_FAIL;
                    }
                    break;

                case (PrinterState.PRINTING):
                    // we are printing the card
                    if (_abortPressed)
                    {
                        if (Properties.Settings.Default.PrinterType.Equals("Magicard"))
                        {
                            EjectCard();
                            magi_api.DisableReporting();
                        }
                        OnAborted(new PrinterEventArgs(this.nextCard, "Aborted Printing", _printerInfo));
                    }
                    OnPrint(new PrinterEventArgs(this.nextCard, "Printing card", _printerInfo));
                    if (Properties.Settings.Default.DontPrint)
                    {
                        OnPrintSuccess(new PrinterEventArgs(this.nextCard, "Printed successfully (not really, disabled)", _printerInfo));
                        _state = PrinterState.IDLE;
                        break;
                    }
                    if (PrintCard())
                    {
                        OnPrintSuccess(new PrinterEventArgs(this.nextCard, "Printed successfully", _printerInfo));
                        if (Properties.Settings.Default.PrinterType.Equals("Magicard"))
                            magi_api.DisableReporting();
                        // if we have RFID enabled, then go to that state
                        if (Properties.Settings.Default.RFIDEnabled)
                        {
                            // if RFID reading is enabled, go to that state
                            _state = PrinterState.RFIDREAD;
                        } else
                        {
                            // otherwise if RFID isn't enabled, we're done
                            _state = PrinterState.IDLE;
                        }
                    } else
                    {
                        if (Properties.Settings.Default.PrinterType.Equals("Magicard"))
                        {
                            OnPrintFail(new PrinterEventArgs(this.nextCard, "Printing failure: " + magi_api.GetLastEnduroMessage() + " : " + magi_api.GetLastError(), _printerInfo));
                        } else
                        {
                            OnPrintFail(new PrinterEventArgs(this.nextCard, "Printing failure", _printerInfo));
                        }
                        _state = PrinterState.FAILURE;
                    }
                    break;

                case (PrinterState.RFIDREAD):
                    // read the RFID
                    OnRFIDRead(new PrinterEventArgs(this.nextCard, "Reading RFID", _printerInfo));
                    try { 
                        if (ReadRFID())
                        {
                            // read the RFID successfully, now we wait for it to be removed, then enroll it
                            OnRFIDReadSuccess(new PrinterEventArgs(this.nextCard, _currentRfidToken, _printerInfo));
                            _state = PrinterState.RFIDWAITREMOVE;
                        } else
                        {
                            // didn't read RFID successfully
                            OnRFIDReadTimeout(new PrinterEventArgs(this.nextCard, lastError, _printerInfo));
                            _state = PrinterState.RFIDREADTIMEOUT;
                        }
                    } catch (Exception e)
                    {
                        OnRFIDReadTimeout(new PrinterEventArgs(this.nextCard, e.Message, _printerInfo));
                        _state = PrinterState.RFIDREADTIMEOUT;
                    }
                    break;

                case (PrinterState.RFIDWAITREMOVE):
                    rfid.WaitForRemoval();
                    OnRFIDRemoved(new PrinterEventArgs(this.nextCard, "RFID Removed", _printerInfo));
                    _state = PrinterState.ENROLLING;
                    break;
                case (PrinterState.RFIDREADTIMEOUT):
                    // if we request a RFID retry
                    if (_retryPressed)
                    {
                        // go back to the RFIDREAD state and try again
                        _state = PrinterState.RFIDREAD;
                        _retryPressed = false;
                    }
                    if (_abortPressed)
                    {
                        _abortPressed = false;
                        // otherwise go back to the idle state.
                        _state = PrinterState.IDLE;
                    }
                    // otherwise, do nothing.
                    break;
                    
                case (PrinterState.ENROLLING):
                    OnEnrol(new PrinterEventArgs(this.nextCard, "Enrolling", _printerInfo));
                    if (EnrolToken())
                    {
                        OnEnrolSuccess(new PrinterEventArgs(this.nextCard, "Enrolled successfully", _printerInfo));
                        _state = PrinterState.IDLE;
                    } else
                    {
                        // if enrolling didn't work properly, go to the RFID timeout state so we can try again, or abort.
                        OnEnrolFail(new PrinterEventArgs(this.nextCard, lastError , _printerInfo));
                        _state = PrinterState.RFIDREADTIMEOUT;
                    }
                    break;

                case (PrinterState.FAILURE):
                    // a failure has occurred. We can either retry or abort.
                    if (_retryPressed)
                    {
                        _retryPressed = false;
                        magi_api.DisableReporting();
                        _state = PrinterState.CARD_READY;
                    }
                    if (_abortPressed)
                    {
                        // abort card , return to idle state.
                        magi_api.DisableReporting();
                        _abortPressed = false;
                        if (!AbortCard())
                        { 
                            // if it doesn't abort, then there's clearly a bigger problem and we need to force re-registration
                            Unregister();
                        }
                        // go back to the idle state
                        _state = PrinterState.IDLE;
                    }
                    break;

                case (PrinterState.EJECTING_FAIL):
                    if (Properties.Settings.Default.PrinterType.Equals("Magicard"))
                    {
                        if (EjectCard())
                        {
                            // eject the card. only if it has succeeded then proceed to the next step.
                            _state = PrinterState.FAILURE;
                        } // if it fails, keep trying to eject the card.
                    } else
                    { // if it's a non Magicard printer, proceed to the next stage anyway.
                        _state = PrinterState.FAILURE;
                    }
                    break;
            }
            _running = false;
        }

        public bool RequestNextCard()
        {
            MabelCard card;

            try { 
                card = mabel_api.GetNextJob(printer_id);
                if (card == null)
                {
                    return false;
                }
                this.nextCard = card;
                return true;
            } catch (Exception ex)
            {
                OnDebug(new DebugEventArgs(mabel_api.lastRequest.buildURL(), ex.Message));
                return false;
            }
        }

        public void CancelRFIDWait()
        {
            rfid.CancelWait();
        }

        public bool LoadCard()
        {
                magi_api.EnableReporting();
                magi_api.Feed();

                MagiCardAPI.MagiCardReturnVal ret = magi_api.Wait();
                if (ret == MagiCardAPI.MagiCardReturnVal.MAGICARD_ERROR)
                {
                    return false;
                    // go to the fail/retry state?
                }
                return true;
        }



        private bool EncodeCard()
        {
            magi_api.SendEncodeMag(this.nextCard.mag_token);
            MagiCardAPI.MagiCardReturnVal magWaitRet = magi_api.Wait();
            if (magWaitRet == MagiCardAPI.MagiCardReturnVal.ERROR_SUCCESS)
            {
                return true;
            }
            return false;
        }

        private bool EnrolToken()
        {
            try { 
                mabel_api.SetToken(_currentRfidToken, this.printer_id);
                _currentRfidToken = "";
                return true;
            } catch (Exception ex)
            {
                lastError = "Enroll failure: " + ex.Message;
                
                return false;
            }
        }

        private bool ReadRFID()
        {
            try
            {
                _currentRfidToken = rfid.ReadRFIDToken(15);
                
                // fire RFID token update event
                return true;
            }
            catch (Exception ex)
            {
                _currentRfidToken = ""; // no RFID token?
                lastError = "RFID Read Error: " + ex.Message;
                return false;
            }
        }

        private bool AbortCard()
        {
            try { 
                mabel_api.SetCardStatus(printer_id, this.nextCard, "aborted");
                return true;
            } catch (Exception ex)
            {
                lastError = "Couldn't abort card: " + ex.Message;
                return false;
            }
        }

        private bool PrintCard()
        {
            
            this.PrintMabelCard(this.nextCard);
            if (Properties.Settings.Default.PrinterType.Equals("Magicard"))
            {
                MagiCardAPI.MagiCardReturnVal retPrint = magi_api.Wait();
                if (retPrint == MagiCardAPI.MagiCardReturnVal.ERROR_SUCCESS)
                {
                    // printed OK
                    mabel_api.SetCardPrinted(printer_id, this.nextCard);
                    mabel_api.SetCardStatus(printer_id, this.nextCard, "printed");

                    return true;
                } else
                {
                    mabel_api.SetCardStatus(printer_id, this.nextCard, "failed");
                    
                    return false;
                }
            } else
            {
                mabel_api.SetCardPrinted(printer_id, this.nextCard);
                mabel_api.SetCardStatus(printer_id, this.nextCard, "printed");
                return true;
            }
            
        }
        
        private bool EjectCard()
        {
            OnEject(new PrinterEventArgs(this.nextCard, "Ejecting card", _printerInfo));
            magi_api.Eject();
            MagiCardAPI.MagiCardReturnVal retEject = magi_api.Wait();
            if (retEject == MagiCardAPI.MagiCardReturnVal.ERROR_SUCCESS)
            {
                // worked OK
                OnEjectSuccess(new PrinterEventArgs(this.nextCard, "Ejected OK", _printerInfo));
                return true;
            } else
            {
                return false;
            }           
        }

        public void PrintMabelCard(MabelCard card)
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
                //System.Console.Out.WriteLine("Resolution : " + pkResolution.Kind + " X : " + pkResolution.X + " Y: " + pkResolution.Y);
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

            printDoc.PrintPage += new PrintPageEventHandler(printDoc_PrintPage);
            if (printDoc.PrinterSettings.IsValid)
            {
                try
                {
                    printDoc.Print();
                } catch (Exception ex)
                {
                    lastError = "Printing Error: " + ex.Message;
                    // something went wrong.
                }
            }
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
