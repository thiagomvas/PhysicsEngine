using PhysicsEngine.Models;
using Raylib_cs;
using System.Numerics;

namespace PhysicsEngine
{
    public class Program
    {
        private static void Main(string[] args)
        {
            //Starting Physics Engine settings
            PhysicsEngine.enableCollisions = false;
            PhysicsEngine.useGravity = false;

            //Populate simulation
            var rand = new Random();
            for (int i = 0; i < 250; i++)
            {
                Vector2 position = new Vector2(rand.Next(1000) + 100, rand.Next(600) + 100);
                float radius = 5;
                Color color = Color.RED;
                PhysicsEngine.InstantiateParticle(position, radius, color);
            }
            for (int i = 0; i < 250; i++)
            {
                Vector2 position = new Vector2(rand.Next(1000) + 100, rand.Next(600) + 100);
                float radius = 5;
                Color color = Color.BLUE;
                PhysicsEngine.InstantiateParticle(position, radius, color);
            }

            PhysicsEngine.AttractionRules.Add(new AttractionRule(Color.RED, Color.BLUE, 7));
            PhysicsEngine.AttractionRules.Add(new AttractionRule(Color.RED, Color.RED, 5));
            PhysicsEngine.AttractionRules.Add(new AttractionRule(Color.BLUE, Color.BLUE, 24));
            PhysicsEngine.AttractionRules.Add(new AttractionRule(Color.BLUE, Color.RED, -13));


            GraphicsManager.Init(); // Initialize Program

        }
    }

}