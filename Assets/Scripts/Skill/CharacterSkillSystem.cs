/****************************************************
    文件：CharacterSkillSystem.cs
    作者：Olivia
    日期：2022/5/24 7:50:22
    功能：Nothing
*****************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using Knight.Core;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ARPG.Skill
{
    [RequireComponent(typeof(CharacterSkillManager))]
    public class CharacterSkillSystem : MonoBehaviour
    {

        private CharacterSkillManager mSkillManager;
        private Animator mAnimator;
        private void Start()
        {
            mSkillManager = GetComponent<CharacterSkillManager>();
            mAnimator = GetComponent<Animator>();
            //GetComponentInChildren<AnimationEventBehaviour>()
        }

        public void OnMeleeAttack()
        {
            mSkillManager.GenerateSkill(mSkillData);
        }

        private SkillData mSkillData;
        public void UesAttackSkill(int nSkillId)
        {
            //TODO 准备技能
            mSkillData = mSkillManager.PrepareSkill(nSkillId);
            //TODO 播放动画
            mAnimator.SetBool(mSkillData.AnimationName, true);
            //TODO 生成技能
            //TODO 如果单攻
            if (mSkillData.AttackType == SkillAttackType.Single)
            {
                ResetSelectTarget(false);
                //获得目标数据
                mCurrTarget = SelectTarget();
                ResetSelectTarget(true);
            }

        }

        private Transform mCurrTarget;
        void ResetSelectTarget(bool bState)
        {
            if(mCurrTarget == null) return;
            var rSelect = mCurrTarget.transform.GetComponentInChildren<CharacterSelector>();
            rSelect.SetSelectState(bState);
        }

        Transform SelectTarget()
        {
            var rAttakcTarget = new SectorAttackSelector().SelectTargets(mSkillData, transform);
            return rAttakcTarget.Length > 0 ? rAttakcTarget[0] : null;
        }
        

        /// <summary>
        /// 随机释放一个技能
        /// </summary>
        public void UseRandomAttackSkill()
        {
            //通过技能管理器找到可以使用的技能列表
            //随机获得一个技能
            //释放技能
            List<SkillData> rList = mSkillManager.Skills.ToList().FindAll(s => mSkillManager.PrepareSkill(s.SkillID) != null);
            if(rList.Count == 0) return;
            int nIndex = Random.Range(0, rList.Count);
            UesAttackSkill(rList[nIndex].SkillID);
        }
}
}

