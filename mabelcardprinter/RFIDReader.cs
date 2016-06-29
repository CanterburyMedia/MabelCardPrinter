using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibLogicalAccess;

namespace MabelCardPrinter
{
    class RFIDReaderEventArgs
    {

    }

    class RFIDReader
    {
        public String getNFCToken(int timeout)
        {
            // Explicitly use the PC/SC Reader Provider.
            IReaderProvider readerProvider = new PCSCReaderProvider();

            // Create the default reader unit. On PC/SC, we will listen on all readers.
            IReaderUnit readerUnit = readerProvider.CreateReaderUnit();

            if (readerUnit.ConnectToReader())
            {
                //("Waiting 15 seconds for a card insertion...");
                if (readerUnit.WaitInsertion(timeout*1000))
                {
                    if (readerUnit.Connect())
                    {
                        Console.WriteLine("Card inserted on reader '{0}.'", readerUnit.ConnectedName);

                        chip chip = readerUnit.GetSingleChip();
                        Console.WriteLine("Card type: {0}", chip.Type);
                        Console.WriteLine("Card unique manufacturer number: {0}", readerUnit.GetNumber(chip));

                        readerUnit.Disconnect();
                        return readerUnit.GetNumber(chip);
                    }

                    Console.WriteLine("Logical automatic card removal in 15 seconds...");
                    if (!readerUnit.WaitRemoval(15000))
                    {
                        Console.WriteLine("Card removal forced.");
                    }

                    Console.WriteLine("Card removed.");
                }
                else
                {
                    Console.WriteLine("No card inserted.");
                }
                
            }
            return "No RFID returned";
        }
        
    
    }
}
