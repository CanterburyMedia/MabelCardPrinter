using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Printing;


namespace MabelCardPrinter
{
    class PrintableCard
    {
        public MabelCard card;

        private int pagesPrinted = 0;
        public PrintableCard(MabelCard inCard)
        {
            this.card = inCard;
        }


        
    }
}
