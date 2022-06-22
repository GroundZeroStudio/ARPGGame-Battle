using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace INFramework
{
	public class SceneMgr : Singleton<SceneMgr>
	{
		public void LoadScene(string rName, Action rCallBack = null)
        {
			SceneManager.LoadScene(rName);
			if(rCallBack != null)
            {
				rCallBack();
			}
        }

		public void LoadSceneAsync(string rName, Action rCallBack = null)
        {
			MonoMgr.Instance.StartCoroutine(InnerLoadSceneAsync(rName, rCallBack));
        }

		private IEnumerator InnerLoadSceneAsync(string rName, Action rCallBack)
        {
			AsyncOperation ao = SceneManager.LoadSceneAsync(rName);
            while (!ao.isDone)
            {
				yield return ao.progress;
            }
			if(rCallBack != null)
            {
				rCallBack();
            }
        }
	}
}

