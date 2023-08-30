using System.Numerics;
using Raylib_cs;

namespace PhysicsEngine.Models
{
    public static class Utils
    {
        public static bool IsSameColor(Color a, Color b)
            => (a.r == b.r && a.g == b.g && a.b == b.b);
        public static void CreateChain(Vector2 a, Vector2 b, int linkRadius)
        {
            Vector2 dir = (a - b);
            var dist = dir.Length();
            Vector2 dirNormal = dir/dist;
            var amountOfObjects = Math.Floor(dist/(linkRadius * 2));
            List<ChainObject> objs = new();
            for(int i = 0; i < amountOfObjects; i++)
            {
                Vector2 pos = a + dirNormal * i * linkRadius * 2;
                ChainObject obj = new ChainObject(pos, linkRadius, Color.DARKPURPLE);
                objs.Add(obj);
                if(i == 0) {obj.IsFixedPoint = true; obj.Links[0] = obj;}
                else obj.Links[0] = objs[i - 1];
                PhysicsEngine.Objects.Add(obj);
            }
            Console.WriteLine($"Created Chain with {amountOfObjects} objects");
        }
    }
}
