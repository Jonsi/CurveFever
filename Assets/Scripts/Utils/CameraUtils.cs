using UnityEngine;

namespace Utils
{
    public static class CameraUtils
    {
        public static Vector2 GetCameraBoundariesSize(Camera camera)
        {
            var sizeY = camera.orthographicSize * 2;
            var screenRatio =(float) Screen.width / Screen.height;
            var sizeX = sizeY * screenRatio;
            return new Vector2(sizeX, sizeY);
        }
    }
    public static class MyVector3Extension
    {
        public static Vector2[] ToVector2Array (this Vector3[] v3)
        {
            return System.Array.ConvertAll<Vector3, Vector2> (v3, GetV3FromV2);
        }
         
        private static Vector2 GetV3FromV2 (Vector3 v3)
        {
            return new Vector2 (v3.x, v3.y);
        }
    }
}