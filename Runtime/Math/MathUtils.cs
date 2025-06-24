using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cronica.Core.Math
{
    public static class MathUtils
    {
        /// <summary>
        /// Returns the distance between a point and a line.
        /// </summary>
        /// <returns>The between point and line.</returns>
        /// <param name="point">Point.</param>
        /// <param name="lineStart">Line start.</param>
        /// <param name="lineEnd">Line end.</param>
        public static float DistanceBetweenPointAndLine(Vector3 point, Vector3 lineStart, Vector3 lineEnd)
        {
            return Vector3.Magnitude(ProjectPointOnLine(point, lineStart, lineEnd) - point);
        }
    
        /// <summary>
        /// Projects a point on a line (perpendicularly) and returns the projected point.
        /// </summary>
        /// <returns>The point on line.</returns>
        /// <param name="point">Point.</param>
        /// <param name="lineStart">Line start.</param>
        /// <param name="lineEnd">Line end.</param>
        public static Vector3 ProjectPointOnLine(Vector3 point, Vector3 lineStart, Vector3 lineEnd)
        {
            Vector3 rhs = point - lineStart;
            Vector3 vector2 = lineEnd - lineStart;
            float magnitude = vector2.magnitude;
            Vector3 lhs = vector2;
            if (magnitude > 1E-06f)
            {
                lhs = (Vector3)(lhs / magnitude);
            }
            float num2 = Mathf.Clamp(Vector3.Dot(lhs, rhs), 0f, magnitude);
            return (lineStart + ((Vector3)(lhs * num2)));
        }
        
        /// <summary>
        /// 计算并返回两个二维向量之间的夹角，结果基于360°的角度，
        /// 从 vectorA 到 vectorB，逆时针为正，顺时针为负，最后映射到 [0,360)。
        /// </summary>
        public static float AngleBetween(Vector2 a, Vector2 b)
        {
            // 直接用 Atan2(cross, dot) 得到 -180°~+180° 之间的夹角
            float cross = a.x * b.y - a.y * b.x;
            float dot   = Vector2.Dot(a, b);
            float angle = Mathf.Atan2(cross, dot) * Mathf.Rad2Deg;
    
            // 映射到 [0,360)
            if (angle < 0f) angle += 360f;
            return angle;
        }
    }
}
