using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebPayCheckout;
using WebPayCheckout.Request;
using WebPayCheckout.Response;

namespace UnitTest
{
    class Helper
    {

        public void ValidateCard()
        {

        }

        public static WebPayClient CreateContext()
        {
            //ゲスト用シークレットキーを使ってます
            WebPayClient retval = new WebPayClient("test_secret_eHn4TTgsGguBcW764a2KA8Yd");

            ////認証プロキシが挟まる場合はここで設定
            //{
            //    WebProxy proxy = new WebProxy("プロキシのURL", 8080);
            //    proxy.Credentials = new NetworkCredential("ユーザー", "パスワード");
            //    retval.Handler.Proxy = proxy;
            //}

            return retval;
        }

        public static CardRequest CreateCard(DataRow row)
        {
            CardRequest retval = new CardRequest();
            retval.Number = row["CardNumber"].ToString();
            retval.Name = row["Name"].ToString();
            retval.ExpMonth = int.Parse(row["ExpMonth"].ToString());
            retval.ExpYear = int.Parse(row["ExpYear"].ToString());
            retval.CVC = int.Parse(row["CVC"].ToString());

            return retval;
        }


        public static void AssertCardAndCSV(Card card, DataRow row)
        {
            Assert.AreEqual(card.ObjectType, "card");
            Assert.AreEqual(card.CvcCheck, "pass");

            Assert.AreEqual(card.Type, row["CardType"].ToString());
            Assert.AreEqual(card.ExpMonth.ToString(), row["ExpMonth"].ToString());

            Assert.IsTrue(card.ExpYear.ToString().EndsWith(row["ExpYear"].ToString()));
            Assert.IsTrue(row["CardNumber"].ToString().EndsWith(card.Last4));
        }

        public static void AssertCardAndCard(Card before, Card after)
        {
            Assert.AreEqual(before.ObjectType, "card");
            Assert.AreEqual(after.ObjectType, "card");

            Assert.AreEqual(before.CvcCheck, "pass");
            Assert.AreEqual(after.CvcCheck, "pass");

            Assert.AreEqual(before.Fingerprint, after.Fingerprint);
            Assert.AreEqual(before.Last4, after.Last4);
            Assert.AreEqual(before.Name, after.Name);
            Assert.AreEqual(before.ExpYear, after.ExpYear);
            Assert.AreEqual(before.ExpMonth, after.ExpMonth);
        }

        public static void AssertChargeReqAndRes(ChargeRequest req, Charge res)
        {
            Assert.AreEqual(res.ObjectType, "charge");

            Assert.AreEqual(res.Amount, req.Amount);
            Assert.AreEqual(res.Description, req.Description);
            Assert.AreEqual(res.Customer, req.Customer);
            Assert.AreEqual(res.Currency, req.Currency);
            Assert.AreEqual(res.Customer, req.Customer);
        }

        public static void AssertChargeAndCharge(Charge before, Charge after)
        {
            Assert.AreEqual(before.Amount, after.Amount);
            Assert.AreEqual(before.AmountRefunded, after.AmountRefunded);
            Assert.AreEqual(before.Captured, after.Captured);
            Assert.AreEqual(before.Created, after.Created);
            Assert.AreEqual(before.Currency, after.Currency);
            Assert.AreEqual(before.Customer, after.Customer);
            Assert.AreEqual(before.Description, after.Description);
            Assert.AreEqual(before.ExpireTime, after.ExpireTime);
            Assert.AreEqual(before.FailureMessage, after.FailureMessage);
            Assert.AreEqual(before.ID, after.ID);
            Assert.AreEqual(before.Paid, after.Paid);
            Assert.AreEqual(before.Refunded, after.Refunded);

            Helper.AssertCardAndCard(before.Card, after.Card);
        }

    }
}
