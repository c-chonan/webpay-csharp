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
    /// リクエストの内容にエラーが有った際の例外
    /// </summary>
    public class InvalidRequestException : WebPayException
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="err">エラー情報</param>
        /// <param name="status">ステータスコード</param>
        internal InvalidRequestException(Error err, HttpStatusCode status)
            : base(err, status)
        {

        }

    }
}
