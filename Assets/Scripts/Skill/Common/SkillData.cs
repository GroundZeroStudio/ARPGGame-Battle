using System;
using UnityEngine;

namespace ARPG.Skill
{
    /// <summary>
    /// 技能数据
    /// </summary>
    [Serializable]
	public class SkillData
	{
        /// <summary>技能编号</summary>
        public int SkillID;
        /// <summary>技能拥有者</summary>
        [HideInInspector]
        public GameObject Owner;
        /// <summary>技能等级</summary>
        public int Level;
        /// <summary>技能名称</summary>
        public string Name;
        /// <summary>描述</summary>
        public string Description;
        /// <summary>技能图标</summary>
        public string SkillIcon;
        /// <summary>冷却时间</summary>
        public int CoolTime;
        /// <summary>冷却剩余</summary>
        public int CoolRemain;
        /// <summary>魔法消耗</summary>
        public int CostSP;
        /// <summary>攻击距离</summary>
        public float AttackDisntance;
        /// <summary> 攻击范围角度</summary>
        public int AttackAngle;
        /// <summary>攻击目标的TAG</summary>
        public string[] AttckTargetTags = { "Enemy" };
        /// <summary>攻击目标</summary>
        [HideInInspector]
        public Transform[] AttackTargets;
        /// <summary> 技能影响类型 </summary>
        public string[] ImpactType = { "CostSP", "Damage" };
        /// <summary>下一个连击技能编号</summary>
        public int NextBatterId;
        /// <summary> 伤害比率 </summary>
        public float AtkRatio;
        /// <summary>持续时间</summary>
        public float DurationTime;
        /// <summary>伤害间隔</summary>
        public float AtkInterval;
        public string PrefabName;
        /// <summary>技能预制对象</summary>
        [HideInInspector]
        public GameObject SkillPrefab;
        /// <summary>技能对应的动画名称 </summary>
        public string AnimationName;
        /// <summary>目标受击特效</summary>
        public string HitFxName;
        /// <summary>受击特效预制对象</summary>
        [HideInInspector]
        public GameObject HitFxPrefab;
        /// <summary>攻击范围 线形，矩形，扇形，圆形</summary>
        public SelectorType SelectorType;
        /// <summary>攻击类型，单攻，群攻</summary>
        public SkillAttackType AttackType;
    }
}
