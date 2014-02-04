using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WebPayCheckout
{
    internal static class HelperImpl
    {
        /// <summary>
        /// Unixエポックの日時
        /// </summary>
        private static readonly DateTimeOffset epoch = new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero);

        /// <summary>
        /// 与えられた時間をUnix時間に変換します。
        /// </summary>
        /// <param name="dt">変換する時間</param>
        /// <returns>Unix時間の文字列</returns>
        public static string ToUnixTime(DateTimeOffset dt)
        {
            TimeSpan span = dt - epoch;
            return Convert.ToInt64(span.TotalSeconds).ToString();
        }

        /// <summary>
        /// Dictionaryの中身をクエリストリング形式の文字列にします。
        /// </summary>
        /// <param name="dic">Dictionary</param>
        /// <returns>クエリストリング形式の文字列</returns>
        public static string ToQueryString(Dictionary<string, string> dic)
        {
            NameValueCollection query = HttpUtility.ParseQueryString(string.Empty);
            foreach (KeyValuePair<string, string> pair in dic)
            {
                query.Add(pair.Key, pair.Value);
            }

            string retval = query.ToString();
            return retval;
        }


    }
}
