using System.Numerics;
using Raylib_cs;

namespace PhysicsEngine.Models
{
    public static class Utils
    {
        private static Vector2 startChainPos;
        private static bool chainStarted;
        public static bool IsSameColor(Color a, Color b)
            => (a.r == b.r && a.g == b.g && a.b == b.b);

        /// <summary>
        /// Instantiates a chain connecting from point A to B
        /// </summary>
        /// <param name="a">Starting Position</param>
        /// <param name="b">End Position</param>
        /// <param name="linkRadius">The radius of each object that makes up the chain</param>
        /// <param name="linkDist">The target distance between each link, in other words, how "stiff" it should be</param>
        /// <param name="bothEndsFixed">Whether or not it has an anchor point at the end of the chain or not</param>
        public static void InstantiateChain(Vector2 a, Vector2 b, float linkRadius, float linkDist, bool bothEndsFixed = true)
        {

            if(!bothEndsFixed) linkDist = Math.Max(2 * linkRadius, linkDist);

            Console.WriteLine(linkDist);

            Vector2 dir = (b - a);
            var dist = dir.Length();
            Vector2 dirNormal = dir/dist;
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
                link.targetDist = linkDist;
                links.Add(link);
                PhysicsEngine.ChainLinks.Add(link);
            }

            Console.WriteLine($"Created Chain with {amountOfObjects} objects");
            chainStarted = false;
        }

        public static void StartDrawingChain()
        {
            chainStarted = true;
            startChainPos = Raylib.GetMousePosition();
        }

        public static void StopDrawingChain(bool bothendsfixed = true)
        {
            if (chainStarted) InstantiateChain(startChainPos, Raylib.GetMousePosition(), 10, 10, true); ;
        }
    }
}
