using Microsoft.Xna.Framework;
using System;

namespace MazeGame
{
    public class Player
    {
        public Vector2 Position { get; private set; }
        public Vector2 Direction { get; private set; }
        public Vector2 CameraPlane { get; private set; }

        public Player(Vector2 position)
        {
            Position = position;
            Direction = new Vector2(1, 0); // Initially facing right
            CameraPlane = new Vector2(0, 0.66f); // 66 degree field of view
        }

        public void Rotate(float angle)
        {
            // Rotate the direction vector
            Direction = new Vector2(
                Direction.X * MathF.Cos(angle) - Direction.Y * MathF.Sin(angle),
                Direction.X * MathF.Sin(angle) + Direction.Y * MathF.Cos(angle)
            );

            // Rotate the camera plane
            CameraPlane = new Vector2(
                CameraPlane.X * MathF.Cos(angle) - CameraPlane.Y * MathF.Sin(angle),
                CameraPlane.X * MathF.Sin(angle) + CameraPlane.Y * MathF.Cos(angle)
            );
        }

        public void Move(float distance)
        {
            // Move the player along the direction vector
            Position += Direction * distance;
        }
    }
}
