using UnityEngine;
using System;
using System.Collections.Generic;

namespace Cronica.Core.GameUtils
{
    /// <summary>
    /// 状态切换事件，携带目标对象、状态机实例及新旧状态
    /// </summary>
    public struct StateChangeEvent<T> where T : struct, IComparable, IConvertible, IFormattable
    {
        public GameObject Target;
        public StateMachine<T> StateMachine;
        public T NewState;
        public T PreviousState;

        public StateChangeEvent(StateMachine<T> machine)
        {
            Target = machine.Target;
            StateMachine = machine;
            NewState = machine.CurrentState;
            PreviousState = machine.PreviousState;
        }
    }

    /// <summary>
    /// 状态机公共接口，控制是否广播事件
    /// </summary>
    public interface IStateMachine
    {
        bool TriggerEvents { get; set; }
    }

    /// <summary>
    /// 通用状态机实现，支持本地回调与全局事件广播
    /// </summary>
    /// <typeparam name="T">状态枚举类型</typeparam>
    public class StateMachine<T> : IStateMachine where T : struct, IComparable, IConvertible, IFormattable
    {
        /// <summary>是否广播全局事件</summary>
        public bool TriggerEvents { get; set; }

        /// <summary>关联的游戏对象</summary>
        public GameObject Target { get; private set; }

        /// <summary>当前状态</summary>
        public T CurrentState { get; private set; }

        /// <summary>上一次状态</summary>
        public T PreviousState { get; private set; }

        /// <summary>
        /// 本地状态切换回调
        /// </summary>
        public event Action OnStateChanged;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="target">目标GameObject</param>
        /// <param name="triggerEvents">是否广播全局事件</param>
        /// <param name="onStateChanged">状态切换回调</param>
        public StateMachine(GameObject target, bool triggerEvents = false, Action onStateChanged = null)
        {
            Target = target;
            TriggerEvents = triggerEvents;
            PreviousState = default;
            CurrentState = default;
            OnStateChanged = onStateChanged;
        }

        /// <summary>
        /// 切换到指定状态（若相同则忽略），并触发回调/事件
        /// </summary>
        public void ChangeState(T newState)
        {
            if (EqualityComparer<T>.Default.Equals(newState, CurrentState))
            {
                return;
            }

            PreviousState = CurrentState;
            CurrentState = newState;

            OnStateChanged?.Invoke();

            if (TriggerEvents)
            {
                EventManager.Trigger(new StateChangeEvent<T>(this));
            }
        }

        /// <summary>
        /// 恢复到上一次状态，并触发回调/事件
        /// </summary>
        public void RestorePreviousState()
        {
            (CurrentState, PreviousState) = (PreviousState, CurrentState);

            OnStateChanged?.Invoke();

            if (TriggerEvents)
            {
                EventManager.Trigger(new StateChangeEvent<T>(this));
            }
        }
    }
}