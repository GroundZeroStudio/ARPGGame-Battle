/****************************************************
    文件：GameObjectPool.cs
    作者：Olivia
    日期：2022/5/24 21:29:48
    功能：Nothing
*****************************************************/
using INFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool : MonoSingleton<GameObjectPool>
{
    private Dictionary<string, List<GameObject>> mCache;

    private GameObjectPool()
    {
        mCache = new Dictionary<string, List<GameObject>>();
    }

    /// <summary>增加物体进入池(按类别增加)</summary>
    private void Add(string key, GameObject go)
    {
        //1.如果key在容器中存在，则将go加入对应的列表
        //2.如果key在容器中不存在，是先创建一个列表，再加入
        if (!mCache.ContainsKey(key))
            mCache.Add(key, new List<GameObject>());
        mCache[key].Add(go);
    }

    /// <summary>销毁物体(将对象隐藏)</summary>
    public void Recycle(GameObject rGo)
    {
        //设置destoryGo隐藏
        rGo.SetActive(false);
    }

    /// <summary>将对象归入池中<summary>
    public void Recycle(GameObject rTempGo, float rDelay)
    {
        //开启一个协程
        StartCoroutine(DelayRecycle(rTempGo, rDelay));
    }

    /// <summary>延迟销毁</summary>
    private IEnumerator DelayRecycle(GameObject rGo, float rDelay)
    {
        //等待一个延迟的时间
        yield return new WaitForSeconds(rDelay);
        Recycle(rGo);
    }

    /// <summary>取出可用的物体（已经隐藏的）</summary>
    public GameObject FindUsable(string rKey)
    {
        //1.在容器中找出key对应的列表，从列表中找出已经为隐藏状态的对象，返回
        if (mCache.ContainsKey(rKey))
            foreach (GameObject item in mCache[rKey])
            {
                if (!item.activeSelf)
                    return item;
            }
        return null;
    }

    /// <summary>创建一个游戏物体到场景 </summary>
    public GameObject CreateObject(string rKey, GameObject rGo, Vector3 rPosition, Quaternion rQuaternion)
    {
        //先找是否有可用的，如果没有则创建，如果有找到后设置好位置，朝向再返回
        GameObject rTempGo = FindUsable(rKey);
        if (rTempGo != null)
        {
            rTempGo.transform.position = rPosition;
            rTempGo.transform.rotation = rQuaternion;
            rTempGo.SetActive(true);
        }
        else
        {
            rTempGo = GameObject.Instantiate(rGo, rPosition, rQuaternion) as GameObject;
            Add(rKey, rTempGo);
        }
        return rTempGo;

    }

    /// <summary>清空某类游戏对象</summary>
    public void Clear(string key)
    {
        mCache.Remove(key);
    }

    /// <summary>清空池中所有游戏对象</summary>
    public void ClearAll()
    {
        mCache.Clear();
    }
}