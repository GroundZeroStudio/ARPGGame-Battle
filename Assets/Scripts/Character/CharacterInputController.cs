/****************************************************
    文件：CharacterInputController.cs
    作者：Olivia
    日期：2022/1/13 23:12:40
    功能：Nothing
*****************************************************/

using ARPG.Skill;
using System;
using UnityEngine;

namespace ARPG.Character
{
    /// <summary>
    /// 角色输入控制器 EasyTouch
    /// </summary>
    [RequireComponent(typeof(CharacterMotor), typeof(PlayerStatus))]
    public class CharacterInputController : MonoBehaviour
    {
        private ETCJoystick mETCJoystick;
        private CharacterMotor mMotor;
        private ETCButton[] mSkillButtons;

        [SerializeField]
        private PlayerStatus mPlayerStatus;
        [HideInInspector]
        public Animator Animator;

        private CharacterSkillManager mSkillMgr;

        private void Awake()
        {
            //查找组件
            mETCJoystick = FindObjectOfType<ETCJoystick>();
            mSkillButtons = FindObjectsOfType<ETCButton>();
            mMotor = GetComponent<CharacterMotor>();
            mPlayerStatus = this.GetComponent<PlayerStatus>();
            Animator = this.GetComponentInChildren<Animator>();
            mSkillMgr = this.GetComponent<CharacterSkillManager>();
        }

        //注册事件
        private void OnEnable()
        {
            mETCJoystick.onMove.AddListener(OnJoystickMove);
            mETCJoystick.onMoveStart.AddListener(OnJoystickMoveStart);
            mETCJoystick.onMoveEnd.AddListener(OnJoystickMoveEnd);
            for (int i = 0; i < mSkillButtons.Length; i++)
            {
                mSkillButtons[i].onDown.AddListener(OnJoystickSkillButton);
            }
        }

        private void OnJoystickSkillButton(string arg0)
        {
            int nSkillId = 0;
            if(arg0 == "SkillButton01")
            {
                nSkillId = 1001;
            }
            else if(arg0 == "SkillButton02")
            {
                nSkillId = 1002;
            }
            else if(arg0 == "SkillButton03")
            {
                nSkillId = 1003;
            }
            SkillData skillData = this.mSkillMgr.PrepareSkill(nSkillId);
            if(skillData != null)
            {
                Debug.Log(arg0);
                Debug.Log("点击了攻击键");
                this.mSkillMgr.GenerateSkill(skillData);
            }
        }

        private void OnJoystickMoveEnd()
        {
            this.Animator.SetBool(mPlayerStatus.AnimationParam.Run, false);
        }

        private void OnJoystickMoveStart()
        {
            //开始播放动画
            this.Animator.SetBool(mPlayerStatus.AnimationParam.Run, true);
        }

        private void OnJoystickMove(Vector2 dir)
        {
            //调用马达移动功能
            //dir.x 左右
            //dir.y 上下  
            mMotor.Movement(new Vector3(dir.x, 0, dir.y));
        }

        //注销事件
        private void OnDisable()
        {
            mETCJoystick.onMove.RemoveListener(OnJoystickMove);
            mETCJoystick.onMoveStart.RemoveListener(OnJoystickMoveStart);
            mETCJoystick.onMoveEnd.RemoveListener(OnJoystickMoveEnd);
            for (int i = 0; i < mSkillButtons.Length; i++)
            {
                mSkillButtons[i].onDown.RemoveListener(OnJoystickSkillButton);
            }
        }
    }
}



