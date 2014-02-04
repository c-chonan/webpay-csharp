using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebPayCheckout;
using System.Net;
using WebPayCheckout.Request;
using WebPayCheckout.Response;

namespace UnitTest
{
    [TestClass]
    public class TestCustomer
    {

        private WebPayClient client;

        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void Init()
        {
            this.client = Helper.CreateContext();
        }



        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [Description("正常系のテスト")]
        [DeploymentItem("TestProject1\\CardValid.csv")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\CardValid.csv", "CardValid#csv", DataAccessMethod.Sequential)]
        public void CustomerCreate()
        {
            var row = this.TestContext.DataRow;

            CardRequest card = Helper.CreateCard(row);

            CustomerRequest customer = new CustomerRequest();
            customer.Email = "popo@example.com";
            customer.Description = "普通にできるはず";
            customer.Card = card;

            var res = client.Customer.Create(customer);

            res.Email = "popo@example.com";
            res.Description = "普通にできるはず";

            Helper.AssertCardAndCSV(res.ActiveCard, row);
        }


        [TestMethod]
        [Description("異常系のテスト")]
        [DeploymentItem("TestProject1\\CardNumberError.csv")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\CardNumberError.csv", "CardNumberError#csv", DataAccessMethod.Sequential)]
        public void CustomerCreateError()
        {
            var row = this.TestContext.DataRow;

            CardRequest card = Helper.CreateCard(row);

            CustomerRequest cus = new CustomerRequest();
            cus.Card = card;
            cus.Email = "nanika@example.com";
            cus.Description = "エラーが起こるパターン";

            try
            {
                var res = client.Customer.Create(cus);
            }
            catch (CardException e)
            {
                Assert.AreEqual(e.Code, row["ErrorCode"].ToString());
            }

        }

        [TestMethod]
        public void CustomerAll()
        {
            int loops = 1;
            List<string> ids = new List<string>();

            ListRequest req = new ListRequest();
            CustomerList res = null;

            for (int i = 0; i <= loops; i++)
            {
                req.Offset = req.Count * i;
                res = client.Customer.All(req);

                Assert.AreEqual(res.Url, "/v1/customers");
                Assert.AreEqual(res.ObjectType, "list");

                foreach (var cus in res.Customers)
                {
                    Assert.IsFalse(ids.Contains(cus.ID));
                    ids.Add(cus.ID);
                }

                loops = res.Count / req.Count;
            }

            Assert.AreEqual(res.Count, ids.Count);
        }

        [TestMethod]
        public void CustomerDelete()
        {
            CustomerList list = this.client.Customer.All();
            string id = list.Customers[0].ID;
            bool retval = this.client.Customer.Delete(id);

            Assert.IsTrue(retval);

            Customer deleted = this.client.Customer.Retrieve(id);
            Assert.AreEqual(deleted.ID, id);
            Assert.IsTrue(deleted.Deleted);
        }

        [TestMethod]
        public void CustomerUpdate()
        {
            CustomerList list = this.client.Customer.All();
            Customer before = list.Customers[0];

            {
                CustomerRequest req = new CustomerRequest();
                req.Email = "atarashii@example.com";
                req.Description = "中身を修正してますよ";
                Customer after = this.client.Customer.Update(before.ID, req);

                Assert.AreEqual(req.Description, after.Description);
                Assert.AreEqual(req.Email, after.Email);

                Assert.AreEqual(before.ID, after.ID);
                Assert.AreEqual(before.Created, after.Created);

                Helper.AssertCardAndCard(before.ActiveCard, after.ActiveCard);
            }

        }


    }
}
