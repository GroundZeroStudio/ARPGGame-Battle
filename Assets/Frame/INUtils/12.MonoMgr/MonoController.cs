using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace INFramework
{
	public class MonoController : MonoBehaviour
	{
		private event UnityAction mUpdateEvent = ()=> { };
        private void Start()
        {
            DontDestroyOnLoad(this.gameObject);
        }

        private void Update()
        {
            if(mUpdateEvent != null)
            {
                mUpdateEvent();
            }
        }

        public void AddUpdateListener(UnityAction fun)
        {
            mUpdateEvent += fun;
        }

        public void RemoveUpdateListener(UnityAction fun)
        {
            mUpdateEvent -= fun;
        }
    }
}

