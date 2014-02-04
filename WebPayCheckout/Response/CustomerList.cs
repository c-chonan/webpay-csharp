using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WebPayCheckout.Response
{

    /// <summary>
    /// 顧客のリスト
    /// </summary>
    [DataContract]
    public class CustomerList
    {

        [DataMember(Name = "object")]
        public string ObjectType { get; set; }

        [DataMember(Name = "url")]
        public string Url { get; set; }

        [DataMember(Name = "count")]
        public int Count { get; set; }

        [DataMember(Name = "data")]
        public List<Customer> Customers { get; set; }
    }



}
