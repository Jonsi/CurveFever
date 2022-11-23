using UnityEngine;

namespace Utils
{
    public static class WorldArea
    {
        public static Vector2 BottomLeft;
        public static Vector2 TopLeft;
        public static Vector2 TopRight;
        public static Vector2 BottomRight;

        public static void Initialize(Vector2 worldSize)
        {
            var xOffset = worldSize.x/2;
            var yOffset = worldSize.y/2;
            BottomLeft = Vector2.left * xOffset + Vector2.down * yOffset;
            TopLeft = Vector2.left * xOffset + Vector2.up * yOffset;
            TopRight = Vector2.right * xOffset + Vector2.up * yOffset;
            BottomRight = Vector2.right * xOffset + Vector2.down * yOffset;
        }

        public static Vector2 RandomWorldPosition(float edgeOffset)
        {
            var xPos = Random.Range(BottomLeft.x + edgeOffset, TopRight.x - edgeOffset);
            var yPos = Random.Range(BottomLeft.y + edgeOffset, TopRight.y - edgeOffset);
            return new Vector2(xPos, yPos);
        }

        public static bool IsInsidePerimeters(Vector2 position)
        {
            return position.x > BottomLeft.x &&
                   position.x < TopRight.x &&
                   position.y > BottomLeft.y &&
                   position.y < TopRight.y;
        }

        public static Vector2[] GetWorldAreaAsFramePoints(float offset = 0)
        {
            var points = new Vector2[]
            {
                WorldArea.BottomLeft + offset * (Vector2.down + Vector2.left),
                WorldArea.TopLeft + offset * (Vector2.up + Vector2.left),
                WorldArea.TopRight + offset * (Vector2.up + Vector2.right),
                WorldArea.BottomRight + offset * (Vector2.down + Vector2.right),
                WorldArea.BottomLeft + offset * (Vector2.down + Vector2.left),
            };

            return points;
        }
    }
}