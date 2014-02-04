using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebPayCheckout.Request;
using WebPayCheckout.Response;

namespace WebPayCheckout.Clients
{

    /// <summary>
    /// 課金に対するクライアント
    /// </summary>
    public class ChargeClient
    {
        private readonly WebPayClient parent;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="parent">親になるWebPayClient</param>
        internal ChargeClient(WebPayClient parent)
        {
            this.parent = parent;
        }

        /// <summary>
        /// 新たに課金を作成します。
        /// </summary>
        /// <param name="charge">新たな課金情報</param>
        /// <returns>作成された課金情報</returns>
        public Charge Create(ChargeRequest charge)
        {
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, new Uri(parent.BaseUri, "charges"));
            message.Content = new FormUrlEncodedContent(charge.ToFormContent(new Dictionary<string, string>()));

            Charge retval = this.parent.SendRequest<Charge>(message);

            return retval;
        }

        /// <summary>
        /// 過去に作成済みの課金オブジェクトを取得します。
        /// </summary>
        /// <param name="id">一意な課金ID</param>
        /// <returns>課金情報</returns>
        public Charge Retrieve(string id)
        {
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, new Uri(parent.BaseUri, "charges/" + id));

            Charge retval = this.parent.SendRequest<Charge>(message);

            return retval;
        }

        /// <summary>
        /// 過去に作成した課金オブジェクトの払い戻し処理を行います。
        /// 払い戻された金銭は課金請求が元々行われたクレジットカードに対して行われます。
        /// ただし、払い戻し前に請求された手数料は払い戻されません。
        /// これは、顧客は損失を被らないですが、開発者は手数料分の損失を被る可能性があることを示しています。
        /// 
        /// あなたは任意で一部のみを払い戻すことができます。
        /// この場合、全額払い戻されるまで何度でも好きなだけ一部払い戻し処理を行うことが可能です。
        /// 
        /// 一旦全額が払い戻されると、これ以降払い戻し処理を行うことはできなくなります。
        /// このrefund処理は、既に払い戻し処理を終えている場合や、払い戻し可能金額（当初の課金額）を超えて払い戻しを行おうとした場合にはエラーが返ります。
        /// </summary>
        /// <param name="id">一意な課金ID</param>
        /// <param name="amount">円単位の正の整数で、払い戻しを行う金額を指定する。当初課金行った額から既に払い戻しを行った額を引いた金額を上限とする。</param>
        /// <returns>課金情報</returns>
        public Charge Refund(string id, long amount)
        {
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, new Uri(parent.BaseUri, "charges/" + id + "/refund"));
            message.Content = new FormUrlEncodedContent(new Dictionary<string, string>() { { "amount", amount.ToString() } });

            Charge retval = this.parent.SendRequest<Charge>(message);

            return retval;
        }

        /// <summary>
        /// 仮売上として作成した課金を、実売上化します。
        /// 仮売上とする課金は事前に「課金の作成」をcapture=falseとして作成しておきます。
        /// 仮売上は作成されてから一定の日数が経過すると失効します。
        /// その時点までに実売上化しなかった場合、払い戻し済みとして扱われ、実売上化できなくなります。
        /// </summary>
        /// <param name="id">一意な課金ID</param>
        /// <returns>課金情報</returns>
        public Charge Capture(string id)
        {
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, new Uri(parent.BaseUri, "charges/" + id + "/capture"));
            message.Content = new FormUrlEncodedContent(new Dictionary<string, string>());

            Charge retval = this.parent.SendRequest<Charge>(message);

            return retval;
        }

        /// <summary>
        /// 仮売上として作成した課金を、実売上化します。
        /// 仮売上とする課金は事前に「課金の作成」をcapture=falseとして作成しておきます。
        /// 仮売上は作成されてから一定の日数が経過すると失効します。
        /// その時点までに実売上化しなかった場合、払い戻し済みとして扱われ、実売上化できなくなります。
        /// </summary>
        /// <param name="id">一意な課金ID</param>
        /// <param name="amount">実売上化をする金額。仮売上の作成時の金額を上限とする。</param>
        /// <returns>課金情報</returns>
        public Charge Capture(string id, long amount)
        {
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, new Uri(parent.BaseUri, "charges/" + id + "/capture"));
            message.Content = new FormUrlEncodedContent(new Dictionary<string, string>() { { "amount", amount.ToString() } });

            Charge retval = this.parent.SendRequest<Charge>(message);

            return retval;
        }

        /// <summary>
        /// 過去に作成した課金のリストを取得します。
        /// </summary>
        /// <returns>課金情報のリスト</returns>
        public ChargeList All()
        {
            return this.All(null, null);
        }

        /// <summary>
        /// 過去に作成した課金のリストを取得します。
        /// </summary>
        /// <param name="list">リストを取得する際の条件</param>
        /// <returns>課金情報のリスト</returns>
        public ChargeList All(ListRequest list)
        {
            return this.All(list, null);
        }

        /// <summary>
        /// 過去に作成した課金のリストを取得します。
        /// </summary>
        /// <param name="customerid">課金情報を取得する対象の顧客ID</param>
        /// <returns>課金情報のリスト</returns>
        public ChargeList All(string customerid)
        {
            return this.All(new ListRequest(), customerid);
        }

        /// <summary>
        /// 過去に作成した課金のリストを取得します。
        /// </summary>
        /// <param name="list">リストを取得する際の条件</param>
        /// <param name="customerid">課金情報を取得する対象の顧客ID</param>
        /// <returns>課金情報のリスト</returns>
        public ChargeList All(ListRequest list, string customerid)
        {

            string query = string.Empty;
            if (list != null || !string.IsNullOrWhiteSpace(customerid))
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();

                if (list != null)
                {
                    list.ToFormContent(dic);
                }

                if (!string.IsNullOrWhiteSpace(customerid))
                {
                    dic.Add("customer", customerid);
                }

                query = HelperImpl.ToQueryString(dic);
            }

            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, new Uri(parent.BaseUri, "charges?" + query));

            ChargeList retval = this.parent.SendRequest<ChargeList>(message);

            return retval;
        }

    }
}
