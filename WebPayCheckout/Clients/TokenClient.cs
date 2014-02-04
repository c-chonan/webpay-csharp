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
    /// トークンに対するクライアント
    /// </summary>
    public class TokenClient
    {

        private readonly WebPayClient parent;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="parent">親になるWebPayClient</param>
        internal TokenClient(WebPayClient parent)
        {
            this.parent = parent;
        }

        /// <summary>
        /// クレジットカード詳細を隠蔽する一度だけ使用可能な使い切りトークンを作成します。
        /// このトークンは、このAPIのあらゆるメソッドで、クレジットカード情報の入ったディクショナリの代わりとして使用することができます。
        /// これらのトークンは、使い捨てで一度しか使用することができません。
        /// トークンは、新しい課金を行うか、もしくは顧客("customer")オブジェクトに紐づけるかの、二つの使い方があります。
        /// </summary>
        /// <param name="card">クレジットカード情報</param>
        /// <returns>トークン</returns>
        public Token Create(CardRequest card)
        {
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, new Uri(parent.BaseUri, "tokens/"));
            message.Content = new FormUrlEncodedContent(card.ToFormContent(new Dictionary<string, string>()));

            Token retval = this.parent.SendRequest<Token>(message);

            return retval;
        }

        /// <summary>
        /// 指定したトークンIDと一致する作成済みのクレジットカードトークンオブジェクトを取得します。
        /// </summary>
        /// <param name="id">取得したいトークンのID</param>
        /// <returns>トークン</returns>
        public Token Retrieve(string id)
        {
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, new Uri(parent.BaseUri, "tokens/" + id));

            Token retval = this.parent.SendRequest<Token>(message);

            return retval;
        }

    }
}
