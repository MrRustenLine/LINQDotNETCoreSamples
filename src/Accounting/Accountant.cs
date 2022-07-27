using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Diagnostics;
using LINQSamples.src.Auxiliary;

/* Date: 23/6/22
 * Author: Rusten Line
 * Desc: all functionality related to accounting.
 */

namespace LINQSamples.src.Accounting
{
    public class Accountant : IAccountant
    {
        List<Order> orders;
        List<Drink> drinks;
        List<Payment> payments;
        IParameters prms;

        public Accountant(IParameters parameters)
        {
            prms = parameters;
            orders = GetOrdersList();
            drinks = GetDrinksList();
            payments = GetPaymentsList();
        }

        public string GetOrders()  
        {
            return JsonConvert.SerializeObject(orders);
        }

        public string GetOrdersCountByUser()
        {
            var ordersByUser = from o in orders
                                group o by o.User;
            List<OrdersCountByUser> ordersCountByUser = new List<OrdersCountByUser>();
            foreach (var orderGroup in ordersByUser)
            {
                OrdersCountByUser ocbu = new OrdersCountByUser();
                ocbu.name = orderGroup.Key;
                ocbu.count = orderGroup.Count();
                ordersCountByUser.Add(ocbu);
            }
            return JsonConvert.SerializeObject(ordersCountByUser);
        }

        public string GetTotalCostForEachUser()
        {
            var ordersDrinks = orders.Join(drinks,
                                            o => new { Drink_name = o.Drink, Size = o.Size },
                                            p => new { Drink_name = p.Drink_Name, Size = p.Size },
                                            (o, p) => new { o.User, p.Drink_Name, p.Size, p.Price });
            if (orders.Count() != ordersDrinks.Count())
            {
                throw new UnsupportedOrderTypeException("UnsupportedOrderTypeException due to a mismatch between order types in orders and prices files.");
            }
            var result = ordersDrinks
                .GroupBy(u => u.User)
                .Select(tc => new TotalCostForEachUser
                {
                    balance = tc.Sum(p => Convert.ToDecimal(p.Price)),
                    name = tc.Key,
                    count = tc.Count()
                });
            return JsonConvert.SerializeObject(result);
        }

        public string GetOutstandingBalanceForEachUser()
        {
            var ordersTotal = orders.Join(drinks,
                                            o => new { Drink_name = o.Drink, Size = o.Size },
                                            d => new { Drink_name = d.Drink_Name, Size = d.Size },
                                            (o, d) => new { o.User, d.Drink_Name, d.Size, d.Price })
                                            .GroupBy(i => i.User)
                                            .Select (p => new
                                            {
                                                user = p.Key,
                                                orderTotal = p.Sum(i => Convert.ToDecimal(i.Price))
                                            }).ToList();
            var paymentsTotal = payments
            .GroupBy(i => i.User)
            .Select(p => new 
            {
                user = p.Key,
                paymentTotal = p.Sum(i => Convert.ToDecimal(i.Amount)),
            }).ToList();
            // merge 
            var result = from o in ordersTotal
                        join p in paymentsTotal
                            on o.user equals p.user into paymentsOrdersSummary
                            from item in paymentsOrdersSummary
                            select new { item.user, o.orderTotal, item.paymentTotal, Balance = o.orderTotal - item.paymentTotal };
            return JsonConvert.SerializeObject(result);
        }

        public List<Order> GetOrdersList()
        {
            string jsonString = FileReader.Read(prms.OrdersFile);
            return JsonConvert.DeserializeObject<List<Order>>(jsonString);
        }

        public List<Drink> GetDrinksList()
        {
            string jsonString = FileReader.Read(prms.PricesFile);
            List<CompositeDrink> compositeDrinks = JsonConvert.DeserializeObject<List<CompositeDrink>>(jsonString);
            //flatten composite drinks
            List<Drink> drinks = new List<Drink>();
            foreach (CompositeDrink compDrink in compositeDrinks)
            {
                foreach (KeyValuePair<string, decimal> compPrice in compDrink.Prices)
                {
                    Drink drink = new Drink();
                    drink.Drink_Name = compDrink.Drink_Name;
                    drink.Size = compPrice.Key;
                    drink.Price = compPrice.Value;
                    drinks.Add(drink);
                }
            }
            return drinks;
        }

        public List<Payment> GetPaymentsList()
        {
            string jsonString = FileReader.Read(prms.PaymentsFile);
            List<CompositeDrink> compositeDrinks = JsonConvert.DeserializeObject<List<CompositeDrink>>(jsonString);
            List<Payment> payments = JsonConvert.DeserializeObject<List<Payment>>(jsonString);
            return payments;
        }
    }
}
