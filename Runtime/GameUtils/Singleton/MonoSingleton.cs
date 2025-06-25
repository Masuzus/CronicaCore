using UnityEngine;

namespace Cronica.Core.GameUtils
{
    //单例类
    public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T s_ist = null;

        public static T GetInstance(string strName = "")
        {
            if (s_ist == null)
            {
                var type = typeof(T);
                var objects = FindObjectsByType<T>(FindObjectsInactive.Include, FindObjectsSortMode.None);

                if (objects.Length > 0)
                {
                    s_ist = objects[0];
                    if (objects.Length > 1)
                    {
                        Debug.LogError(
                            "There is more than one instance of Singlen of type \"" + type + "\". Keeping the first.Destroying the others.");
                        for (var i = 1; i < objects.Length; i++)
                        {
                            DestroyImmediate(objects[i].gameObject);
                        }
                    }
                }
                else
                {
                    if (strName != "")
                    {
                        GameObject goObject = GameObject.Find(strName);
                        s_ist = goObject.AddComponent<T>();
                    }
                    else
                    {
                        GameObject go = new GameObject();
                        go.name = "[" + type.ToString() + "]";
                        s_ist = go.AddComponent<T>();
                    }
                }
            }

            return s_ist;
        }
    }
}