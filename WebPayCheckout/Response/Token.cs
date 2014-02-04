using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WebPayCheckout.Response
{

    /// <summary>
    /// トークンとは、クレジットカードの情報と紐付く識別ID(例えば"tok_0123456789abcd")です。
    /// あなたはこのトークンをクレジットカード情報と同じように扱うことができ、このトークンを用いてトークンと紐付くクレジットカード保持者に課金を行うことができます。
    /// </summary>
    [DataContract]
    public class Token
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember(Name = "id")]
        public string ID { get; set; }

        /// <summary>
        /// "token"が入ります
        /// </summary>
        [DataMember(Name = "object")]
        public string ObjectType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember(Name = "livemode")]
        public bool LiveMode { get; set; }

        /// <summary>
        /// unixタイム
        /// </summary>
        [DataMember(Name = "created")]
        public int Created { get; set; }

        /// <summary>
        /// このトークンが既に使用済みかどうか。
        /// (トークンは一度のみ使用可能です。)
        /// </summary>
        [DataMember(Name = "used")]
        public bool Used { get; set; }

        /// <summary>
        /// 課金されたクレジットカードの情報
        /// </summary>
        [DataMember(Name = "card")]
        public Card Cards { get; set; }
    }


}
