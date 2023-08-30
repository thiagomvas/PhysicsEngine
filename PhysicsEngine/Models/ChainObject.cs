using System.Numerics;
using Raylib_cs;

namespace PhysicsEngine.Models{
    public class ChainObject : BaseObject {
        public ChainObject[] Links = new ChainObject[2];

        public ChainObject(Vector2 position, int radius, Color color) {
            CurrentPosition = position;
            Color = color;
            Radius = radius;
        }
        public override void UpdatePosition(float deltaTime)
        {
            if(IsFixedPoint) return;
            base.UpdatePosition(deltaTime);
            if(Links[0] == null) return;
            var dir = (Links[0].CurrentPosition - CurrentPosition);
            var dist = dir.Length();
                Vector2 normal = dir/dist;
                var realDist = dist - Radius * 2;
                CurrentPosition += normal * realDist;
            
        }
    }
}