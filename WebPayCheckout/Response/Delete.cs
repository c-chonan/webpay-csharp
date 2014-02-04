using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WebPayCheckout.Response
{
    
    [DataContract]
    public class Delete
    {

        [DataMember(Name = "id")]
        public string ID { get; set; }

        [DataMember(Name = "deleted")]
        public bool Deleted { get; set; }

    }
}
