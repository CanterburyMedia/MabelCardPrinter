using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace MabelCardPrinter
{
    public class PrinterInfo
    {
        public bool bPrinterConnected = new bool();
        public Int32 eModel = new Int32();
        public char[] sModel = new char[32];
        public Int32 ePrintheadtype = new Int32();
        public char[] sPrinterSerial = new char[20];
        public char[] sPrintheadSerial = new char[20];
        public char[] sPCBSerial = new char[20];
        public char[] sFirmwareVersion = new char[20];

        public Int32 iES_Density = new Int32();
        //public char[] sPCBVersion = new char[20];
        public Int32 iHandFeed = new Int32();
        public Int32 iCardsPrinted = new Int32();
        public Int32 iCardsOnPrinthead = new Int32();
        public Int32 iDyePanelsPrinted = new Int32();
        public Int32 iCleansSinceShipped = new Int32();
        public Int32 iDyePanelsSinceClean = new Int32();
        public Int32 iCardsSinceClean = new Int32();

        public Int32 iCardsBetweenCleans = new Int32();
        public Int32 iPrintHeadPosn = new Int32();
        public Int32 iImageStartPosn = new Int32();
        public Int32 iImageEndPosn = new Int32();
        public Int32 iMajorError = new Int32();
        public Int32 iMinorError = new Int32();
        public char[] sTagUID = new char[20];
        public Int32 iShotsOnFilm = new Int32();
        public Int32 iShotsUsed = new Int32();
        public char[] sDyeFilmType = new char[20];
        public Int32 iColourLength = new Int32();
        public Int32 iResinLength = new Int32();
        public Int32 iOvercoatLength = new Int32();
        public Int32 eDyeFlags = new Int32();
        public Int32 iCommandCode = new Int32();
        public Int32 iDOB = new Int32();
        public Int32 eDyeFilmManuf = new Int32();
        public Int32 eDyeFilmProg = new Int32();

        public PrinterInfo()
        {
        }


        public PrinterInfo(byte[] rBytes)
        {
            int i = 0;
            System.IO.MemoryStream ms = new System.IO.MemoryStream(rBytes);
            System.IO.BinaryReader br = new System.IO.BinaryReader(ms);

            bPrinterConnected = Convert.ToBoolean(br.ReadInt32());
            eModel = br.ReadInt32();
            sModel = br.ReadChars(30);
            ePrintheadtype = br.ReadInt32();
            sPrinterSerial = br.ReadChars(20);
            sPrintheadSerial = br.ReadChars(20);
            sPCBSerial = br.ReadChars(20);

            byte[] sFV = new byte[20];
            sFV = br.ReadBytes(20); 
            
            for (i = 0; i <= 19; i += 2)
            {
                char tchar = (char) BitConverter.ToInt16(sFV, i);
                sFirmwareVersion[Convert.ToInt32(i / 2)] = tchar;
            }

            byte[] dummy = new byte[16];
            dummy = br.ReadBytes(16);
            //  sPCBVersion = br.ReadChars(20);

            iES_Density = br.ReadInt32();
            iHandFeed = br.ReadInt32();
            iCardsPrinted = br.ReadInt32();
            iCardsOnPrinthead = br.ReadInt32();
            iDyePanelsPrinted = br.ReadInt32();
            iCleansSinceShipped = br.ReadInt32();
            iDyePanelsSinceClean = br.ReadInt32();
            iCardsSinceClean = br.ReadInt32();
            iCardsBetweenCleans = br.ReadInt32();

            iPrintHeadPosn = br.ReadInt32();
            iImageStartPosn = br.ReadInt32();
            //is only returning 0 don't quite know why yet
            iImageEndPosn = br.ReadInt32();
            iMajorError = br.ReadInt32();
            iMinorError = br.ReadInt32();
            sTagUID = br.ReadChars(20);
            iShotsOnFilm = br.ReadInt32();
            iShotsUsed = br.ReadInt32();
            sDyeFilmType = br.ReadChars(20);
            iColourLength = br.ReadInt32();
            iResinLength = br.ReadInt32();
            iOvercoatLength = br.ReadInt32();
            eDyeFlags = br.ReadInt32();
            iCommandCode = br.ReadInt32();
            iDOB = br.ReadInt32();
            eDyeFilmManuf = br.ReadInt32();
            eDyeFilmProg = br.ReadInt32();

        }
    }
}
