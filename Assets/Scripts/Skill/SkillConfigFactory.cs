/****************************************************
    文件：SkillConfigFactory.cs
    作者：Olivia
    日期：2022/2/9 21:19:50
    功能：Nothing
*****************************************************/
using System;

namespace ARPG.Skill 
{
    public class SkillConfigFactory
    {
        public static IAttackSelector CrteateAttackSelector(SkillData rData)
        {
            string typeName = $"ARPG.Skill.{rData.SelectorType}AttackSelector";
            IAttackSelector selector = CreateObject(typeName) as IAttackSelector;
            return selector;
        }

        public static ISkillImpact[] CreateAttackImpact(SkillData rData)
        {
            ISkillImpact[] impacts = new ISkillImpact[]{};
            int index = 0;
            foreach (var impact in rData.ImpactType)
            {
                string typeName = $"ARPG.Skill.{impact}AttackImpact";
                impacts[index] = CreateObject(typeName) as ISkillImpact;
            }
            return impacts;
        }

        static object CreateObject(string rTypeName)
        {
            Type type = Type.GetType(rTypeName);
            object instanceObj = Activator.CreateInstance(type);
            return instanceObj;
        }
    }
}
