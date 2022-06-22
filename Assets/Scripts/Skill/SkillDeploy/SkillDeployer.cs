/****************************************************
    文件：SkillDeployer.cs
    作者：Olivia
    日期：2022/2/9 21:11:2
    功能：1.创建算法对象
        2.执行算法对象
        3.释放方式
*****************************************************/

using UnityEngine;

namespace ARPG.Skill
{
    /// <summary>
    /// 技能释放器
    /// </summary>
    public abstract class SkillDeployer : MonoBehaviour
    {
        IAttackSelector mAttackSelector;
        ISkillImpact[] mAttackImpacts;
        public Transform[] mAttackTargets;

        SkillData mSkillData;
        public SkillData SkillData
        {
            get
            {
                return mSkillData;
            }
            set
            {
                mSkillData = value;
                InitDeployer();
            }
        }

        /// <summary>
        /// 构建技能释放器相关数据
        /// </summary>
        public void InitDeployer()
        {
            //选区数据(类)
            mAttackSelector = SkillConfigFactory.CrteateAttackSelector(mSkillData);
            //效果数据（类）
            mAttackImpacts = SkillConfigFactory.CreateAttackImpact(mSkillData);
            //根据数据
        }

        //选区对象
        public void CalculateTargets()
        {
            mAttackTargets = mAttackSelector.SelectTargets(mSkillData, this.gameObject.transform);
        }
        //技能的影响效果
        public void ImpactTargets()
        {
            foreach (var impact in mAttackImpacts)
            {
                impact.Execute(this);
            }
        }
        
        //释放方式  供技能管理器调用，由子类实现，定义具体释放测量
        public abstract void ExecuteSkill();
        
    }

}

