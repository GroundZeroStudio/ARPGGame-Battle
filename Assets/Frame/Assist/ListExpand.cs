using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knight.Core
{
    public static class ListExpand
    {
        public static string Concat<T>(this IList<T> rList, string rSplitChar = "")
        {
            if (rList == null)
            {
                return "";
            }
            var rStringBuilder = new StringBuilder();
            for (int i = 0; i < rList.Count; i++)
            {
                rStringBuilder.A(rList[i].ToString());
                if (i + 1 != rList.Count)
                {
                    rStringBuilder.A(rSplitChar);
                }
            }
            return rStringBuilder.ToString();
        }
    }
}
