using LINQSamples;
using LINQSamples.src.Accounting;
using Newtonsoft.Json;
using LINQSamples.src.Auxiliary;

namespace LINQSamplesTestProject
{
    public class Tests
    {
        Parameters prms;
        Accountant accountant;
        [SetUp]
        public void Setup()
        {
            prms = new Parameters();
            prms.OrdersFile = "orders.json";
            prms.PaymentsFile = "payments.json";
            prms.PricesFile = "prices.json";
            accountant = new Accountant(prms);
        }

        [Test]
        public void FileReaderRead()
        {
            string jsonString = FileReader.Read(prms.OrdersFile);
            Assert.IsNotEmpty(jsonString);

            jsonString = FileReader.Read(prms.PaymentsFile);
            Assert.IsNotEmpty(jsonString);

            jsonString = FileReader.Read(prms.PricesFile);
            Assert.IsNotEmpty(jsonString);
        }

        [Test]
        public void GetOrdersList()
        {
            List<Order> orders = accountant.GetOrdersList();
            Assert.That(orders.Count == 100);
        }

        [Test]
        public void GetDrinksList()
        {
            List<Drink> drinks = accountant.GetDrinksList();
            Assert.That(drinks.Count == 16);
        }

        [Test]
        public void GetPaymentsList()
        {
            List<Payment> payments = accountant.GetPaymentsList();
            Assert.That(payments.Count == 20);
        }

        [Test]
        public void GetOrders()
        {
            string jsonString = accountant.GetOrders();
            Assert.IsNotEmpty(jsonString);
        }

        [Test]
        public void GetOrdersCountByUser()
        {
            string jsonString = accountant.GetOrdersCountByUser();
            Assert.IsNotEmpty(jsonString);
        }

        [Test]
        public void GetTotalCostForEachUser()
        {
 
            prms.PricesFile = "prices.json";
            prms.OrdersFile = "ordersunittest1.json";
            Accountant accountant = new Accountant(prms);

            string jsonOrdersPrices = accountant.GetTotalCostForEachUser();

            List<TotalCostForEachUser> totalCostsForEachUser = JsonConvert.DeserializeObject<List<TotalCostForEachUser>>(jsonOrdersPrices);
            foreach (TotalCostForEachUser user in totalCostsForEachUser)
            {
                if (user.name == "ellis")
                {
                    int userCountExpected = 5;
                    decimal userBalanceExpected = 19.25m;
                    Assert.AreEqual(user.count, userCountExpected);
                    Assert.AreEqual(user.balance, userBalanceExpected);
                }
            }
        }

        [Test]
        public void GetOutstandingBalanceForEachUser()
        {
            string jsonString = accountant.GetOutstandingBalanceForEachUser();
            Assert.IsNotEmpty(jsonString);
        }
    }
}