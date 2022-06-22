using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace INFramework
{
    public class ObjectPool : MonoSingleton<ObjectPool>
    {
        private Dictionary<int, List<GameObject>> objectPools = new Dictionary<int, List<GameObject>>();

        private GameObject Add(int instanceId, GameObject instance)
        {
            if (objectPools.ContainsKey(instanceId))
            {
                objectPools[instanceId].Add(instance);
            }
            else
            {
                objectPools.Add(instanceId, new List<GameObject>() { instance });
            }

            return instance;
        }

        private GameObject Get(int instanceId)
        {
            if (objectPools.ContainsKey(instanceId))
            {
                for (int i = 0; i < objectPools[instanceId].Count; i++)
                {
                    if (objectPools[instanceId][i].GetComponent<IReuseable>().CanReuse())
                    {
                        return objectPools[instanceId][i];
                    }
                }
            }

            return null;
        }

        public T Create<T>(GameObject prefab, Transform parent = null) where T : Component
        {
            int instanceId = prefab.GetInstanceID();
            GameObject obj = Get(instanceId);
            if (obj == null)
                obj = Add(instanceId, Instantiate(prefab, parent));
            obj.gameObject.SetActive(true);
            if (parent != null)
            {
                if (prefab.transform.parent != parent)
                {
                    prefab.transform.SetParent(parent);
                }
            }
            return obj.GetComponent<T>();
        }

        public void Close(int instanceId)
        {
            if (!objectPools.ContainsKey(instanceId))
            {
                return;
            }

            for (int i = 0; i < objectPools[instanceId].Count; i++)
            {
                objectPools[instanceId][i].SetActive(false);
            }
        }

        public List<T> CreateSelfPool<T>(List<T> list, T prefab, Transform parent, int length) where T : Component
        {
            if (list == null)
            {
                list = new List<T>();
                for (int i = 0; i < length; i++)
                {
                    T t = Instantiate(prefab, parent);
                    list.Add(t);
                }
            }
            else
            {
                if (list.Count >= length)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        list[i].gameObject.SetActive(i < length);
                    }
                }
                else
                {
                    for (int i = 0; i < length; i++)
                    {
                        if (list.Count > i)
                        {
                            list[i].gameObject.SetActive(true);
                        }
                        else
                        {
                            T t = Instantiate(prefab, parent);
                            list.Add(t);
                        }
                    }
                }
            }
            return list;
        }
    }
}



