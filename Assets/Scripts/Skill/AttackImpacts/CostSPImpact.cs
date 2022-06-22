/****************************************************
    文件：CostSPImpact.cs
    作者：Olivia
    日期：2022/2/10 21:21:40
    功能：Nothing
*****************************************************/
using System;

namespace ARPG.Skill
{
    /// <summary>
    /// 减少玩家法力值
    /// </summary>
    public class CostSPImpact : ISkillImpact
    {
        public void Execute(SkillDeployer rSkillDeployer)
        {
            var state = rSkillDeployer.SkillData.Owner.GetComponent<CharacterStatus>();
            state.SP -= rSkillDeployer.SkillData.CostSP;
        }
    }
}

