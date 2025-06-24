using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cronica.Core.GameUtils
{
    /// <summary>
    /// 通用游戏事件，包含可选参数
    /// </summary>
    public struct GameEvent
    {
        public string Name;
        public int IntParam;
        public Vector2 Vector2Param;
        public Vector3 Vector3Param;
        public bool BoolParam;
        public string StringParam;

        /// <summary>
        /// 触发事件
        /// </summary>
        public static void Trigger(string name,
            int intParam = 0,
            Vector2 vector2Param = default,
            Vector3 vector3Param = default,
            bool boolParam = false,
            string stringParam = "")
        {
            var e = new GameEvent
            {
                Name = name,
                IntParam = intParam,
                Vector2Param = vector2Param,
                Vector3Param = vector3Param,
                BoolParam = boolParam,
                StringParam = stringParam
            };
            EventManager.Trigger(e);
        }
    }

    /// <summary>
    /// 事件管理器：注册、移除和广播事件
    /// </summary>
    [ExecuteAlways]
    public static class EventManager
    {
        private static Dictionary<Type, List<IEventListenerBase>> subscribers;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void Init()
        {
            subscribers = new Dictionary<Type, List<IEventListenerBase>>();
        }

        /// <summary>
        /// 添加事件监听器
        /// </summary>
        public static void AddListener<T>(IEventListener<T> listener) where T : struct
        {
            if (subscribers == null) Init();
            var type = typeof(T);
            if (!subscribers.TryGetValue(type, out var list))
            {
                list = new List<IEventListenerBase>();
                subscribers[type] = list;
            }

            if (!list.Contains(listener))
            {
                list.Add(listener);
            }
        }

        /// <summary>
        /// 移除事件监听器
        /// </summary>
        public static void RemoveListener<T>(IEventListener<T> listener) where T : struct
        {
            if (subscribers == null)
            {
#if EVENTROUTER_THROWEXCEPTIONS
            throw new ArgumentException("事件管理器尚未初始化。");
#else
                return;
#endif
            }

            var type = typeof(T);
            if (!subscribers.TryGetValue(type, out var list))
            {
#if EVENTROUTER_THROWEXCEPTIONS
            throw new ArgumentException($"类型 {type} 未注册任何监听器。");
#else
                return;
#endif
            }

            if (list.Remove(listener) && list.Count == 0)
            {
                subscribers.Remove(type);
            }
        }

        /// <summary>
        /// 广播事件
        /// </summary>
        public static void Trigger<T>(T evt) where T : struct
        {
            if (subscribers == null) Init();
            var type = typeof(T);
            if (!subscribers.TryGetValue(type, out var list))
            {
                return;
            }

            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (list[i] is IEventListener<T> listener)
                {
                    listener.OnEvent(evt);
                }
            }
        }
    }

    /// <summary>
    /// 扩展方法：启动和停止监听
    /// </summary>
    public static class EventExtensions
    {
        public static void StartListening<T>(this IEventListener<T> listener) where T : struct
        {
            EventManager.AddListener(listener);
        }

        public static void StopListening<T>(this IEventListener<T> listener) where T : struct
        {
            EventManager.RemoveListener(listener);
        }
    }

    /// <summary>
    /// 事件监听器基础接口
    /// </summary>
    public interface IEventListenerBase
    {
    }

    /// <summary>
    /// 事件监听器接口，需实现 OnEvent 方法
    /// </summary>
    public interface IEventListener<T> : IEventListenerBase where T : struct
    {
        void OnEvent(T evt);
    }

    /// <summary>
    /// 监听器包装类：自动注册和注销，并执行回调
    /// </summary>
    public class EventListenerWrapper<TOwner, TTarget, T> : IEventListener<T>, IDisposable where T : struct
    {
        private readonly Action<TTarget> callback;
        private readonly TOwner owner;

        public EventListenerWrapper(TOwner owner, Action<TTarget> callback)
        {
            this.owner = owner;
            this.callback = callback;
            this.StartListening();
        }

        public void Dispose()
        {
            this.StopListening();
        }

        public void OnEvent(T evt)
        {
            var item = HandleEvent(evt);
            callback?.Invoke(item);
        }

        /// <summary>
        /// 处理事件并返回回调参数，子类可重写
        /// </summary>
        protected virtual TTarget HandleEvent(T evt) => default;
    }
}