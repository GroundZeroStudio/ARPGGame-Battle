/****************************************************
    文件：CharacterMotor.cs
    作者：Olivia
    日期：2022/1/13 23:12:19
    功能：Nothing
*****************************************************/

using UnityEngine;


namespace ARPG.Character
{
    /// <summary>
    /// 角色马达：负责控制角色运动
    /// </summary>
    public class CharacterMotor : MonoBehaviour
    {
        private CharacterController mCC;
        private float mMoveSpeed = 5f;

        private float mRotateSpeed = 10f;
        private void Awake()
        {
            mCC = this.GetComponent<CharacterController>();
        }

        //朝一个方向旋转
        public void LookAtTarget(Vector3 rDirection)
        {
            Quaternion lookDir = Quaternion.LookRotation(rDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookDir, mRotateSpeed * Time.deltaTime);
        }

        //移动
        public void Movement(Vector3 rDirection)
        {
            LookAtTarget(rDirection);
            Vector3 forward = transform.forward;
            forward.y = -1;
            //移动
            mCC.Move(forward * Time.deltaTime * mMoveSpeed);
        }
    }
}


