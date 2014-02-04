using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WebPayCheckout.Response
{
    /// <summary>
    /// 課金情報
    /// </summary>
    [DataContract]
    public class Charge
    {
        /// <summary>
        /// 取得する課金オブジェクトを判別する識別用のユニークID。
        /// </summary>
        [DataMember(Name = "id")]
        public string ID { get; set; }

        /// <summary>
        /// "Charge"と入ってくるはずです。
        /// </summary>
        [DataMember(Name = "object")]
        public string ObjectType { get; set; }

        [DataMember(Name = "livemode")]
        public bool LiveMode { get; set; }

        /// <summary>
        /// 三文字のISOコードで表される通貨の名称
        /// 現在は円("jpy")のみ対応
        /// </summary>
        [DataMember(Name = "currency")]
        public string Currency { get; set; }

        /// <summary>
        /// 課金オブジェクトに添付することのできる任意の文字列。
        /// この文字列は、ウェブ上で課金を管理する際に、課金オブジェクトとともに表示されます。
        /// 後でトラッキングするために、ユーザーのIDやemailアドレス等を記載しておくと良いかもしれません。
        /// </summary>
        [DataMember(Name = "description")]
        public string Description { get; set; }

        /// <summary>
        /// 1円単位で正の整数で表現される課金額。
        /// </summary>
        [DataMember(Name = "amount")]
        public int Amount { get; set; }

        /// <summary>
        /// 円単位で払い戻された金額
        /// （一部払い戻された場合は課金額よりも小さい値となります。）
        /// </summary>
        [DataMember(Name = "amount_refunded")]
        public int AmountRefunded { get; set; }

        /// <summary>
        /// もし存在している場合は、この課金に紐付いている顧客のID
        /// </summary>
        [DataMember(Name = "customer")]
        public string Customer { get; set; }

        /// <summary>
        /// unixタイムで表示される作成日時
        /// </summary>
        [DataMember(Name = "created")]
        public int Created { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember(Name = "paid")]
        public bool Paid { get; set; }

        /// <summary>
        /// 課金が払い戻されたかどうか。
        /// もし一部のみ払い戻されている場合は、この属性はfalseのままです。
        /// 全額払い戻された場合はtrueになります。
        /// 仮売上が実売上化されずに期限切れになった場合、全額払い戻されたことになり、この属性はtrueになります。
        /// </summary>
        [DataMember(Name = "refunded")]
        public bool Refunded { get; set; }

        /// <summary>
        /// より詳しい課金の決済失敗に関するメッセージ。
        /// </summary>
        [DataMember(Name = "failure_message")]
        public string FailureMessage { get; set; }

        /// <summary>
        /// 仮売上として作成された場合、この属性はfalseとなります。
        /// 仮売上が実売上化されるとtrueになります。
        /// 即時売上の場合は常にtrueです。
        /// </summary>
        [DataMember(Name = "captured")]
        public bool Captured { get; set; }

        /// <summary>
        /// unixタイムで表示される仮売上げが自動的に失効される日時。
        /// 即時売上の場合は常にnullとなり、仮売上の場合に一定の日数後の日時が設定されます。
        /// （日数は設定画面から変更可能です）
        /// </summary>
        [DataMember(Name = "expire_time")]
        public int? ExpireTime { get; set; }

        /// <summary>
        /// 課金されたクレジットカードの情報
        /// </summary>
        [DataMember(Name = "card")]
        public Card Card { get; set; }


        public override string ToString()
        {
            return this.ID + " " + this.Amount + " " + this.AmountRefunded;
        }
    }



}
