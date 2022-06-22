using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;

namespace INFramework
{
	public class MonoMgr : Singleton<MonoMgr>
	{
		//全局唯一的对象
		private MonoController mController;

		private MonoMgr()
        {
			GameObject obj = new GameObject("MonoController");
			mController = obj.AddComponent<MonoController>();
        }

		public void AddUpdateListener(UnityAction fun)
        {
			mController.AddUpdateListener(fun);
        }

		public void RemoveUpdateListener(UnityAction fun)
        {
			mController.RemoveUpdateListener(fun);
        }

        public Coroutine StartCoroutine(IEnumerator routine)
        {
            return mController.StartCoroutine(routine);
        }

        public Coroutine StartCoroutine(string methodName, [DefaultValue("null")] object value)
        {
            return mController.StartCoroutine(methodName, value);
        }

        public Coroutine StartCoroutine(string methodName)
        {
            return mController.StartCoroutine(methodName);
        }
    }
}

