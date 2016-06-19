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
	    private static extern Int32 FeedCardA(Int32 hSession, UInt32 dwMode, Int32 iParam, IntPtr JobName);

	    [DllImport("c:\\windows\\system32\\MagAPI.dll")]
	    private static extern Int32 EjectCard(Int32 hSession, IntPtr JobName);

	    [DllImport("c:\\windows\\system32\\MagAPI.dll")]
	    private static extern Int32 WaitForPrinter(Int32 hSession);

	    [DllImport("c:\\windows\\system32\\MagAPI.dll")]
	    private static extern Int32 RequestMagData(Int32 hSession);

	    [DllImport("c:\\windows\\system32\\MagAPI.dll")]
	    private static extern Int32 ReadMagData(Int32 hSession, IntPtr pMSV);

	    [DllImport("c:\\windows\\system32\\MagAPI.dll")]
	    private static extern Int32 GetLastPrinterMessage(Int32 hSession, IntPtr lpszBuffer, IntPtr pdwBufferSize);

	    [DllImport("c:\\windows\\system32\\MagAPI.dll")]
	    private static extern Int32 GetLastEnduroMessage(Int32 hSession, IntPtr lpszBuffer, IntPtr pdwBufferSize);

	    [DllImport("c:\\windows\\system32\\MagAPI.dll")]
	    private static extern Int32 GeneralCommand(Int32 hSession, IntPtr lpszCommandString);

	    [DllImport("c:\\windows\\system32\\MagAPI.dll")]
	    private static extern Int32 GetPrinterStatus(Int32 hSession);

	    [DllImport("c:\\windows\\system32\\MagAPI.dll")]
	    private static extern Int32 GetEnduroInfo(Int32 hSession, IntPtr pPrinterInfo);

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

	    private string LastError = "";
	    private IntPtr MyPrinterHdc;
        private Int32 hSession;

	    private IntPtr pJobName;
	    private void ThrowException(string FunctionName, int Err)
	    {
		    string strException = FunctionName + " Has returned a result code of: " + Err + " - ";
		    switch ((Err)) {
			    //Case MagiCardReturnVal.MAGICARD_DRIVER_NOTCOMPLIANT
			    //    strException += "MAGICARD_DRIVER_NOTCOMPLIANT"
			    //    Exit Select
			    //Case MagiCardReturnVal.MAGICARD_ERROR
			    //    strException += "MagiCard Error"
			    //    Exit Select
			    //Case MagiCardReturnVal.MAGICARD_LOCALCOMM_ERROR
			    //    strException += "MAGICARD_LOCALCOMM_ERROR"
			    //    Exit Select
			    //Case MagiCardReturnVal.MAGICARD_LOCALCOMM_IN_USE
			    //    strException += "MAGICARD_LOCALCOMM_IN_USE"
			    //    Exit Select
			    //Case MagiCardReturnVal.MAGICARD_OPENPRINTER_ERROR
			    //    strException += "MAGICARD_OPENPRINTER_ERROR"
			    //    Exit Select
			    //Case MagiCardReturnVal.MAGICARD_REMOTECOMM_ERROR
			    //    strException += "MAGICARD_REMOTECOMM_ERROR"
			    //    Exit Select
			    //Case MagiCardReturnVal.MAGICARD_REMOTECOMM_IN_USE
			    //    strException += "MAGICARD_REMOTECOMM_IN_USE"
			    //    Exit Select
			    //Case MagiCardReturnVal.MAGICARD_SPOOLER_NOT_EMPTY
			    //    strException += "MAGICARD_SPOOLER_NOT_EMPTY"
			    //    Exit Select


			    case MAGICARD_ERROR:
				    strException += "MagiCard Error";
				    break; // TODO: might not be correct. Was : Exit Select

			    case MAGICARD_PRINTER_ERROR:
				    strException += "MAGICARD_PRINTER_ERROR";
				    break; // TODO: might not be correct. Was : Exit Select

			    case MAGICARD_DRIVER_NOTCOMPLIANT:
				    strException += "MAGICARD_DRIVER_NOTCOMPLIANT";
				    break; // TODO: might not be correct. Was : Exit Select

			    case MAGICARD_OPENPRINTER_ERROR:
				    strException += "MAGICARD_OPENPRINTER_ERROR";
				    break; // TODO: might not be correct. Was : Exit Select

			    case MAGICARD_REMOTECOMM_ERROR:
				    strException += "MAGICARD_REMOTECOMM_ERROR";
				    break; // TODO: might not be correct. Was : Exit Select

			    case MAGICARD_LOCALCOMM_ERROR:
				    strException += "MAGICARD_LOCALCOMM_ERROR";
				    break; // TODO: might not be correct. Was : Exit Select

			    case MAGICARD_SPOOLER_NOT_EMPTY:
				    strException += "MAGICARD_SPOOLER_NOT_EMPTY";
				    break; // TODO: might not be correct. Was : Exit Select

			    case MAGICARD_REMOTECOMM_IN_USE:
				    strException += "MAGICARD_REMOTECOMM_IN_USE";
				    break; // TODO: might not be correct. Was : Exit Select

			    case MAGICARD_LOCALCOMM_IN_USE:
				    strException += "MAGICARD_LOCALCOMM_IN_USE";
				    break; // TODO: might not be correct. Was : Exit Select
		    }

		    throw new Exception(strException);
	    }

	    public MagiCardAPI(IntPtr PrinterHDC)
	    {
		    MyPrinterHdc = PrinterHDC;
		    LastError = "";
	    }

	    public void EnableReporting()
	    {
		    IntPtr tmpPhSession = Marshal.AllocHGlobal(IntPtr.Size);
		    int res = EnableStatusReporting(MyPrinterHdc, tmpPhSession, 1);
		    hSession = Marshal.ReadInt32(tmpPhSession, 0);

		    if (!(res == ERROR_SUCCESS)) {
			    ThrowException("EnableReporting", res);
		    }
	    }

	    public void Feed()
	    {
		    IntPtr pJob = Marshal.StringToHGlobalAnsi("Feeding Card");
		    int res = FeedCardA(hSession, FEED_CONTACTLESS, 0, pJob);
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
		    IntPtr pErrorMessage = Marshal.AllocHGlobal(256);
		    IntPtr pErrorMessageSize = Marshal.AllocHGlobal(4);
		    Marshal.WriteInt32(pErrorMessageSize, 0, 256);
		    //load error into the memory
		    int errorRes = GetLastEnduroMessage(hSession, pErrorMessage, pErrorMessageSize);
		    if (errorRes == ERROR_SUCCESS) {
			    LastError = Marshal.PtrToStringAuto(pErrorMessage, Marshal.ReadInt32(pErrorMessageSize, 0));
		    }
		    //MsgBox(LastError)
		    Marshal.FreeHGlobal(pErrorMessage);
		    Marshal.FreeHGlobal(pErrorMessageSize);

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
		    //Dim pCancelMessage As IntPtr = Marshal.StringToHGlobalAnsi("~2;" + magNumber + "?")
		    IntPtr pCancelMessage = Marshal.StringToHGlobalAnsi(magNumber);
		    int res = GeneralCommand(hSession, pCancelMessage);
		    Marshal.FreeHGlobal(pCancelMessage);
	    }

	    public void DisableReporting()
	    {
		    int res = DisableStatusReporting(hSession);
		    if (!(res == ERROR_SUCCESS)) {
			    ThrowException("DisableReporting", res);
		    }
	    }

	    public string GetLastError()
	    {
		    //'LastError = ""
		    //'Dim pErrorMessage As IntPtr = Marshal.AllocHGlobal(256)
		    //'Dim pErrorMessageSize As IntPtr = Marshal.AllocHGlobal(4)
		    //'Marshal.WriteInt32(pErrorMessageSize, 0, 256)
		    //'Dim errorRes As Integer = GetLastEnduroMessage(hSession, pErrorMessage, pErrorMessageSize)
		    //'If errorRes = ERROR_SUCCESS Then
		    //'    LastError = Marshal.PtrToStringAuto(pErrorMessage, Marshal.ReadInt32(pErrorMessageSize, 0))
		    //'End If
		    // ''MsgBox(LastError)
		    //'Marshal.FreeHGlobal(pErrorMessage)
		    //'Marshal.FreeHGlobal(pErrorMessageSize)

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

	    public PrinterInfo GetEnduroStatus()
	    {
		    byte[] rBytes = new byte[501];
		    //for a total length of 192

		    IntPtr pPrinterInfo = Marshal.AllocHGlobal(500);
		    int res = GetEnduroInfo(hSession, pPrinterInfo);
		    Marshal.Copy(pPrinterInfo, rBytes, 0, 500);
		    Marshal.FreeHGlobal(pPrinterInfo);

		    if (!(res == ERROR_SUCCESS)) {
			    ThrowException("GetEnduroStatus", res);
		    }

		    PrinterInfo pInfo = new PrinterInfo(rBytes);

		    return pInfo;

	    }

    }    
}
