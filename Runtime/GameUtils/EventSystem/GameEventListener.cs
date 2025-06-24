using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Cronica.Core.GameUtils
{
    /// <summary>
    /// 将此组件添加到物体上，可以在触发指定名称的事件时，轻松触发 UnityEvent。
    /// </summary>
    public class GameEventListener : MonoBehaviour, IEventListener<GameEvent>
    {
        [Tooltip("需要监听的事件名称")]
        public string EventName = "Load";
        
        /// <summary>
        /// 触发指定事件时调用的方法的 UnityEvent 钩子
        /// </summary>
        [Tooltip("触发指定事件时调用的方法的 UnityEvent 钩子")]
        public UnityEvent OnGameEvent;
		
        /// <summary>
        /// 当指定的 GameEvent 触发时，如果名称匹配，触发 UnityEvent
        /// </summary>
        /// <param name="gameEvent">触发的 GameEvent</param>
        public void OnEvent(GameEvent gameEvent)
        {
            // 判断触发的事件名称是否与指定的名称相同
            if (gameEvent.Name == EventName)
            {
                // 如果匹配，触发 UnityEvent
                OnGameEvent?.Invoke();
            }
        }

        /// <summary>
        /// 在启用时，开始监听指定的 GameEvent。您可以根据需求扩展为监听其他类型的事件。
        /// </summary>
        protected virtual void OnEnable()
        {
            // 开始监听 GameEvent 事件
            this.StartListening<GameEvent>();
        }

        /// <summary>
        /// 在禁用时，停止监听指定的 GameEvent。您可以根据需求扩展为停止监听其他类型的事件。
        /// </summary>
        protected virtual void OnDisable()
        {
            // 停止监听 GameEvent 事件
            this.StopListening<GameEvent>();
        }
    }
}