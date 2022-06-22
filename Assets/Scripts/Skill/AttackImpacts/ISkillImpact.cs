/****************************************************
    文件：IAttackImpact.cs
    作者：Olivia
    日期：2022/2/9 21:18:13
    功能：Nothing
*****************************************************/
using System;

namespace ARPG.Skill
{
    public interface ISkillImpact
    {

        /// <summary>
        /// 技能效果-效果释放器
        /// </summary>
        public void Execute(SkillDeployer rSkillDeployer);
    }
}
