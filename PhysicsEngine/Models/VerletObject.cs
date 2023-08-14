using Raylib_cs;
using System.Numerics;

namespace PhysicsEngine.Models
{
    public class VerletObject
    {
        public Vector2 CurrentPosition;
        public Vector2 PreviousPosition;
        public Vector2 Acceleration;
        public float Radius;
        public Color Color;

        public VerletObject(Vector2 position, float radius, Color color)
        {
            CurrentPosition = position;
            PreviousPosition = position;
            Acceleration = Vector2.Zero;
            Radius = radius;
            Color = color;
        }
        public void UpdatePosition(float deltaTime)
        {
            NaNOrOutOfBoundsCheck();
            Vector2 velocity = CurrentPosition - PreviousPosition;
            PreviousPosition = CurrentPosition;
            CurrentPosition = CurrentPosition + velocity + Acceleration * deltaTime * deltaTime;
            //Console.WriteLine($"{CurrentPosition} | {PreviousPosition} | {velocity}"); // For logging
            Acceleration = Vector2.Zero;
        }

        public void Accelerate(Vector2 acc)
        {
            Acceleration += acc;
        }

        public void AccelerateTowards(Vector2 pos, float force)
        {
            var dist = pos - CurrentPosition;
            Acceleration += force * dist/(dist.Length() + .1f);
        }

        public void NaNOrOutOfBoundsCheck()
        {
            if (float.IsNaN(CurrentPosition.X) || 
                float.IsNaN(CurrentPosition.Y) ||
                CurrentPosition.X > PhysicsEngine.Space.X ||
                CurrentPosition.Y > PhysicsEngine.Space.Y ||
                CurrentPosition.X < 0 ||
                CurrentPosition.Y < 0) Console.WriteLine(CurrentPosition);
        }
    }
}
