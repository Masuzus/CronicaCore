using System;
using System.Collections.Generic;

namespace Cronica.Core.GameUtils
{
    //本地消息发送（单例）
    public static class LocalMsgDispatch
    {
        static Dictionary<LocalMsgDef, List<Delegate>> m_dicMsgCache = new Dictionary<LocalMsgDef, List<Delegate>>();

        static void AddListener(LocalMsgDef key, Delegate callBack)
        {
            List<Delegate> eventList;
            if (m_dicMsgCache.TryGetValue(key, out eventList))
            {
                eventList.Add(callBack);
            }
            else
            {
                eventList = new List<Delegate>();
                eventList.Add(callBack);
                m_dicMsgCache.Add(key, eventList);
            }
        }

        static void RemoveListener(LocalMsgDef key, Delegate callBack)
        {
            List<Delegate> eventList;
            if (m_dicMsgCache.TryGetValue(key, out eventList))
            {
                eventList.Remove(callBack);
            }

            if (eventList.Count == 0)
            {
                m_dicMsgCache.Remove(key);
            }
        }

        public static void RemoveAllListener()
        {
            m_dicMsgCache.Clear();
        }

        public static void AddListener(LocalMsgDef key, Action callBack)
        {
            AddListener(key, (Delegate)callBack);
        }

        public static void AddListener<T1>(LocalMsgDef key, Action<T1> callBack)
        {
            AddListener(key, (Delegate)callBack);
        }

        public static void AddListener<T1, T2>(LocalMsgDef key, Action<T1, T2> callBack)
        {
            AddListener(key, (Delegate)callBack);
        }

        public static void AddListener<T1, T2, T3>(LocalMsgDef key, Action<T1, T2, T3> callBack)
        {
            AddListener(key, (Delegate)callBack);
        }

        public static void AddListener<T1, T2, T3, T4>(LocalMsgDef key, Action<T1, T2, T3, T4> callBack)
        {
            AddListener(key, (Delegate)callBack);
        }

        public static void AddListener<T1, T2, T3, T4, T5>(LocalMsgDef key, Action<T1, T2, T3, T4, T5> callBack)
        {
            AddListener(key, (Delegate)callBack);
        }

        public static void RemoveListener(LocalMsgDef key, Action callBack)
        {
            RemoveListener(key, (Delegate)callBack);
        }

        public static void RemoveListener<T1>(LocalMsgDef key, Action<T1> callBack)
        {
            RemoveListener(key, (Delegate)callBack);
        }

        public static void RemoveListener<T1, T2>(LocalMsgDef key, Action<T1, T2> callBack)
        {
            RemoveListener(key, (Delegate)callBack);
        }

        public static void RemoveListener<T1, T2, T3>(LocalMsgDef key, Action<T1, T2, T3> callBack)
        {
            RemoveListener(key, (Delegate)callBack);
        }

        public static void RemoveListener<T1, T2, T3, T4>(LocalMsgDef key, Action<T1, T2, T3, T4> callBack)
        {
            RemoveListener(key, (Delegate)callBack);
        }

        public static void RemoveListener<T1, T2, T3, T4, T5>(LocalMsgDef key, Action<T1, T2, T3, T4, T5> callBack)
        {
            RemoveListener(key, (Delegate)callBack);
        }

        public static void SendMsg(LocalMsgDef key)
        {
            List<Delegate> eventList;
            if (m_dicMsgCache.TryGetValue(key, out eventList))
            {
                for (int i = 0; i < eventList.Count; i++)
                {
                    if (eventList[i] is Action)
                    {
                        Action callBack = eventList[i] as Action;
                        callBack?.Invoke();
                    }
                }
            }
        }

        public static void SendMsg<T1>(LocalMsgDef key, T1 arg1)
        {
            List<Delegate> eventList;
            if (m_dicMsgCache.TryGetValue(key, out eventList))
            {
                for (int i = 0; i < eventList.Count; i++)
                {
                    if (eventList[i] is Action<T1>)
                    {
                        Action<T1> callBack = eventList[i] as Action<T1>;
                        callBack?.Invoke(arg1);
                    }
                }
            }
        }

        public static void SendMsg<T1, T2>(LocalMsgDef key, T1 arg1, T2 arg2)
        {
            List<Delegate> eventList;
            if (m_dicMsgCache.TryGetValue(key, out eventList))
            {
                for (int i = 0; i < eventList.Count; i++)
                {
                    if (eventList[i] is Action<T1, T2>)
                    {
                        Action<T1, T2> callBack = eventList[i] as Action<T1, T2>;
                        callBack?.Invoke(arg1, arg2);
                    }
                }
            }
        }

        public static void SendMsg<T1, T2, T3>(LocalMsgDef key, T1 arg1, T2 arg2, T3 arg3)
        {
            List<Delegate> eventList;
            if (m_dicMsgCache.TryGetValue(key, out eventList))
            {
                for (int i = 0; i < eventList.Count; i++)
                {
                    if (eventList[i] is Action<T1, T2, T3>)
                    {
                        Action<T1, T2, T3> callBack = eventList[i] as Action<T1, T2, T3>;
                        callBack?.Invoke(arg1, arg2, arg3);
                    }
                }
            }
        }

        public static void SendMsg<T1, T2, T3, T4>(LocalMsgDef key, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            List<Delegate> eventList;
            if (m_dicMsgCache.TryGetValue(key, out eventList))
            {
                for (int i = 0; i < eventList.Count; i++)
                {
                    if (eventList[i] is Action<T1, T2, T3, T4>)
                    {
                        Action<T1, T2, T3, T4> callBack = eventList[i] as Action<T1, T2, T3, T4>;
                        callBack?.Invoke(arg1, arg2, arg3, arg4);
                    }
                }
            }
        }

        public static void SendMsg<T1, T2, T3, T4, T5>(LocalMsgDef key, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            List<Delegate> eventList;
            if (m_dicMsgCache.TryGetValue(key, out eventList))
            {
                for (int i = 0; i < eventList.Count; i++)
                {
                    if (eventList[i] is Action<T1, T2, T3, T4, T5>)
                    {
                        Action<T1, T2, T3, T4, T5> callBack = eventList[i] as Action<T1, T2, T3, T4, T5>;
                        callBack?.Invoke(arg1, arg2, arg3, arg4, arg5);
                    }
                }
            }
        }
    }
}