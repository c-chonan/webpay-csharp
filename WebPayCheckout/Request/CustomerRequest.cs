using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPayCheckout.Request
{
    /// <summary>
    /// 新たに作成する顧客情報
    /// </summary>
    public class CustomerRequest : IRequest
    {
        /// <summary>
        /// カード情報
        /// </summary>
        public CardRequest Card { get; set; }

        /// <summary>
        /// カードのトークンID
        /// </summary>
        public string CardToken { get; set; }

        /// <summary>
        /// 顧客のメールアドレス。
        /// この情報はウェブ上のダッシュボードに表示され、また履歴の検索やトラッキング等に使用することができます。
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 顧客オブジェクトに添付することができる任意の文字列。
        /// この情報はウェブ上のダッシュボードで顧客情報を確認する際に表示されます。
        /// </summary>
        public string Description { get; set; }

        public Dictionary<string, string> ToFormContent(Dictionary<string, string> dictionary)
        {
            if (Card != null)
            {
                this.Card.ToFormContent(dictionary);
            }
            else if (CardToken != null)
            {
                dictionary.Add("card", CardToken);
            }

            if (!string.IsNullOrEmpty(Email))
            {
                dictionary.Add("email", Email);
            }

            if (!string.IsNullOrEmpty(Description))
            {
                dictionary.Add("description", Description);
            }

            return dictionary;
        }
    }
}
