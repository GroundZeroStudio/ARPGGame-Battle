using System.Collections;
using UnityEngine;

namespace Knight.Core
{
    public class WaitForSecondsWithTimeScale
    {
        public static IEnumerator Wait(float fWaitForSecond, bool bIsIgnoreTimeScale)
        {
            if (bIsIgnoreTimeScale)
            {
                yield return new WaitForSecondsRealtime(fWaitForSecond);
            }
            else
            {
                yield return new WaitForSeconds(fWaitForSecond);
            }
        }
    }
}
