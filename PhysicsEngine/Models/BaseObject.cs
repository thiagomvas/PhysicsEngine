using System.Numerics;
using Raylib_cs;

namespace PhysicsEngine.Models {
    public class BaseObject {
        public Vector2 CurrentPosition;
        public Vector2 Acceleration;
        public bool IsFixedPoint = false;
        public Color Color;
        public int Radius;

        public virtual void UpdatePosition(float deltaTime)
        {
            if(IsFixedPoint) return;
            NaNOrOutOfBoundsCheck();
            CurrentPosition = CurrentPosition + Acceleration * deltaTime * deltaTime;
            //Console.WriteLine($"{CurrentPosition} | {PreviousPosition} | {velocity}"); // For logging
        }

        public virtual void Accelerate(Vector2 acc)
        {
            Acceleration += acc;
        }

        public virtual void AccelerateTowards(Vector2 pos, float force)
        {
            var dist = pos - CurrentPosition;
            Acceleration += force * dist/(dist.Length() + .1f);
        }

        public virtual void NaNOrOutOfBoundsCheck()
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