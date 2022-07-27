using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQSamples.src.Accounting
{
    public interface IAccountant
    {
        string GetOrders();
        string GetOrdersCountByUser();
        string GetTotalCostForEachUser();
        string GetOutstandingBalanceForEachUser();

    }
}
