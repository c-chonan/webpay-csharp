using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPayCheckout.Request
{

    /// <summary>
    /// 必要なパラメータが設定されていない場合の例外
    /// </summary>
    class RequiredParamNotSetException :
        ApplicationException
    {
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="message"></param>
        public RequiredParamNotSetException(string message)
            : base(message)
        {

        }
    }
}
