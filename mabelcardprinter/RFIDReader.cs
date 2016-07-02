using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibLogicalAccess;

namespace MabelCardPrinter
{
    class RFIDReader
    {
        private IReaderUnit readerUnit;
        private IReaderProvider readerProvider;
        private bool waitCancelled = false;

        public void CancelWait()
        {
            waitCancelled = true;
        }
        public String ReadRFIDToken(int timeout)
        {
            // Explicitly use the PC/SC Reader Provider.
            readerProvider = new PCSCReaderProvider();

            // Create the default reader unit. On PC/SC, we will listen on all readers.
            readerUnit = readerProvider.CreateReaderUnit();

            if (readerUnit.ConnectToReader())
            {
                //("Waiting x seconds for a card insertion...");
                int i = 0;
                bool readCard = false;
                String readID = "";
                while (i < timeout && !waitCancelled && !readCard)
                {
                    i++;
                    if (readerUnit.WaitInsertion(1000))
                    {
                        if (readerUnit.Connect())
                        {
                            chip chip = readerUnit.GetSingleChip();
                            readerUnit.Disconnect();
                            readCard = true;
                            readID = readerUnit.GetNumber(chip);
                        }
                    }
                }
                return readID;
            }
            throw new Exception("No RFID Reader connected");
        }
        
    public bool WaitForRemoval()
        {
            return readerUnit.WaitRemoval(Properties.Settings.Default.RFIDTimeout*1000);
        }
    }
}
