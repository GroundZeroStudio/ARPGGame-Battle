/****************************************************
    文件：CharacterStatus.cs
    作者：Olivia
    日期：2022/2/5 0:59:25
    功能：Nothing
*****************************************************/

using UnityEngine;

public class CharacterStatus : MonoBehaviour
{
    public CharacterAnimationParameter AnimationParam;

    public float HP;
    public float MaxHP;
    public float SP;
    public float MaxSP;
    public float BaseATK;
    public float Defence;
    public float AttackInterval;
    public float AttackDistance;

    //血量
    public void Damage(float value)
    {
        float damage = value - Defence;
        if (damage <= 0) return;
        HP -= damage;
        if (HP <= 0)
        {
            Dead();
        }
    }

    protected virtual void Dead()
    {
        print("Dead");
        GetComponentInChildren<Animator>().SetBool(AnimationParam.Death, true);

    }
}
