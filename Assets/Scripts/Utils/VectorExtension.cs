using UnityEngine;

namespace Utils
{
    public static class VectorExtension
    {
        public static Vector2[] ToVector2Array (this Vector3[] v3)
        {
            return System.Array.ConvertAll<Vector3, Vector2> (v3, GetV2FromV3);
        }
        
        public static Vector3[] ToVector3Array (this Vector2[] v2)
        {
            return System.Array.ConvertAll<Vector2, Vector3> (v2, GetV3FromV2);
        }

        private static Vector3 GetV3FromV2(Vector2 v2)
        {
            return new Vector3(v2.x, v2.y, 0f);
        }

        private static Vector2 GetV2FromV3 (Vector3 v3)
        {
            return new Vector2 (v3.x, v3.y);
        }
    }
}