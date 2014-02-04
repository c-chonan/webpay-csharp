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
    /// 認証でエラーが起こった際の例外
    /// </summary>
    public class AuthenticationException : WebPayException
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="err">エラー情報</param>
        /// <param name="status">ステータスコード</param>
        internal AuthenticationException(Error err, HttpStatusCode status)
            : base(err, status)
        {

        }

    }
}
