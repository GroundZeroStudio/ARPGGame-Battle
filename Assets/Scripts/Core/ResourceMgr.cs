/****************************************************
    文件：ResourceMgr.cs
    作者：Olivia
    日期：2022/2/7 22:42:33
    功能：Nothing
*****************************************************/
using INFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class ResourceMgr: MonoSingleton<ResourceMgr>
{
    private Dictionary<string, string> mResPathDict = new Dictionary<string, string>();

    private void Start()
    {
        InitResConfig();
    }

    //public string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, "MyFile");
    //public string result = "";
    //IEnumerator Example()
    //{
    //    if (filePath.Contains("://"))
    //    {
    //        WWW www = new WWW(filePath);
    //        yield return www;
    //        result = www.text;
    //    }
    //    else
    //        result = System.IO.File.ReadAllText(filePath);

        private void InitResConfig()
    { 
        string url = Application.streamingAssetsPath + "/ConfigMap.txt";
        UnityWebRequest webRequest = UnityWebRequest.Get(url);
        webRequest.SendWebRequest();
        while (true)
        {
            if (webRequest.isDone)
            {
                break;
            }
        }
        if (webRequest.error != null)
        {
            Debug.Log(webRequest.downloadHandler.text);
        }
        else
        {
            Debug.Log("write error");
        }
    }


    public T Load<T>(string rName)
    {
        return default(T);
    }

    public GameObject LoadFxPrefab(string rPath)
    {
        var rKey = rPath.Substring(rPath.LastIndexOf("/") + 1);
        var rGo = Resources.Load<GameObject>(rPath);
        GameObjectPool.Instance.Recycle(GameObjectPool.Instance.CreateObject(rKey, rGo, transform.position, transform.rotation));
        return rGo;
    }
}