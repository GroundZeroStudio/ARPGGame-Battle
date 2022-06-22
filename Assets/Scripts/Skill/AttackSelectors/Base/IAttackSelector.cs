/****************************************************
    文件：IAttackSelector.cs
    作者：Olivia
    日期：2022/2/9 21:16:23
    功能：Nothing
*****************************************************/
using System;
using UnityEngine;

namespace ARPG.Skill 
{
    public interface IAttackSelector
    {
        /// <summary>
        /// 选区内的攻击对象
        /// </summary>
        /// <param name="rData"></param>
        /// <param name="rSkillTran">技能的位置</param>
        /// <returns></returns>
        public Transform[] SelectTargets(SkillData rData, Transform rSkillTran);
    }
}
