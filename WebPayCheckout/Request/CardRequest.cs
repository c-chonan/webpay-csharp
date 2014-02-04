using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPayCheckout.Request
{

    /// <summary>
    /// 新たに作成するカード情報
    /// </summary>
    public class CardRequest
    {

        /// <summary>
        /// ハイフン"-"なしで数字のみのカード番号。
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// カードの持ち主の名前（名義）
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// カード有効期限の月を示す二桁の数字
        /// </summary>
        public int ExpMonth { get; set; }

        /// <summary>
        /// カード有効期限の年を示す四桁の数字
        /// </summary>
        public int ExpYear { get; set; }

        /// <summary>
        /// カードのセキュリティーコード
        /// </summary>
        public int CVC { get; set; }

        /// <summary>
        /// このインスタンスに持っている情報をWebのフォームなどで送信するための
        /// Dictionaryに詰め込みます。
        /// </summary>
        /// <param name="dictionary">情報を詰め込むDictionary</param>
        /// <returns>情報の詰まったDictionary</returns>
        internal Dictionary<string, string> ToFormContent(Dictionary<string, string> dictionary)
        {
            dictionary.Add("card[number]", this.Number);
            dictionary.Add("card[name]", this.Name);
            dictionary.Add("card[exp_month]", this.ExpMonth.ToString());
            dictionary.Add("card[exp_year]", this.ExpYear.ToString());
            dictionary.Add("card[cvc]", this.CVC.ToString());

            return dictionary;
        }

    }
}
