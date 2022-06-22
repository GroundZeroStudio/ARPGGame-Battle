/****************************************************
    文件：MeleeSkillDeployer.cs
    作者：Olivia
    日期：2022/2/10 0:14:21
    功能：Nothing
*****************************************************/

using UnityEngine;

namespace ARPG.Skill
{
    /// <summary>
    /// 近身技能释放器
    /// </summary>
    public class MeleeSkillDeployer : SkillDeployer
    {
        public override void ExecuteSkill()
        {
            print("释放技能");
            this.CalculateTargets();
            this.ImpactTargets();
        }
    }
}

