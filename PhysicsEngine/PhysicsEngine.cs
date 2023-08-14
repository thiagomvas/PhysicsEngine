using PhysicsEngine.Models;
using Raylib_cs;
using System.Numerics;


namespace PhysicsEngine
{
    public static class PhysicsEngine
    {
        // General Settings
        public static Vector2 Gravity = new Vector2(0, 1000);
        public static Vector2 Space = new Vector2(1200, 900);
        public static Vector2 WorldBorder = new Vector2(25, 25);
        public static List<VerletObject> Objects = new();
        public static float repelAttractForce = 200;

        // Toggles
        public static bool useGravity = true;
        public static bool useConstraints = true;
        public static bool enableCollisions = true;
        public static bool attractToMouse = false;
        public static bool repelFromMouse = false;
        public static void Update(float deltaTime)
        {
            if (useGravity) ApplyGravity();
            if (useConstraints) ApplyConstraints(deltaTime);
            if (enableCollisions) SolveColisions(deltaTime);
            if (attractToMouse) AttractParticles(repelAttractForce);
            if (repelFromMouse) AttractParticles(-repelAttractForce);
            UpdatePositions(deltaTime);
        }

        private static void AttractParticles(float force)
        {
            foreach (VerletObject obj in Objects)
                obj.AccelerateTowards(Raylib.GetMousePosition(), force);
        }

        private static void ApplyGravity()
        {
            foreach(VerletObject obj in Objects)           
                obj.Accelerate(Gravity);
        }

        private static void UpdatePositions(float deltaTime)
        {
            foreach (VerletObject obj in Objects)
                obj.UpdatePosition(deltaTime);
        }

        private static void ApplyConstraints(float deltaTime)
        {
            foreach(VerletObject obj in Objects)
            {
                Vector2 pos = obj.CurrentPosition;
                obj.CurrentPosition = new Vector2(Math.Clamp(obj.CurrentPosition.X, WorldBorder.X + obj.Radius, Space.X - WorldBorder.X - obj.Radius),
                    Math.Clamp(obj.CurrentPosition.Y, WorldBorder.Y + obj.Radius, Space.Y - WorldBorder.Y - obj.Radius));
                
            }
        }

        private static void SolveColisions(float deltaTime)
        {
            foreach(VerletObject obj in Objects)
            {
                foreach(VerletObject obj2 in Objects)
                {
                    if (obj == obj2) continue;
                    Vector2 col = obj.CurrentPosition - obj2.CurrentPosition;
                    var dist = col.Length();
                    if (dist < obj.Radius + obj2.Radius) 
                    {
                        Vector2 n = col / dist;
                        var delta = obj.Radius + obj2.Radius - dist;
                        //Console.WriteLine($"{obj.CurrentPosition} {obj.CurrentPosition} |{col} | {dist} | {n} | {delta}");
                        obj.CurrentPosition += 0.5f * delta * n;
                        obj2.CurrentPosition -= 0.5f * delta * n;
                    }
                }
            }
        }

        public static Vector2 AttractionForce(VerletObject particle1, VerletObject particle2, float acceleration)
        {
            var dist = (particle1.CurrentPosition - particle2.CurrentPosition).Length();
            if (dist <= 10) return Vector2.Zero;
            var force = acceleration / dist;

            var dir = (particle2.CurrentPosition - particle1.CurrentPosition) / dist;

            var attraction = dir * force;

            return attraction;
        }

        public static void InstantiateRandomParticle(Vector2? pos = null)
        {
            var rand = new Random();
            var position = pos ?? new Vector2(Raylib.GetScreenWidth() / 2 + rand.Next(200) - 100, 100);
            var obj = new VerletObject(position,
                rand.Next(10) + 5,
                new Color(rand.Next(255), rand.Next(255), rand.Next(255), 255));
            Objects.Add(obj);
        }


    }
}
