using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPayCheckout.Request
{
    /// <summary>
    /// 新たに作成する課金情報
    /// </summary>
    public class ChargeRequest : IRequest
    {

        /// <summary>
        /// 1円単位で正の整数で表現される課金額。
        /// 推奨する最小課金額は50円で、これより低い金額を指定すると手数料のほうが高くなるので推奨されません。
        /// </summary>
        public long Amount { get; set; }

        /// <summary>
        /// この課金で請求を行う既存の顧客のID。
        /// </summary>
        public string Customer { get; set; }

        /// <summary>
        /// カードに対するトークンのID
        /// </summary>
        public string CardToken { get; set; }

        /// <summary>
        /// この課金で請求を行うクレジットカードの情報。
        /// このカード情報は、tokenもしくはカード情報を含んだハッシュ（ディクショナリ）のどちらでもかまいません。
        /// 下のオプションの全ての情報が必要というわけではありませんが、クレジットカード詐欺を防ぐためには、できるだけ多くの情報を照会することが有効な対策となります。
        /// </summary>
        public CardRequest Card { get; set; }

        /// <summary>
        /// 3文字のISOコードで規定されている通貨。
        /// 現在は、"jpy"(日本円)のみ対応しています。
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// 課金オブジェクトに添付することのできる任意の文字列。
        /// この文字列は、ウェブ上で課金を管理する際に、課金オブジェクトとともに表示されます。
        /// 後でトラッキングするために、ユーザーのIDやemailアドレス等を記載しておくと良いかもしれません。
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// すぐに実売上にするか、仮売上として後で実売上化するかを指定します。
        /// falseの場合に与信のみが行われ、後で「仮売上の実売上化」をすることで実売上化できます。
        /// 標準で7日経つと仮売上は失効します。（設定画面から日数は変更可能です）
        /// </summary>
        public bool Capture { get; set; }

        /// <summary>
        /// 同じUUIDを持つリクエストが複数回送信されたとき、24時間の間に高々一度だけ処理がおこなわれることを保証します。
        /// 以前の同じUUIDを持つリクエストで作成済みの課金がある場合は、それを通常の作成時と同じように返却します。
        /// </summary>
        public Guid Uuid { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ChargeRequest()
        {
            this.Capture = true;
            this.Currency = "jpy";
            this.Uuid = Guid.Empty;
        }

        public Dictionary<string, string> ToFormContent(Dictionary<string, string> dictionary)
        {

            if (Amount <= 0)
            {
                throw new RequiredParamNotSetException("amount");
            }
            else
            {
                dictionary.Add("amount", Amount.ToString());
            }

            if (Currency == null)
            {
                throw new RequiredParamNotSetException("currency");
            }
            else
            {
                dictionary.Add("currency", Currency);
            }

            if (Customer != null)
            {
                dictionary.Add("customer", Customer);
            }
            else if (CardToken != null)
            {
                dictionary.Add("card", CardToken);
            }
            else if (Card != null)
            {
                this.Card.ToFormContent(dictionary);
            }
            else
            {
                throw new RequiredParamNotSetException("card");
            }

            if (Description != null)
            {
                dictionary.Add("description", Description);
            }

            dictionary.Add("capture", Capture.ToString().ToLower());

            if (Uuid != Guid.Empty)
            {
                dictionary.Add("uuid", Uuid.ToString());
            }

            return dictionary;
        }
    }


}
