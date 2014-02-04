using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WebPayCheckout.Response
{
    /// <summary>
    /// アカウント情報
    /// </summary>
    [DataContract]
    public class Account
    {
        /// <summary>
        /// "account"の文字列が入ります
        /// </summary>
        [DataMember(Name = "object")]
        public string ObjectType { get; set; }

        /// <summary>
        /// アカウントの一意な識別子。
        /// </summary>
        [DataMember(Name = "id")]
        public string ID { get; set; }

        /// <summary>
        /// アカウントの登録済みメールアドレス
        /// </summary>
        [DataMember(Name = "email")]
        public string Email { get; set; }

        /// <summary>
        /// クレジットカードの明細書に記載される文字列。
        /// カードの持ち主の契約するカード会社によって表記がカタカナになる場合があります。
        /// </summary>
        [DataMember(Name = "statement_descriptor")]
        public string StatementDescripter { get; set; }

        /// <summary>
        /// アカウントの審査用の詳細情報が既に入力済みかどうか。
        /// </summary>
        [DataMember(Name = "details_submitted")]
        public bool DetailsSubmitted { get; set; }

        /// <summary>
        /// このアカウントが本番環境での課金を行うことができるかどうか。
        /// </summary>
        [DataMember(Name = "charge_enabled")]
        public bool ChargeEnabled { get; set; }

        /// <summary>
        /// 課金を作成する際にこのアカウントが利用することができる通貨の一覧。
        /// </summary>
        [DataMember(Name = "currencies_supported")]
        public List<string> CurrenciesSupported { get; set; }

    }
}
