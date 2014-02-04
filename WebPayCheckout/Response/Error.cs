using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WebPayCheckout.Response
{
    [DataContract]
    public class Error
    {
        [DataMember(Name="error")]
        public ErrorContent Content { get; set; }

        [DataContract]
        public class ErrorContent
        {
            [DataMember(Name = "code")]
            public string Code { get; set; }

            [DataMember(Name = "message")]
            public string Message { get; set; }

            [DataMember(Name = "param")]
            public string Param { get; set; }

            [DataMember(Name = "type")]
            public string Type { get; set; }
        }

    }



}
