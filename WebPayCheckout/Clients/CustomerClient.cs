using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebPayCheckout.Request;
using WebPayCheckout.Response;

namespace WebPayCheckout.Clients
{
    /// <summary>
    /// 顧客に対するクライアント
    /// </summary>
    public class CustomerClient
    {
   
        private readonly WebPayClient parent;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="parent">親になるWebPayClient</param>
        internal CustomerClient(WebPayClient parent)
        {
            this.parent = parent;
        }

        /// <summary>
        /// 新たな顧客を作成します。
        /// </summary>
        /// <param name="customer">新たな顧客情報</param>
        /// <returns>顧客情報</returns>
        public Customer Create(CustomerRequest customer)
        {
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, new Uri(parent.BaseUri, "customers"));
            message.Content = new FormUrlEncodedContent(customer.ToFormContent(new Dictionary<string, string>()));

            Customer retval = this.parent.SendRequest<Customer>(message);

            return retval;
        }

        /// <summary>
        /// 既存の顧客の詳細情報を取得します。
        /// </summary>
        /// <param name="id">顧客の識別ID</param>
        /// <returns>顧客情報</returns>
        public Customer Retrieve(string id)
        {
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, new Uri(parent.BaseUri, "customers/" + id));

            Customer retval = this.parent.SendRequest<Customer>(message);

            return retval;
        }

        /// <summary>
        /// 指定した顧客の情報を必要な部分のみ更新します。
        /// </summary>
        /// <param name="id">顧客の識別ID</param>
        /// <param name="customer">更新する顧客情報</param>
        /// <returns>顧客情報</returns>
        public Customer Update(string id, CustomerRequest customer)
        {
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, new Uri(parent.BaseUri, "customers/" + id));
            message.Content = new FormUrlEncodedContent(customer.ToFormContent(new Dictionary<string, string>()));

            Customer retval = this.parent.SendRequest<Customer>(message);

            return retval;
        }

        /// <summary>
        /// 顧客を永久に削除します。
        /// この操作は巻き戻すことはできませんので、慎重に行ってください。
        /// </summary>
        /// <param name="id">顧客の識別ID</param>
        /// <returns>顧客情報</returns>
        public bool Delete(string id)
        {
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Delete, new Uri(parent.BaseUri, "customers/" + id));

            Delete retval = this.parent.SendRequest<Delete>(message);

            return retval.Deleted;
        }

        /// <summary>
        /// 顧客のリストを取得します。
        /// </summary>
        /// <returns>顧客のリスト</returns>
        public CustomerList All()
        {
            return this.All(new ListRequest());
        }

        /// <summary>
        /// 顧客のリストを取得します。
        /// </summary>
        /// <param name="list">リストを取得する際の条件</param>
        /// <returns>顧客のリスト</returns>
        public CustomerList All(ListRequest list)
        {
            Dictionary<string, string> dic = list.ToFormContent(new Dictionary<string, string>());
            string query = HelperImpl.ToQueryString(dic);
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, new Uri(parent.BaseUri, "customers?" + query));

            CustomerList retval = this.parent.SendRequest<CustomerList>(message);

            return retval;
        }
    }
}
