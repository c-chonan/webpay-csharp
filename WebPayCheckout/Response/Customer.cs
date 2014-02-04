using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using WebPayCheckout.Request;

namespace WebPayCheckout.Response
{


    /// <summary>
    /// 顧客("customer")オブジェクトは、同一の顧客に対する複数回の課金を可能にします。
    /// このAPIによって、顧客の作成、削除、更新ができるようになります。
    /// その他にも、特定の顧客の情報を取得したり、顧客一覧のリストを取得したりすることができます。
    /// </summary>
    [DataContract]
    public class Customer
    {
        [DataMember(Name = "id")]
        public string ID { get; set; }

        [DataMember(Name = "object")]
        public string ObjectType { get; set; }

        [DataMember(Name = "livemode")]
        public bool LiveMode { get; set; }

        [DataMember(Name = "created")]
        public int Created { get; set; }

        [DataMember(Name = "email")]
        public string Email { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "active_card")]
        public Card ActiveCard { get; set; }

        /// <summary>
        /// すでに削除されていたらtrueになります。
        /// </summary>
        [DataMember(Name = "deleted")]
        public bool Deleted { get; set; }

    }


}
