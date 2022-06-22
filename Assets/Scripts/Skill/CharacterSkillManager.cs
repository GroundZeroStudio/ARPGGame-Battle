/****************************************************
    文件：CharacterSkillManager.cs
    作者：Olivia
    日期：2022/2/5 1:24:13
    功能：Nothing
*****************************************************/

using ARPG.Skill;
using Knight.Core;
using System.Collections;
using UnityEngine;

public class CharacterSkillManager : MonoBehaviour
{
    /// <summary>
    /// 技能数据
    /// </summary>
    public SkillData[] Skills;
    private PlayerStatus mPlayerStatus;
    private SkillDeployer mSkillDeployer;
    private Dict<int, SkillData> mSkillDict = new Dict<int, SkillData>();

    private void Start()
    {
        this.mSkillDict.Clear();
        mPlayerStatus = GetComponent<PlayerStatus>();
        mSkillDeployer = GetComponent<SkillDeployer>();
        foreach (var skill in Skills)
        {
            InitSkill(skill);
        }
    }

    /// <summary>
    /// 初始化技能数据
    /// </summary>
    /// <param name="rSkillData"></param>
    private void InitSkill(SkillData rSkillData)
    {
        //动态加载技能特效预制体  //Resources/Skill -- 技能特效预制体 
        if (rSkillData.SkillPrefab == null && !string.IsNullOrEmpty(rSkillData.PrefabName))
            rSkillData.SkillPrefab = ResourceMgr.Instance.LoadFxPrefab("Skill/" + rSkillData.PrefabName);
        //Resources/Skill/HitFx     技能伤害特效预制体
        if (rSkillData.HitFxPrefab == null && !string.IsNullOrEmpty(rSkillData.HitFxName))
            rSkillData.HitFxPrefab = ResourceMgr.Instance.LoadFxPrefab("Skill/" + rSkillData.HitFxName);
        rSkillData.Owner = gameObject;
    }

    public SkillData PrepareSkill(int nSkillId)
    {
        //查找目标技能数据
        SkillData data = this.Skills.Find((s) => s.SkillID == nSkillId);
        //判断是否满足条件
        if (data != null && data.CoolRemain <= 0 && data.CostSP <= mPlayerStatus.SP)
            return data;
        else
            return null;
    }

    public void GenerateSkill(SkillData rSkillData)
    {
        //生成技能预制体
        GameObject skillObj = Instantiate(rSkillData.SkillPrefab, transform.position, transform.rotation);
        this.mSkillDeployer.SkillData = rSkillData;
        this.mSkillDeployer.ExecuteSkill();
        //销毁
        Destroy(skillObj, rSkillData.DurationTime);
        //技能冷却
        StartCoroutine(CoolTimeDown(rSkillData));
    }

    private IEnumerator CoolTimeDown(SkillData rSkillData)
    {
        rSkillData.CoolRemain = rSkillData.CoolTime;
        while (rSkillData.CoolRemain > 0)
        {
            yield return new WaitForSeconds(1);
            rSkillData.CoolRemain--;
        }
    }
}
