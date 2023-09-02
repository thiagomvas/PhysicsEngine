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
        public static List<BaseObject> Objects = new();
        public static List<AttractionRule> AttractionRules = new();
        public static List<ChainLink> ChainLinks = new();
        public static float repelAttractForce = 200;

        // Toggles
        public static bool useGravity = true;
        public static bool useConstraints = true;
        public static bool enableCollisions = true;
        public static bool repelAwayFromWalls = true;
        public static bool attractToMouse = false;
        public static bool repelFromMouse = false;
        public static void Update(float deltaTime)
        {
            if (useGravity) ApplyGravity();
            if (useConstraints) ApplyConstraints();
            if (enableCollisions) SolveColisions();
            if (attractToMouse) AttractParticlesToMouse(repelAttractForce);
            if (repelFromMouse) AttractParticlesToMouse(-repelAttractForce);
            ApplyRules();
            UpdatePositions(deltaTime);
            SolveLinks();
        }

        private static void ApplyRules()
        {
            foreach (BaseObject obj in Objects)
            {
                foreach (BaseObject obj2 in Objects)
                {
                    if (obj == obj2) continue;
                    AttractionRule? rule = AttractionRules.Find((x) => Utils.IsSameColor(obj.Color, x.ColorGroup1)
                                                                   && Utils.IsSameColor(obj2.Color, x.ColorGroup2));
                    if (rule == null) continue;
                    var f = AttractionForce(obj, obj2, rule.Force);
                    obj.Accelerate(f);
                }
            }
        }
        private static void AttractParticlesToMouse(float force)
        {
            foreach (BaseObject obj in Objects)
                obj.AccelerateTowards(Raylib.GetMousePosition(), force);
        }

        private static void ApplyGravity()
        {
            foreach (BaseObject obj in Objects)
                obj.Accelerate(Gravity);
        }

        private static void UpdatePositions(float deltaTime)
        {
            foreach (BaseObject obj in Objects)
                obj.UpdatePosition(deltaTime);
        }

        private static void ApplyConstraints()
        {
            foreach (BaseObject obj in Objects)
            {
                Vector2 pos = obj.CurrentPosition;
                
                obj.CurrentPosition = new Vector2(Math.Clamp(obj.CurrentPosition.X, WorldBorder.X + obj.Radius, Space.X - WorldBorder.X - obj.Radius),
                    Math.Clamp(obj.CurrentPosition.Y, WorldBorder.Y + obj.Radius, Space.Y - WorldBorder.Y - obj.Radius));
                var delta = Space - pos;
                Vector2 accel = Vector2.Zero;
                if (repelAwayFromWalls)
                {
                    if (delta.X < WorldBorder.X * 2) accel.X = -250;
                    else if (delta.X > Space.X - WorldBorder.X * 2) accel.X = 250;
                    if (delta.Y < WorldBorder.Y * 2) accel.Y = -250;
                    else if (delta.Y > Space.Y - WorldBorder.Y * 2) accel.Y = 250;
                }

                obj.Accelerate(accel);
            }
        }

        private static void SolveColisions()
        {
            foreach (VerletObject obj in Objects)
            {
                foreach (VerletObject obj2 in Objects)
                {
                    if (obj == obj2) continue;
                    Vector2 col = obj.CurrentPosition - obj2.CurrentPosition;
                    var dist = col.Length();
                    if (dist < obj.Radius + obj2.Radius)
                    {
                        Vector2 n = col / (dist + .1f);
                        var delta = obj.Radius + obj2.Radius - dist;
                        //Console.WriteLine($"{obj.CurrentPosition} {obj.CurrentPosition} |{col} | {dist} | {n} | {delta}");
                        if(!obj.IsFixedPoint)obj.CurrentPosition += 0.5f * delta * n;
                        obj2.CurrentPosition -= 0.5f * delta * n;
                    }
                }
            }
        }

        private static void SolveLinks()
        {
            foreach (ChainLink link in ChainLinks) link.Solve();
        }
        public static Vector2 AttractionForce(BaseObject particle1, BaseObject particle2, float acceleration)
        {
            var dist = (particle1.CurrentPosition - particle2.CurrentPosition).Length();
            if (dist <= 10) return Vector2.Zero;
            var force = acceleration / (dist + .1f);

            var dir = (particle2.CurrentPosition - particle1.CurrentPosition) / (dist + .1f);

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
        public static void InstantiateParticle(Vector2 pos, int radius, Color color)
        {
            var rand = new Random();
            var obj = new VerletObject(pos, radius, color);
            Objects.Add(obj);
        }

        public static void AddAttractionRule(Color a, Color b, float force) => AttractionRules.Add(new AttractionRule(a, b, force));
    }
}
