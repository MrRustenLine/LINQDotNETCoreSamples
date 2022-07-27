using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQSamples.src.Accounting
{
    public class Parameters: IParameters
    {
        public string OrdersFile { get; set; }

        public string PricesFile { get; set; }

        public string PaymentsFile { get; set; }
    }
}
