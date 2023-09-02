using System.Numerics;
using Raylib_cs;

namespace PhysicsEngine.Models
{
    public static class Utils
    {
        public static bool IsSameColor(Color a, Color b)
            => (a.r == b.r && a.g == b.g && a.b == b.b);
        public static void CreateChain(Vector2 a, Vector2 b, float linkRadius, bool bothEndsFixed = true)
        {
            Vector2 dir = (b - a);
            var dist = dir.Length();
            Vector2 dirNormal = dir/dist;
            var decimals = dist/(linkRadius * 2) % 1;
            linkRadius *= decimals + 1;
            var amountOfObjects = Math.Round(dist/(linkRadius * 2)) + 2;
            List<ChainLink> links = new();
            List<VerletObject> objs = new();
            for (int i = 0; i < amountOfObjects; i++)
            {
                Vector2 pos = a + dirNormal * i * linkRadius * 2;
                VerletObject obj = new VerletObject(pos, linkRadius, Color.DARKPURPLE);
                if (i == 0) obj.IsFixedPoint = true;
                else if (bothEndsFixed && i == amountOfObjects - 1) obj.IsFixedPoint = true;
                objs.Add(obj);
                PhysicsEngine.Objects.Add(obj);
            }

            for(int i = 0; i <  objs.Count; i++)
            {
                if (i + 1 == objs.Count) break;
                ChainLink link = new ChainLink();
                link.Links[0] = objs[i];
                link.Links[1] = objs[i+1];
                links.Add(link);
                PhysicsEngine.ChainLinks.Add(link);
            }

            Console.WriteLine($"Created Chain with {amountOfObjects} objects");
        }
    }
}
