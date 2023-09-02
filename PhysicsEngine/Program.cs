using PhysicsEngine.Models;
using Raylib_cs;
using System.Numerics;

namespace PhysicsEngine
{
    public class Program
    {
        public const int Width = 1500, Height = 800;
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

            Utils.CreateChain(new Vector2(Width/2, Height/2), new Vector2(Width, Height), 3, false);

            PhysicsEngine.AddAttractionRule(Color.RED, Color.BLUE, 1500);
            PhysicsEngine.AddAttractionRule(Color.RED, Color.RED, 500);
            PhysicsEngine.AddAttractionRule(Color.BLUE, Color.BLUE, 2400);
            PhysicsEngine.AddAttractionRule(Color.BLUE, Color.RED, -1300);


            GraphicsManager.Init(Width, Height); // Initialize Program

        }
    }

}