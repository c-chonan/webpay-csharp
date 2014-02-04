using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebPayCheckout;
using WebPayCheckout.Request;

namespace UnitTest
{
    [TestClass]
    public class TestToken
    {

        private WebPayClient client;

        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void Init()
        {
            this.client = Helper.CreateContext();
        }

        [TestMethod]
        [DeploymentItem("TestProject1\\CardValid.csv")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\CardValid.csv", "CardValid#csv", DataAccessMethod.Sequential)]
        public void TokenCreateFromCard()
        {
            var row = this.TestContext.DataRow;

            CardRequest card = Helper.CreateCard(row);

            var res = client.Token.Create(card);

            Assert.AreEqual(res.ObjectType, "token");
            Assert.IsFalse(res.Used);

            Helper.AssertCardAndCSV(res.Cards, row);
        }



    }
}
