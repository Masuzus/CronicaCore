using UnityEngine;
using System.Collections.Generic;

namespace Cronica.Core.GameUtils
{
    public static class AnimatorHelper
    {
        /// <summary>
        /// 检查 Animator 是否包含指定类型和名称的参数
        /// </summary>
        public static bool HasParameter(this Animator animator, string name, AnimatorControllerParameterType type)
        {
            if (string.IsNullOrEmpty(name)) return false;

            foreach (var param in animator.parameters)
            {
                if (param.type == type && param.name == name)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 添加参数到哈希集合（如果存在）
        /// </summary>
        public static void RegisterParameter(this Animator animator, string parameterName, out int parameterHash,
            AnimatorControllerParameterType type, HashSet<int> parameterSet)
        {
            parameterHash = -1;
            if (string.IsNullOrEmpty(parameterName)) return;

            parameterHash = Animator.StringToHash(parameterName);
            if (animator.HasParameter(parameterName, type))
            {
                parameterSet.Add(parameterHash);
            }
        }

        // 基本参数操作方法（无安全检查）
        public static void SetValue(this Animator animator, string parameterName, bool value) =>
            animator.SetBool(parameterName, value);

        public static void SetValue(this Animator animator, string parameterName, int value) =>
            animator.SetInteger(parameterName, value);

        public static void SetValue(this Animator animator, string parameterName, float value) =>
            animator.SetFloat(parameterName, value);

        public static void SetValue(this Animator animator, string parameterName) => animator.SetTrigger(parameterName);

        // 带参数集合的安全检查方法
        public static bool SafeSetValue(this Animator animator, bool value, int parameterHash, HashSet<int> validParameters)
        {
            if (!validParameters.Contains(parameterHash)) return false;
            animator.SetBool(parameterHash, value);
            return true;
        }

        public static bool SafeSetValue(this Animator animator, int value, int parameterHash, HashSet<int> validParameters)
        {
            if (!validParameters.Contains(parameterHash)) return false;
            animator.SetInteger(parameterHash, value);
            return true;
        }

        public static bool SafeSetValue(this Animator animator, float value, int parameterHash, HashSet<int> validParameters)
        {
            if (!validParameters.Contains(parameterHash)) return false;
            animator.SetFloat(parameterHash, value);
            return true;
        }

        public static bool SafeSetValue(this Animator animator, int parameterHash, HashSet<int> validParameters)
        {
            if (!validParameters.Contains(parameterHash)) return false;
            animator.SetTrigger(parameterHash);
            return true;
        }

        // 自动检查参数是否存在的方法
        public static bool TrySetValue(this Animator animator, string parameterName, bool value)
        {
            if (!animator.HasParameter(parameterName, AnimatorControllerParameterType.Bool)) return false;
            animator.SetBool(parameterName, value);
            return true;
        }

        public static bool TrySetValue(this Animator animator, string parameterName, int value)
        {
            if (!animator.HasParameter(parameterName, AnimatorControllerParameterType.Int)) return false;
            animator.SetInteger(parameterName, value);
            return true;
        }

        public static bool TrySetValue(this Animator animator, string parameterName, float value)
        {
            if (!animator.HasParameter(parameterName, AnimatorControllerParameterType.Float)) return false;
            animator.SetFloat(parameterName, value);
            return true;
        }

        public static bool TrySetValue(this Animator animator, string parameterName)
        {
            if (!animator.HasParameter(parameterName, AnimatorControllerParameterType.Trigger)) return false;
            animator.SetTrigger(parameterName);
            return true;
        }
    }
}