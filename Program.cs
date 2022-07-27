
using LINQSamples.src.Accounting;
using Newtonsoft.Json;

namespace LINQSamples
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Parameters prms = new Parameters();
            prms.OrdersFile = "orders.json";
            prms.PaymentsFile = "payments.json";
            prms.PricesFile = "prices.json";

            Accountant accountant = new Accountant(prms);

            //Requirement No 1
            List<Order> orders = accountant.GetOrdersList();
            foreach (Order order in orders)
            {
                Console.Write(order.User + ", " + order.Drink + ", " + order.Size + Environment.NewLine);
            }

            //Requirement No 2
            string jsonCountOrdersByUser = accountant.GetOrdersCountByUser();

            //Requirement No 3
            string jsonTotalCostForEachUser = accountant.GetTotalCostForEachUser();

            //Requirement 5
            string jsonOutstandingBalance = accountant.GetOutstandingBalanceForEachUser();

            //Requirement 6
            prms.OrdersFile = "ordersunsupportedordertype.json";
            accountant = new Accountant(prms);
            jsonTotalCostForEachUser = accountant.GetTotalCostForEachUser();
        }
    }
}