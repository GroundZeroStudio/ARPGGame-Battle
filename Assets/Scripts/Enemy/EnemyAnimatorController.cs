using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [Serializable]
	public class EnemyAnimationConst
    {
        public string RunName = "run";
        public string AtkName = "shooting";
        public string DeathName = "death";
        public string IdleName = "idleWgun";
    }

	public class EnemyAnimatorController : MonoBehaviour
    {
        public EnemyAnimationConst AnimationConst;


	}
}

