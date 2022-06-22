/****************************************************
    文件：SectorAttackSelector.cs
    作者：Olivia
    日期：2022/2/9 23:39:33
    功能：Nothing
*****************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ARPG.Skill
{
    /// <summary>
    /// 扇形选区对象
    /// </summary>
    public class SectorAttackSelector : IAttackSelector
    {
        //返回技能攻击目标对象
        public Transform[] SelectTargets(SkillData rData, Transform rSkillTran)
        {
            List<Transform> targets = new List<Transform>();
            //TODO 可以用Lamdba表达式，还不会_(:з」∠)_
            //标签
            for (int i = 0; i < rData.AttckTargetTags.Length; i++)
            {
                var objects = GameObject.FindGameObjectsWithTag(rData.AttckTargetTags[i]);
                targets.AddRange(objects.Select(g => g.transform));
            }
            //距离和角度，判断是否在攻击范围内
            targets = targets.Where((t) => Vector3.Distance(t.position, rSkillTran.position) <= rData.AttackDisntance &&
            Vector3.Angle(rSkillTran.position, t.position) <= rData.AttackAngle).ToList();

            //敌人状态是否满足条件
            targets = targets.Where((t) => t.gameObject.GetComponent<CharacterStatus>().HP > 0).ToList();

            //判断攻击方式（群攻/单攻）
            if(rData.AttackType == SkillAttackType.Group)
            {
                return targets.ToArray();
            }
            else
            {
                Transform[] target = new Transform[] { };
                if (targets.Count == 0) return target;
                Transform min = targets[0];
                float minDistance = Vector3.Distance(min.position, rSkillTran.position);
                target[0] = targets.Find((t) => Vector3.Distance(t.position, rSkillTran.position) < minDistance);
                return target;
            }
        }
    }
}
