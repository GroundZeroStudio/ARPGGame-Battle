using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 1.添加监听
/// 2.移除监听
/// 3.Action
/// </summary>
namespace INFramework
{
	public class EventMgr:Singleton<EventMgr>
	{
        private Dictionary<EventType, Delegate> m_EventTable = new Dictionary<EventType, Delegate>();

        private void OnListenerAdding(EventType eventType, Delegate callBack)
        {
            if (!m_EventTable.ContainsKey(eventType))
            {
                m_EventTable.Add(eventType, null);
            }
            Delegate d = m_EventTable[eventType];
            if (d != null && d.GetType() != callBack.GetType())
            {
                throw new Exception(string.Format("尝试为事件{0}添加不同类型的委托，当前事件所对应的委托是{1}，要添加的委托类型为{2}", eventType, d.GetType(), callBack.GetType()));
            }
        }
        private void OnListenerRemoving(EventType eventType, Delegate callBack)
        {
            if (m_EventTable.ContainsKey(eventType))
            {
                Delegate d = m_EventTable[eventType];
                if (d == null)
                {
                    throw new Exception(string.Format("移除监听错误：事件{0}没有对应的委托", eventType));
                }
                else if (d.GetType() != callBack.GetType())
                {
                    throw new Exception(string.Format("移除监听错误：尝试为事件{0}移除不同类型的委托，当前委托类型为{1}，要移除的委托类型为{2}", eventType, d.GetType(), callBack.GetType()));
                }
            }
            else
            {
                throw new Exception(string.Format("移除监听错误：没有事件码{0}", eventType));
            }
        }
        private void OnListenerRemoved(EventType eventType)
        {
            if (m_EventTable[eventType] == null)
            {
                m_EventTable.Remove(eventType);
            }
        }
        //no parameters
        public void AddListener(EventType eventType, Action callBack)
        {
            OnListenerAdding(eventType, callBack);
            m_EventTable[eventType] = (Action)m_EventTable[eventType] + callBack;
        }
        //Single parameters
        public void AddListener<T>(EventType eventType, Action<T> callBack)
        {
            OnListenerAdding(eventType, callBack);
            m_EventTable[eventType] = (Action<T>)m_EventTable[eventType] + callBack;
        }
        //two parameters
        public void AddListener<T, X>(EventType eventType, Action<T, X> callBack)
        {
            OnListenerAdding(eventType, callBack);
            m_EventTable[eventType] = (Action<T, X>)m_EventTable[eventType] + callBack;
        }
        //three parameters
        public void AddListener<T, X, Y>(EventType eventType, Action<T, X, Y> callBack)
        {
            OnListenerAdding(eventType, callBack);
            m_EventTable[eventType] = (Action<T, X, Y>)m_EventTable[eventType] + callBack;
        }
        //four parameters
        public void AddListener<T, X, Y, Z>(EventType eventType, Action<T, X, Y, Z> callBack)
        {
            OnListenerAdding(eventType, callBack);
            m_EventTable[eventType] = (Action<T, X, Y, Z>)m_EventTable[eventType] + callBack;
        }
        //five parameters
        public void AddListener<T, X, Y, Z, W>(EventType eventType, Action<T, X, Y, Z, W> callBack)
        {
            OnListenerAdding(eventType, callBack);
            m_EventTable[eventType] = (Action<T, X, Y, Z, W>)m_EventTable[eventType] + callBack;
        }

        //no parameters
        public void RemoveListener(EventType eventType, Action callBack)
        {
            OnListenerRemoving(eventType, callBack);
            m_EventTable[eventType] = (Action)m_EventTable[eventType] - callBack;
            OnListenerRemoved(eventType);
        }
        //single parameters
        public void RemoveListener<T>(EventType eventType, Action<T> callBack)
        {
            OnListenerRemoving(eventType, callBack);
            m_EventTable[eventType] = (Action<T>)m_EventTable[eventType] - callBack;
            OnListenerRemoved(eventType);
        }
        //two parameters
        public void RemoveListener<T, X>(EventType eventType, Action<T, X> callBack)
        {
            OnListenerRemoving(eventType, callBack);
            m_EventTable[eventType] = (Action<T, X>)m_EventTable[eventType] - callBack;
            OnListenerRemoved(eventType);
        }
        //three parameters
        public void RemoveListener<T, X, Y>(EventType eventType, Action<T, X, Y> callBack)
        {
            OnListenerRemoving(eventType, callBack);
            m_EventTable[eventType] = (Action<T, X, Y>)m_EventTable[eventType] - callBack;
            OnListenerRemoved(eventType);
        }
        //four parameters
        public void RemoveListener<T, X, Y, Z>(EventType eventType, Action<T, X, Y, Z> callBack)
        {
            OnListenerRemoving(eventType, callBack);
            m_EventTable[eventType] = (Action<T, X, Y, Z>)m_EventTable[eventType] - callBack;
            OnListenerRemoved(eventType);
        }
        //five parameters
        public void RemoveListener<T, X, Y, Z, W>(EventType eventType, Action<T, X, Y, Z, W> callBack)
        {
            OnListenerRemoving(eventType, callBack);
            m_EventTable[eventType] = (Action<T, X, Y, Z, W>)m_EventTable[eventType] - callBack;
            OnListenerRemoved(eventType);
        }


        //no parameters
        public void Broadcast(EventType eventType)
        {
            Delegate d;
            if (m_EventTable.TryGetValue(eventType, out d))
            {
                Action callBack = d as Action;
                if (callBack != null)
                {
                    callBack();
                }
                else
                {
                    throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型", eventType));
                }
            }
        }
        //single parameters
        public void Broadcast<T>(EventType eventType, T arg)
        {
            Delegate d;
            if (m_EventTable.TryGetValue(eventType, out d))
            {
                Action<T> callBack = d as Action<T>;
                if (callBack != null)
                {
                    callBack(arg);
                }
                else
                {
                    throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型", eventType));
                }
            }
        }
        //two parameters
        public void Broadcast<T, X>(EventType eventType, T arg1, X arg2)
        {
            Delegate d;
            if (m_EventTable.TryGetValue(eventType, out d))
            {
                Action<T, X> callBack = d as Action<T, X>;
                if (callBack != null)
                {
                    callBack(arg1, arg2);
                }
                else
                {
                    throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型", eventType));
                }
            }
        }
        //three parameters
        public void Broadcast<T, X, Y>(EventType eventType, T arg1, X arg2, Y arg3)
        {
            Delegate d;
            if (m_EventTable.TryGetValue(eventType, out d))
            {
                Action<T, X, Y> callBack = d as Action<T, X, Y>;
                if (callBack != null)
                {
                    callBack(arg1, arg2, arg3);
                }
                else
                {
                    throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型", eventType));
                }
            }
        }
        //four parameters
        public void Broadcast<T, X, Y, Z>(EventType eventType, T arg1, X arg2, Y arg3, Z arg4)
        {
            Delegate d;
            if (m_EventTable.TryGetValue(eventType, out d))
            {
                Action<T, X, Y, Z> callBack = d as Action<T, X, Y, Z>;
                if (callBack != null)
                {
                    callBack(arg1, arg2, arg3, arg4);
                }
                else
                {
                    throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型", eventType));
                }
            }
        }
        //five parameters
        public void Broadcast<T, X, Y, Z, W>(EventType eventType, T arg1, X arg2, Y arg3, Z arg4, W arg5)
        {
            Delegate d;
            if (m_EventTable.TryGetValue(eventType, out d))
            {
                Action<T, X, Y, Z, W> callBack = d as Action<T, X, Y, Z, W>;
                if (callBack != null)
                {
                    callBack(arg1, arg2, arg3, arg4, arg5);
                }
                else
                {
                    throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型", eventType));
                }
            }
        }
    }
}

