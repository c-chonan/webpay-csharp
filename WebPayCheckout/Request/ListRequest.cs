using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPayCheckout.Request
{
    /// <summary>
    /// リストを取得する際の条件を指定するクラス
    /// </summary>
    public class ListRequest : IRequest
    {
        /// <summary>
        /// 一度にリストで返す顧客オブジェクトの上限数。
        /// "count"は1から100の間の整数を指定することができます。
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 顧客オブジェクトのオフセット（始値）の指定。
        /// APIは、このオフセットで指定された番号を一番目として、顧客オブジェクトのリストを返します。
        /// 例えば offset=20 count=10 であれば、リストの20番目から順番に10個の顧客オブジェクトを返します。
        /// </summary>
        public int Offset { get; set; }

        /// <summary>
        /// このタイムスタンプよりも後に作成された項目のみに限定します
        /// </summary>
        public DateTimeOffset CreatedGt { get; set; }

        /// <summary>
        /// このタイムスタンプと同時かもしくはそれより後に作成された項目のみに限定します
        /// </summary>
        public DateTimeOffset CreatedGte { get; set; }

        /// <summary>
        /// このタイムスタンプよりも前に作成された項目のみに限定します
        /// </summary>
        public DateTimeOffset CreatedLt { get; set; }

        /// <summary>
        /// このタイムスタンプと同時かもしくはそれより前に作成された項目のみに限定します
        /// </summary>
        public DateTimeOffset CreatedLte { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ListRequest()
        {
            this.Count = 10;
            this.Offset = 0;

            this.CreatedGt = DateTimeOffset.MinValue;
            this.CreatedGte = DateTimeOffset.MinValue;
            this.CreatedLt = DateTimeOffset.MinValue;
            this.CreatedLte = DateTimeOffset.MinValue;
        }

        public Dictionary<string, string> ToFormContent(Dictionary<string, string> dictionary)
        {
            dictionary.Add("count", this.Count.ToString());

            dictionary.Add("offset", this.Offset.ToString());

            if (CreatedGt != DateTimeOffset.MinValue)
            {
                dictionary.Add("created[gt]", HelperImpl.ToUnixTime(CreatedGt));
            }

            if (CreatedGte != DateTimeOffset.MinValue)
            {
                dictionary.Add("created[gte]", HelperImpl.ToUnixTime(CreatedGte));
            }

            if (CreatedLt != DateTimeOffset.MinValue)
            {
                dictionary.Add("created[lt]", HelperImpl.ToUnixTime(CreatedLt));
            }

            if (CreatedLte != DateTimeOffset.MinValue)
            {
                dictionary.Add("created[lte]", HelperImpl.ToUnixTime(CreatedLte));
            }

            return dictionary;
        }
    }
}
