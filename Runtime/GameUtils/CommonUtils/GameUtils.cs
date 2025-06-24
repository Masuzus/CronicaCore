using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Cronica.Core.GameUtils
{
    public static class GameUtils
    {
        public static void RemoveAllChild(this Transform parent)
        {
            var count = parent.childCount;
            for (int i = count - 1; i >= 0; i--)
            {
                Object.Destroy(parent.GetChild(i).gameObject);
            }
            parent.DetachChildren();
        }
    
        //生成guid
        public static Guid GenerateGuid()
        {
            return Guid.NewGuid();
        }
    }
}