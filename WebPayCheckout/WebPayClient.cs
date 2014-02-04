using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WebPayCheckout.Clients;
using WebPayCheckout.Request;
using WebPayCheckout.Response;

namespace WebPayCheckout
{
    /// <summary>
    /// WebPayに対しての操作が集約されたクラスです。
    /// </summary>
    public class WebPayClient
    {
        /// <summary>
        /// APIキー
        /// </summary>
        private readonly string ApiKey;

        /// <summary>
        /// APIのベースURI
        /// </summary>
        internal readonly Uri BaseUri;

        /// <summary>
        /// 顧客関連の操作
        /// </summary>
        public readonly CustomerClient Customer;

        /// <summary>
        /// トークンの操作
        /// </summary>
        public readonly TokenClient Token;

        /// <summary>
        /// 課金の操作
        /// </summary>
        public readonly ChargeClient Charge;

        /// <summary>
        /// アカウントの操作
        /// </summary>
        public readonly AccountClient Account;

        /// <summary>
        /// HTTPの通信の際に使用するHttpClientHandler
        /// </summary>
        public readonly HttpClientHandler Handler;

        public WebPayClient(string apikey)
        {
            this.ApiKey = apikey;
            this.BaseUri = new Uri("https://api.webpay.jp/v1/");
            this.Handler = new HttpClientHandler();

            this.Customer = new CustomerClient(this);
            this.Token = new TokenClient(this);
            this.Charge = new ChargeClient(this);
            this.Account = new AccountClient(this);
        }


        internal T SendRequest<T>(HttpRequestMessage message) where T : class
        {

            HttpResponseMessage response = null;
            {
                HttpClient client = new HttpClient(this.Handler);

                //認証の設定
                message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", this.ApiKey);

                //リクエストを投げてレスポンスを受ける
                var task = client.SendAsync(message, HttpCompletionOption.ResponseContentRead);
                task.Wait();
                response = task.Result;
            }

            string content = null;
            {
                //コンテントをとってくる
                var task = response.Content.ReadAsStringAsync();
                task.Wait();
                content = task.Result;
            }

            T retval = null;
            if ((int)response.StatusCode >= 200 && (int)response.StatusCode < 300)
            {
                //200で返ってきたら指定された型でデシリアライズ
                retval = this.Deserialise<T>(content);
            }
            else
            {
                //エラーで返ってきたらJSONがちょっと違うのよ
                Error err = this.Deserialise<Error>(content);

                WebPayException exp = null;
                switch (response.StatusCode)
                {
                    case HttpStatusCode.BadRequest:
                    case HttpStatusCode.NotFound:
                        exp = new InvalidRequestException(err, response.StatusCode);
                        break;
                    case HttpStatusCode.Unauthorized:
                        exp = new AuthenticationException(err, response.StatusCode);
                        break;
                    case HttpStatusCode.PaymentRequired:
                        exp = new CardException(err, response.StatusCode);
                        break;
                    default:
                        exp = new WebPayException(err, response.StatusCode);
                        break;
                }

                throw exp;
            }

            return retval;
        }

        /// <summary>
        /// JSON文字列を指定された方にデシリアライズします。
        /// </summary>
        /// <typeparam name="T">デシリアライスする際の型</typeparam>
        /// <param name="content">JSON文字列</param>
        /// <returns>デシリアライズしたインスタンス</returns>
        private T Deserialise<T>(string content) where T : class
        {
            T retval = null;

            using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(content)))
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
                retval = (T)ser.ReadObject(stream);
            }

            return retval;
        }

    }
}
