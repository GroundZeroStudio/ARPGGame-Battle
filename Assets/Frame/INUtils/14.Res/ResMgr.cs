using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 1.同步资源加载
/// 2.异步加载资源
/// </summary>

namespace INFramework
{
	public class ResMgr : Singleton<ResMgr>
	{

        private ResMgr()
        {

        }

		public T Load<T>(string rName) where T: UnityEngine.Object
        {
			return Resources.Load<T>(rName) as T;
        }

		public void LoadAsync<T>(string rName, Action<T> rCallBack) where T : UnityEngine.Object
        {
            MonoMgr.Instance.StartCoroutine(InnerLoadAsync(rName, rCallBack));
        }

		private IEnumerator InnerLoadAsync<T>(string rName, Action<T> rCallBack) where T : UnityEngine.Object
        {
			ResourceRequest rq = Resources.LoadAsync<T>(rName);
			yield return rq;
			if(rq.asset is GameObject)
            {
				rCallBack(GameObject.Instantiate(rq.asset) as T);
            }
            else
            {
                rCallBack(rq.asset as T);
            }

        }
	}
}

