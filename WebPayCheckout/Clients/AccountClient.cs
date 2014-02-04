using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using WebPayCheckout.Response;

namespace WebPayCheckout.Clients
{

    /// <summary>
    /// アカウント操作に対するクライアント
    /// </summary>
    public class AccountClient
    {
        private readonly WebPayClient parent;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="parent">親になるWebPayClient</param>
        internal AccountClient(WebPayClient parent)
        {
            this.parent = parent;
        }

        /// <summary>
        /// リクエストを行ったAPIキーの情報を基にして、APIキーの持ち主のアカウントの詳細情報を取得します。
        /// </summary>
        /// <returns>使用されたAPIキーに紐付くアカウントオブジェクト</returns>
        public Account Retrieve()
        {
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, new Uri(parent.BaseUri, "account"));

            Account retval = this.parent.SendRequest<Account>(message);

            return retval;
        }

        /// <summary>
        /// リクエストを行ったAPIキーをもつユーザの、テスト環境のデータを全て削除します。
        /// ユーザ設定画面から行える操作と同じです。
        /// リクエスト時は、テスト環境用非公開鍵を指定する必要があります。
        /// この操作を行った後、元に戻すことは出来ません。
        /// </summary>
        /// <returns>trueが返れば成功</returns>
        public bool DeleteData()
        {
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Delete, new Uri(parent.BaseUri, "account/data"));

            Delete retval = this.parent.SendRequest<Delete>(message);

            return retval.Deleted;
        }
    }
}
