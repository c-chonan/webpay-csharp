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
    /// API操作に関してのエラーに対する例外
    /// </summary>
    public class WebPayException : ApplicationException
    {
        /// <summary>
        /// レスポンスのステータスコード
        /// </summary>
        public HttpStatusCode StatusCode { get; protected set; }

        /// <summary>
        /// エラーコード
        /// </summary>
        public string Code { get; set; }

        public string Param { get; set; }

        public string Type { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="err">エラー情報</param>
        /// <param name="status">ステータスコード</param>
        internal WebPayException(Error err, HttpStatusCode status)
            : base(err.Content.Message)
        {
            this.StatusCode = status;

            this.Code = err.Content.Code;
            this.Param = err.Content.Param;
            this.Type = err.Content.Type;
        }

    }
}
