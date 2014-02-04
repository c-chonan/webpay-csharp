using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WebPayCheckout.Response
{

    /// <summary>
    /// 課金リスト
    /// </summary>
    [DataContract]
    public class ChargeList
    {
        /// <summary>
        /// "list"と入る
        /// </summary>
        [DataMember(Name = "object")]
        public string ObjectType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember(Name = "url")]
        public string Url { get; set; }

        /// <summary>
        /// 対象の条件で取得される全体の件数。
        /// 実際に取得した件数とは違うので、Chargesの中に入っている件数とは一致しません。
        /// </summary>
        [DataMember(Name = "count")]
        public int Count { get; set; }

        /// <summary>
        /// 課金情報
        /// </summary>
        [DataMember(Name = "data")]
        public List<Charge> Charges { get; set; }
    }
}
