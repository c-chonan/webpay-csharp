using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebPayCheckout.Response;

namespace WebPayCheckout
{
    /// <summary>
    /// カード情報に関するエラーの際の例外
    /// </summary>
    public class CardException : WebPayException
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="err">エラー情報</param>
        /// <param name="status">ステータスコード</param>
        internal CardException(Error err, HttpStatusCode status)
            : base(err, status)
        {

        }

    }
}
