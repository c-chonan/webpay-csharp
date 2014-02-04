using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPayCheckout.Request
{
    internal interface IRequest
    {

        Dictionary<string, string> ToFormContent(Dictionary<string, string> dictionary);

    }
}
