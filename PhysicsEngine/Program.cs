using PhysicsEngine.Models;
using Raylib_cs;
using System.Numerics;

namespace PhysicsEngine
{
    public class Program
    {
        public const int Width = 1100, Height = 800;
        private static void Main(string[] args)
        {
            //Starting Physics Engine settings
            PhysicsEngine.enableCollisions = true;
            PhysicsEngine.useGravity = true;

            //Populate simulation
            var rand = new Random();
            
            var widthScaled = Width - 100;
            var heightScaled = Height - 100;
            var widthOffset = Width - widthScaled;
            var heightOffset = Height - heightScaled;

             Utils.CreateChain(new Vector2(350, 350),
                               new Vector2(800, 250), 5);
            PhysicsEngine.AttractionRules.Add(new AttractionRule(Color.RED, Color.BLUE, 1500));
            PhysicsEngine.AttractionRules.Add(new AttractionRule(Color.RED, Color.RED, 500));
            PhysicsEngine.AttractionRules.Add(new AttractionRule(Color.BLUE, Color.BLUE, 2400));
            PhysicsEngine.AttractionRules.Add(new AttractionRule(Color.BLUE, Color.RED, -1300));

            PhysicsEngine.Space = new Vector2(Width, Height);
            GraphicsManager.Init(Width, Height); // Initialize Program

        }
    }

}