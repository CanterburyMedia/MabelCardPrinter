using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Configuration;
using System.Drawing.Printing;

namespace MabelCardPrinter
{
    public class MagiCardAPIVersion
    {
        public Int32 Major;
        public Int32 Minor;
        public Int32 Build;
        public Int32 Private;
        public MagiCardAPIVersion(byte[] rBytes)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream(rBytes);
            System.IO.BinaryReader br = new System.IO.BinaryReader(ms);
            Major = br.ReadInt32();
            Minor = br.ReadInt32();
            Build = br.ReadInt32();
            Private = br.ReadInt32();
        }
    }

    public class MagicardException : Exception
    {
        public MagicardException(string message) : base(message)
        {

        }
    }
    public class MagiCardAPI
    {




	    public enum MagiCardStatus
	    {
		    STATUS_READY,
		    STATUS_BUSY,
		    STATUS_ERROR,
		    STATUS_OFFLINE,
		    MAGICARD_ERROR
	    }

        private const uint CONFIG_QUIET = 1;
        private const UInt32 FEED_CHIPCARD = 1;

        private const UInt32 FEED_CONTACTLESS = 2;
        private const int MAGICARD_TIMEOUT = -1;
        private const int MAGICARD_ERROR = -2;
        private const int MAGICARD_PRINTER_ERROR = -3;
        private const int MAGICARD_DRIVER_NOTCOMPLIANT = -4;
        private const int MAGICARD_OPENPRINTER_ERROR = -5;
        private const int MAGICARD_REMOTECOMM_ERROR = -6;
        private const int MAGICARD_LOCALCOMM_ERROR = -7;
        private const int MAGICARD_SPOOLER_NOT_EMPTY = -8;
        private const int MAGICARD_REMOTECOMM_IN_USE = -9;

        private const int MAGICARD_LOCALCOMM_IN_USE = -10;

        private const int ERROR_SUCCESS = 0;

        public enum MagiCardReturnVal
	    {
		    ERROR_SUCCESS = 0,
		    MAGICARD_ERROR = -1,
		    MAGICARD_DRIVER_NOTCOMPLIANT = -2,
		    MAGICARD_LOCALCOMM_ERROR = -3,
		    MAGICARD_REMOTECOMM_ERROR = -4,
		    MAGICARD_OPENPRINTER_ERROR = -5,
		    MAGICARD_SPOOLER_NOT_EMPTY = -6,
		    MAGICARD_REMOTECOMM_IN_USE = -7,
		    MAGICARD_LOCALCOMM_IN_USE = -8
		    //MAGICARD_TIMEOUT = -1
		    //MAGICARD_ERROR = -2
		    //MAGICARD_PRINTER_ERROR = -3
		    //MAGICARD_DRIVER_NOTCOMPLIANT = -4
		    //MAGICARD_OPENPRINTER_ERROR = -5
		    //MAGICARD_REMOTECOMM_ERROR = -6
		    //MAGICARD_LOCALCOMM_ERROR = -7
		    //MAGICARD_SPOOLER_NOT_EMPTY = -8
		    //MAGICARD_REMOTECOMM_IN_USE = -9
		    //MAGICARD_LOCALCOMM_IN_USE = -10
	    }


	    [DllImport("c:\\windows\\system32\\MagAPI.dll")]
	    private static extern Int32 EnableStatusReporting(IntPtr hDC, IntPtr phSession, UInt32 dwFlags);
	    
	    [DllImport("c:\\windows\\system32\\MagAPI.dll")]
	    private static extern Int32 DisableStatusReporting(Int32 hSession);

	    [DllImport("c:\\windows\\system32\\MagAPI.dll")]
	    private static extern Int32 FeedCard(Int32 hSession, UInt32 dwMode, Int32 iParam, IntPtr JobName);

	    [DllImport("c:\\windows\\system32\\MagAPI.dll")]
	    private static extern Int32 EjectCard(Int32 hSession, IntPtr JobName);

	    [DllImport("c:\\windows\\system32\\MagAPI.dll")]
	    private static extern Int32 WaitForPrinter(Int32 hSession);

        [DllImport("c:\\windows\\system32\\MagAPI.dll")]
        private static extern Int32 GetLastPrinterMessage(Int32 hSession, IntPtr lpszBuffer, IntPtr pdwBufferSize);

        [DllImport("c:\\windows\\system32\\MagAPI.dll")]
        private static extern Int32 GetLastEnduroMessage(Int32 hSession, IntPtr lpszBuffer, IntPtr pdwBufferSize);

        [DllImport("c:\\windows\\system32\\MagAPI.dll")]
        private static extern Int32 GeneralCommand(Int32 hSession, IntPtr lpszCommandString);
    
        [DllImport("c:\\windows\\system32\\MagAPI.dll")]
        private static extern Int32 GetPrinterStatus(Int32 hSession);

        [DllImport("c:\\windows\\system32\\MagAPI.dll")]
        private static extern Int32 GetPrinterInfo(Int32 hSession, IntPtr printerInfoPtr);

        [DllImport("c:\\windows\\system32\\MagAPI.dll")]
        private static extern Int32 SetEjectMode(Int32 hSession, Int32 mode);

        [DllImport("c:\\windows\\system32\\MagAPI.dll")]
	    private static extern Int32 RequestMagData(Int32 hSession);

	    [DllImport("c:\\windows\\system32\\MagAPI.dll")]
	    private static extern Int32 ReadMagData(Int32 hSession, IntPtr pMSV);

        [DllImport("c:\\windows\\system32\\MagAPI.dll")]
        private static extern Int32 EncodeMagStripe(Int32 hSession,int iTrackNo,
                Int32 iCharCount,
                IntPtr lpszData,
                Int32 iEncodingSpec,
                Int32 iVerify,
                Int32 iCoercivity,
                Int32 iBitsPerChar,
                Int32 iBitsPerInch,
                Int32 iParity,
                Int32 iLRC);
        [DllImport("c:\\windows\\system32\\MagAPI.dll")]
        private static extern Int32 ReadMagStripe(Int32 hSession , IntPtr pMSV,Int32 iEncodingSpec);

        [DllImport("c:\\windows\\system32\\MagAPI.dll")]
        private static extern void DecodeMagData(Int32 hSession,IntPtr pMSV);

        [DllImport("c:\\windows\\system32\\MagAPI.dll")]
        private static extern Int32 GetPrinterType(Int32 hSession);

        [DllImport("c:\\windows\\system32\\MagAPI.dll")]
        private static extern Int32 GetConnectionType(Int32 hSession);

        [DllImport("c:\\windows\\system32\\MagAPI.dll")]
        private static extern Int32 FlipCard(Int32 hSession);

        [DllImport("c:\\windows\\system32\\MagAPI.dll")]
        private static extern Int32 CleanPrinter(Int32 hSession);

        [DllImport("c:\\windows\\system32\\MagAPI.dll")]
        private static extern Int32 RestartPrinter(Int32 hSession);

        [DllImport("c:\\windows\\system32\\MagAPI.dll")]
        private static extern Int32 PrintTestCard(Int32 hSession);

        [DllImport("c:\\windows\\system32\\MagAPI.dll")]
        private static extern Int32 SetSmartMode(Int32 hSession,Int32 iMode);

        [DllImport("c:\\windows\\system32\\MagAPI.dll")]
        private static extern Int32 SetSmartLocation(Int32 hSession, Int32 iParam);

        [DllImport("c:\\windows\\system32\\MagAPI.dll")]
        private static extern Int32 EraseCard(Int32 hSession,
            Int32 iBottomLeftX,
            Int32 iBottomLeftY,
            Int32 iTopRightX,
            Int32 iTopRightY);

        [DllImport("c:\\windows\\system32\\MagAPI.dll")]
        private static extern Int32 SetEraseSpeed(Int32 hSession, Int32 iMode);

        [DllImport("c:\\windows\\system32\\MagAPI.dll")]
        private static extern Int32 GetAPIVersion(Int32 hSession, IntPtr pAPIVersion);

        [DllImport("c:\\windows\\system32\\MagAPI.dll")]
        private static extern Int32 ErrorResponse(Int32 hSession, Int32 iParam);

        // depreciated
        [DllImport("c:\\windows\\system32\\MagAPI.dll")]
	    private static extern Int32 GetEnduroInfo(Int32 hSession, IntPtr pPrinterInfo);



	    private string LastError = "";
	    private IntPtr MyPrinterHdc;
        private Int32 hSession;
        private bool reportingEnabled = false;

	    //private IntPtr pJobName;
	    private void ThrowException(string FunctionName, int Err)
	    {
		    string strException = FunctionName + " Has returned a result code of: " + Err + " - ";
		    switch ((Err)) {

			    case MAGICARD_ERROR:
				    strException += "MagiCard Error";
				    break; 

			    case MAGICARD_PRINTER_ERROR:
				    strException += "MAGICARD_PRINTER_ERROR";
				    break; 

			    case MAGICARD_DRIVER_NOTCOMPLIANT:
				    strException += "MAGICARD_DRIVER_NOTCOMPLIANT";
				    break; 

			    case MAGICARD_OPENPRINTER_ERROR:
				    strException += "MAGICARD_OPENPRINTER_ERROR";
				    break; 

			    case MAGICARD_REMOTECOMM_ERROR:
				    strException += "MAGICARD_REMOTECOMM_ERROR";
				    break; 

			    case MAGICARD_LOCALCOMM_ERROR:
				    strException += "MAGICARD_LOCALCOMM_ERROR";
				    break; 

			    case MAGICARD_SPOOLER_NOT_EMPTY:
				    strException += "MAGICARD_SPOOLER_NOT_EMPTY";
				    break;

			    case MAGICARD_REMOTECOMM_IN_USE:
				    strException += "MAGICARD_REMOTECOMM_IN_USE";
				    break; 

			    case MAGICARD_LOCALCOMM_IN_USE:
				    strException += "MAGICARD_LOCALCOMM_IN_USE";
				    break;

                default:
                    strException += "Unknown error";
                    break;
		    }

		    throw new MagicardException(strException);
	    }

	    public MagiCardAPI(IntPtr PrinterHDC)
	    {
		    MyPrinterHdc = PrinterHDC;
	    }

        public MagiCardAPIVersion GetAPIVersionA()
        {
            byte[] rBytes = new byte[4*8+1];
            IntPtr pApiVersion = Marshal.AllocHGlobal(4*8);
            int res = GetAPIVersion(hSession, pApiVersion);
            Marshal.Copy(pApiVersion, rBytes, 0, 4*8);
            Marshal.FreeHGlobal(pApiVersion);
            return new MagiCardAPIVersion(rBytes);
        }

	    public void EnableReporting()
	    {
            if (reportingEnabled)
                return;
		    IntPtr tmpPhSession = Marshal.AllocHGlobal(IntPtr.Size);
		    int res = EnableStatusReporting(MyPrinterHdc, tmpPhSession, 1);
		    hSession = Marshal.ReadInt32(tmpPhSession, 0);

		    if (!(res == ERROR_SUCCESS)) {
			    ThrowException("EnableReporting", res);
		    }
            reportingEnabled = true;
	    }

	    public void Feed()
	    {
		    IntPtr pJob = Marshal.StringToHGlobalAnsi("Feeding Card");
		    int res = FeedCard(hSession, FEED_CONTACTLESS, 0, pJob);
		    Marshal.FreeHGlobal(pJob);

		    if (!(res == ERROR_SUCCESS)) {
			    ThrowException("Feed", res);
		    }
	    }

	    public MagiCardReturnVal Wait()
	    {

		    Int16 EscapeCount = 30;
		    //MagiCardReturnVal Result = default(MagiCardReturnVal);
            Int32 ret = (Int32) default(MagiCardReturnVal);
		    while (EscapeCount > 0) {
			    ret = WaitForPrinter(hSession);
			    if ((ret == -1)) {
				    System.Threading.Thread.Sleep(1000);
			    } else {
				    break;
			    }
			    EscapeCount -= 1;
		    }

            return (MagiCardReturnVal) ret;

	    }

	    public string GetLastEnduroMessage()
	    {

		    string LastError = string.Empty;

		    //allocates unmanaged memory 
		    IntPtr pErrorMessage = Marshal.AllocHGlobal(128);
           
		    IntPtr pErrorMessageSize = Marshal.AllocHGlobal(sizeof(Int32));
		    Marshal.WriteInt32(pErrorMessageSize, 0, 128);
		    //load error into the memory
		    int errorRes = GetLastEnduroMessage(hSession, pErrorMessage, pErrorMessageSize);
		    if (errorRes == ERROR_SUCCESS) {
                // if return OK, then we have the string and length
			    LastError = Marshal.PtrToStringAuto(pErrorMessage, Marshal.ReadInt32(pErrorMessageSize, 0));
		    } else
            {
                // otherwise we need to repeat the call (pErrorMessageSize will have been written with the right length)
                Marshal.WriteInt32(pErrorMessageSize, 0, Marshal.ReadInt32(pErrorMessageSize));
                errorRes = GetLastEnduroMessage(hSession, pErrorMessage, pErrorMessageSize);
            }
		    //MsgBox(LastError)
		    Marshal.FreeHGlobal(pErrorMessage);
		    Marshal.FreeHGlobal(pErrorMessageSize);
            int pos = LastError.IndexOf('\0');
            if (pos >= 0)
                LastError = LastError.Substring(0, pos);
            return LastError;

	    }

	    public void Eject()
	    {
		    IntPtr pJob = Marshal.StringToHGlobalAnsi("Ejecting Card");
		    int res = EjectCard(hSession, pJob);
		    Marshal.FreeHGlobal(pJob);

		    if (!(res == ERROR_SUCCESS)) {
			    ThrowException("Eject", res);
		    }
	    }

	    public void SendCancel()
	    {
		    IntPtr pCancelMessage = Marshal.StringToHGlobalAnsi("cancel");
		    int res = GeneralCommand(hSession, pCancelMessage);
		    Marshal.FreeHGlobal(pCancelMessage);
	    }

	    public void RequestMag()
	    {
		    int res = RequestMagData(hSession);
		    if (!(res == ERROR_SUCCESS)) {
			    ThrowException("RequestMag", res);
		    }
	    }

	    public void SendEncodeMag(string magNumber)
	    {
		    IntPtr pCancelMessage = Marshal.StringToHGlobalAnsi(magNumber);
		    int res = GeneralCommand(hSession, pCancelMessage);
		    Marshal.FreeHGlobal(pCancelMessage);
	    }

	    public void DisableReporting()
	    {
            if (!reportingEnabled)
                return;
		    int res = DisableStatusReporting(hSession);
		    if (!(res == ERROR_SUCCESS)) {
			    ThrowException("DisableReporting", res);
		    }
            reportingEnabled = false;
	    }

	    public string GetLastError()
	    {

		    return LastError;
	    }

	    public void Reset()
	    {
		    IntPtr pRSTMessage = Marshal.StringToHGlobalAnsi("RST");
		    int res = GeneralCommand(hSession, pRSTMessage);
		    Marshal.FreeHGlobal(pRSTMessage);
	    }

	    public MagiCardStatus GetStatus()
	    {
		    return (MagiCardStatus) GetPrinterStatus(hSession);
	    }

	    public PrinterInfo GetPrinterInfoA()
	    {
		    byte[] rBytes = new byte[501];
		    //for a total length of 192

		    IntPtr pPrinterInfo = Marshal.AllocHGlobal(500);
		    int res = GetPrinterInfo(hSession, pPrinterInfo);
		    Marshal.Copy(pPrinterInfo, rBytes, 0, 500);
		    Marshal.FreeHGlobal(pPrinterInfo);

		    if (!(res == ERROR_SUCCESS)) {
			    ThrowException("GetEnduroStatus", res);
		    }

		    PrinterInfo pInfo = new PrinterInfo(rBytes);
            pInfo.LastEnduroMessage = this.GetLastEnduroMessage();
		    return pInfo;

	    }

    }    
}
