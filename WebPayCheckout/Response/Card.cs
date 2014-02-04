using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using WebPayCheckout.Request;

namespace WebPayCheckout.Response
{
    [DataContract]
    public class Card
    {
        /// <summary>
        /// 値は"card"です。
        /// </summary>
        [DataMember(Name = "object")]
        public string ObjectType { get; set; }

        /// <summary>
        /// カード有効期限の月を示す二桁の数字
        /// </summary>
        [DataMember(Name = "exp_year")]
        public int ExpYear { get; set; }

        /// <summary>
        /// カード有効期限の年を示す四桁の数字
        /// </summary>
        [DataMember(Name = "exp_month")]
        public int ExpMonth { get; set; }

        /// <summary>
        /// このクレジットカード番号に紐づけられた一意（他と重複しない）キー。
        /// あなたは例えばこの値を、2人の別々のユーザーが同じカードを登録することを発見・禁止するために使用することができます。
        /// </summary>
        [DataMember(Name = "fingerprint")]
        public string Fingerprint { get; set; }

        /// <summary>
        /// クレジットカード所有者の名義
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }


        [DataMember(Name = "country")]
        public string Country { get; set; }

        /// <summary>
        /// カードのブランド。
        /// Visa, MasterCard, American Express, Discover, JCB, Diners Club もしくは"unknown"(不明)のどれか。
        /// </summary>
        [DataMember(Name = "type")]
        public string Type { get; set; }

        /// <summary>
        /// もしcvcが入力されている場合、その情報の真偽を確かめたうえで"pass"(正当)、"fail"（不正）、"unchecked"(不明)のいずれかの値が結果となる。
        /// </summary>
        [DataMember(Name = "cvc_check")]
        public string CvcCheck { get; set; }

        /// <summary>
        /// カード番号の最後4桁
        /// </summary>
        [DataMember(Name = "last4")]
        public string Last4 { get; set; }

        public override string ToString()
        {
            return "\"" + this.Name + "\" " + this.Last4 + " " + this.ExpMonth + "/" + this.ExpYear;
        }

    }


}
