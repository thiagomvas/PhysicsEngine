using Raylib_cs;
using System.Numerics;

namespace PhysicsEngine.Models
{
    public class VerletObject : BaseObject
    {
        public Vector2 PreviousPosition;

        public VerletObject(Vector2 position, float radius, Color color)
        {
            CurrentPosition = position;
            PreviousPosition = position;
            Acceleration = Vector2.Zero;
            Radius = radius;
            Color = color;
        }

        public override void UpdatePosition(float deltaTime)
        {
            if(IsFixedPoint) return;
            NaNOrOutOfBoundsCheck();
            Vector2 velocity = CurrentPosition - PreviousPosition;
            PreviousPosition = CurrentPosition;
            CurrentPosition = CurrentPosition + velocity + Acceleration * deltaTime * deltaTime;
            //Console.WriteLine($"{CurrentPosition} | {PreviousPosition} | {velocity}"); // For logging
            Acceleration = Vector2.Zero;
        }
    }
}
