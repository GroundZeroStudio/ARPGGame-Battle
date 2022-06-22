/****************************************************
    文件：CharacterSelector.cs
    作者：Olivia
    日期：2022/5/25 20:41:23
    功能：Nothing
*****************************************************/

using System;
using UnityEngine;

namespace ARPG.Skill
{
    /// <summary>
    /// 如果选中目标，需要重新触发冷却时间
    /// </summary>
    public class CharacterSelector : MonoBehaviour
    {
        private Transform mSelectObj;
        public string SelectName = "Selected";
        public int IntervalTime = 3;

        private void Start()
        {
            mSelectObj = transform.Find(SelectName);
        }

        public void SetSelectState(bool bState)
        {
            this.gameObject.SetActive(bState);
            if (bState)
            {
                mVisibleTime = Time.deltaTime + IntervalTime;
            }
            else
            {
                this.enabled = false;
            }
        }

        private float mVisibleTime = 0;
        private void Update()
        {
            if (Time.deltaTime > mVisibleTime)
            {
                SetSelectState(false);
            }
        }
    }
}

