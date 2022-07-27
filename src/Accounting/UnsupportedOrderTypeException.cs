using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQSamples.src.Accounting
{
    [Serializable]
    internal class UnsupportedOrderTypeException: Exception
    {
        internal UnsupportedOrderTypeException(string message)
    : base(message)
        {
        }


    }
}
