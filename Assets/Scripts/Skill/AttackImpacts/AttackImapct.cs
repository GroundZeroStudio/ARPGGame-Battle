/****************************************************
    文件：AttaclImpact.cs
    作者：Olivia
    日期：2022/2/10 22:17:43
    功能：Nothing
*****************************************************/
using System;
using System.Collections;
using UnityEngine;

namespace ARPG.Skill
{
    /// <summary>
    /// 技能伤害效果，在持续时间内对攻击范围内的敌人造成血量减少
    /// </summary>
    public class AttackImapct : ISkillImpact
    {
        SkillDeployer mSkillDeployer;
        public void Execute(SkillDeployer rSkillDeployer)
        {
            mSkillDeployer = rSkillDeployer;
            mSkillDeployer.StartCoroutine(RepeatDamage());
        }

        /// <summary>
        /// 间隔时间造成伤害
        /// </summary>
        /// <returns></returns>
        IEnumerator RepeatDamage()
        {
            float attackTime = mSkillDeployer.SkillData.DurationTime;
            //目标对象
            do
            {
                //造成伤害
                OnDamage();
                yield return new WaitForSeconds(mSkillDeployer.SkillData.AtkInterval);
                attackTime -= mSkillDeployer.SkillData.AtkInterval;
                //更新一下可攻击的敌人数据
                mSkillDeployer.CalculateTargets();
            }
            while (attackTime > 0);
        }

        /// <summary>
        /// 敌人血量减少
        /// </summary>
        void OnDamage()
        {
            float atk = mSkillDeployer.SkillData.Owner.gameObject.GetComponent<CharacterStatus>().BaseATK * mSkillDeployer.SkillData.AtkRatio;
            //攻击力计算公式
            for (int i = 0; i < mSkillDeployer.mAttackTargets.Length; i++)
            {
                var rState = mSkillDeployer.mAttackTargets[i].GetComponent<CharacterStatus>();
                rState.Damage(atk);
            }
        }
    }
}
