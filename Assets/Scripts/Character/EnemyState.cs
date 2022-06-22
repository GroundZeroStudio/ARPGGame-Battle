using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
	public class EnemyState : MonoBehaviour
	{
		private float mHP;
		private float mMaxHP;

		public void Damage(float nHurt)
        {
			mHP -= nHurt;
			if(mHP <= 0)
            {
				Death();
            }
        } 

		private void Death()
        {
			print("死亡");
        }
	}
}

