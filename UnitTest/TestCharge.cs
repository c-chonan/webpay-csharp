using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebPayCheckout;
using System.Net;
using WebPayCheckout.Request;
using WebPayCheckout.Response;
using System.Collections.Generic;
using System.Linq;

namespace UnitTest
{
    [TestClass]
    public class TestCharge
    {
        /// <summary>
        /// クライアント
        /// </summary>
        private WebPayClient client;

        /// <summary>
        /// テストのコンテキスト
        /// </summary>
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
        [DeploymentItem("TestProject1\\CardValid.csv")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\CardValid.csv", "CardValid#csv", DataAccessMethod.Sequential)]
        [Description("カードから直接課金するパターン")]
        public void ChargeCreateFromCard()
        {
            var row = this.TestContext.DataRow;

            //カードを作る
            CardRequest card = Helper.CreateCard(row);

            //課金を作成
            ChargeRequest charge = new ChargeRequest();
            charge.Amount = 500;
            charge.Description = "カードから直接課金";
            charge.Card = card;

            var res = client.Charge.Create(charge);
            {
                Helper.AssertChargeReqAndRes(charge, res);
                Assert.AreEqual(res.AmountRefunded, 0);

                Assert.IsTrue(res.Captured);
                Assert.IsTrue(res.Paid);
                Assert.IsFalse(res.Refunded);

                Assert.IsNull(res.Customer);
                Assert.IsNull(res.FailureMessage);

                Helper.AssertCardAndCSV(res.Card, row);
            }

            //課金を検証
            var retrieved = client.Charge.Retrieve(res.ID);
            {
                Helper.AssertChargeAndCharge(res, retrieved);
            }
        }

        [TestMethod]
        [Description("顧客から課金するパターン")]
        public void ChargeCreateFromCustomer()
        {
            CustomerList list = this.client.Customer.All();
            Customer customer = list.Customers[0];

            //課金を作成
            ChargeRequest charge = new ChargeRequest();
            charge.Amount = 500;
            charge.Description = "顧客から課金";
            charge.Customer = customer.ID;

            var res = client.Charge.Create(charge);
            {
                Helper.AssertChargeReqAndRes(charge, res);
                Assert.AreEqual(res.AmountRefunded, 0);

                Assert.IsTrue(res.Captured);
                Assert.IsTrue(res.Paid);
                Assert.IsFalse(res.Refunded);

                Assert.IsNull(res.FailureMessage);

                Helper.AssertCardAndCard(customer.ActiveCard, res.Card);
            }

            //課金を検証
            var retrieved = client.Charge.Retrieve(res.ID);
            {
                Helper.AssertChargeAndCharge(res, retrieved);
            }
        }



        [TestMethod]
        [DeploymentItem("TestProject1\\CardValid.csv")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\CardValid.csv", "CardValid#csv", DataAccessMethod.Sequential)]
        [Description("トークンから課金するパターン")]
        public void ChargeCreateFromToken()
        {
            var row = this.TestContext.DataRow;

            //カードを作る
            CardRequest card = Helper.CreateCard(row);

            //トークン作る
            Token token = this.client.Token.Create(card);
            {
                Assert.AreEqual(token.ObjectType, "token");
                Assert.IsFalse(token.Used);

                Helper.AssertCardAndCSV(token.Cards, row);
            }

            //課金を作成
            ChargeRequest charge = new ChargeRequest();
            charge.Amount = 500;
            charge.Description = "トークンから課金";
            charge.CardToken = token.ID;

            var res = client.Charge.Create(charge);
            {
                Helper.AssertChargeReqAndRes(charge, res);
                Assert.AreEqual(res.AmountRefunded, 0);

                Assert.IsTrue(res.Captured);
                Assert.IsTrue(res.Paid);
                Assert.IsFalse(res.Refunded);

                Assert.IsNull(res.Customer);
                Assert.IsNull(res.FailureMessage);

                Helper.AssertCardAndCSV(res.Card, row);
            }

            //課金を検証
            var retrieved = client.Charge.Retrieve(res.ID);
            {
                Helper.AssertChargeAndCharge(res, retrieved);
            }

            //使い終わったトークンを検証
            var usedToken = this.client.Token.Retrieve(token.ID);
            {
                Assert.AreEqual(usedToken.ObjectType, "token");
                Assert.AreEqual(usedToken.Created, token.Created);
                Assert.IsTrue(usedToken.Used);

                Helper.AssertCardAndCard(token.Cards, usedToken.Cards);
            }

        }

        [TestMethod]
        [Description("課金の一部払い戻しパターン")]
        public void ChargeRefundPart()
        {
            //とりあえず一覧を取ってくる
            var charges = this.AllCharges();

            //払戻されていないものだけ取る
            var notrefunded = from c in charges where !c.Refunded && c.AmountRefunded == 0 && c.Paid select c;

            //一部払い戻し
            foreach (Charge c in notrefunded.Take(10))
            {
                int amount = c.Amount / 10;
                var refund = this.client.Charge.Refund(c.ID, amount);

                Assert.AreEqual(refund.AmountRefunded, amount + c.AmountRefunded);
                Assert.IsFalse(refund.Refunded);
                Assert.IsNull(refund.FailureMessage);
            }

        }

        [TestMethod]
        [Description("課金の全部払い戻しパターン")]
        public void ChargeRefundAll()
        {
            //とりあえず一覧を取ってくる
            var charges = this.AllCharges();

            //払戻されていないものだけ取る
            var notrefunded = from c in charges where !c.Refunded && c.AmountRefunded != 0 && c.Paid select c;

            //全部払い戻し
            foreach (Charge c in notrefunded.Take(10))
            {
                int amount = c.Amount - c.AmountRefunded;
                var refund = this.client.Charge.Refund(c.ID, amount);

                Assert.AreEqual(refund.AmountRefunded, refund.Amount);
                Assert.IsTrue(refund.Refunded);
                Assert.IsNull(refund.FailureMessage);
            }

        }

        [TestMethod]
        [DeploymentItem("TestProject1\\CardValid.csv")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\CardValid.csv", "CardValid#csv", DataAccessMethod.Sequential)]
        [Description("仮売上から実売上")]
        public void ChargeCapture()
        {
            var row = this.TestContext.DataRow;

            Charge response = null;
            {
                //カードを作る
                CardRequest card = Helper.CreateCard(row);

                //課金を作成
                ChargeRequest charge = new ChargeRequest();
                charge.Amount = 500;
                charge.Description = "仮の売上";
                charge.Capture = false;//これで仮売上
                charge.Card = card;

                response = client.Charge.Create(charge);
                {
                    Helper.AssertChargeReqAndRes(charge, response);
                    Assert.AreEqual(response.AmountRefunded, 0);

                    Assert.IsFalse(response.Paid);
                    Assert.IsFalse(response.Captured);
                    Assert.IsFalse(response.Refunded);

                    Assert.IsNull(response.Customer);
                    Assert.IsNull(response.FailureMessage);

                    Helper.AssertCardAndCSV(response.Card, row);
                }
            }

            {
                var captured = this.client.Charge.Capture(response.ID, response.Amount);

                Assert.AreEqual(captured.Amount, response.Amount);
                Assert.IsTrue(captured.Captured);
                Assert.IsNull(captured.FailureMessage);
            }

        }


        private List<Charge> AllCharges()
        {
            List<Charge> retval = new List<Charge>();
            {
                int loops = 1;
                ListRequest req = new ListRequest();
                for (int i = 0; i <= loops; i++)
                {
                    req.Offset = req.Count * i;
                    var res = client.Charge.All(req);
                    retval.AddRange(res.Charges);

                    loops = res.Count / req.Count;
                }
            }

            return retval;
        }

    }
}
