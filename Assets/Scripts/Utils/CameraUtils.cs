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
}