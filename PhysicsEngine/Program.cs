using PhysicsEngine.Models;
using Raylib_cs;
using System.Numerics;

namespace PhysicsEngine
{
    public class Program
    {
        public const int Width = 900, Height = 600;
        private static void Main(string[] args)
        {
            //Starting Physics Engine settings
            PhysicsEngine.enableCollisions = true;
            PhysicsEngine.useGravity = false;

            //Populate simulation
            var rand = new Random();
            
            var widthScaled = Width - 100;
            var heightScaled = Height - 100;
            var widthOffset = Width - widthScaled;
            var heightOffset = Height - heightScaled;
            for (int i = 0; i < 50; i++)
            {
                Vector2 position = new Vector2(rand.Next(widthScaled) + widthOffset, rand.Next(heightScaled) + heightOffset);
                int radius = 5;
                Color color = Color.RED;
                PhysicsEngine.InstantiateParticle(position, radius, color);
            }

            for (int i = 0; i < 50; i++)
            {
                Vector2 position = new Vector2(rand.Next(widthScaled) + widthOffset, rand.Next(heightScaled) + heightOffset);
                int radius = 5;
                Color color = Color.BLUE;
                PhysicsEngine.InstantiateParticle(position, radius, color);
            }

            // Utils.CreateChain(new Vector2(400, 250),
            //                   new Vector2(600, 250), 5);
            PhysicsEngine.AttractionRules.Add(new AttractionRule(Color.RED, Color.BLUE, 1500));
            PhysicsEngine.AttractionRules.Add(new AttractionRule(Color.RED, Color.RED, 500));
            PhysicsEngine.AttractionRules.Add(new AttractionRule(Color.BLUE, Color.BLUE, 2400));
            PhysicsEngine.AttractionRules.Add(new AttractionRule(Color.BLUE, Color.RED, -1300));

            PhysicsEngine.Space = new Vector2(Width, Height);
            GraphicsManager.Init(Width, Height); // Initialize Program

        }
    }

}