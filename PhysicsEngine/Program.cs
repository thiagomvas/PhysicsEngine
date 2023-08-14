using System.Numerics;

namespace PhysicsEngine
{
    public class Program
    {
        private static void Main(string[] args)
        {
            //Starting Physics Engine settings
            PhysicsEngine.enableCollisions = true;
            PhysicsEngine.useGravity = true;

            //Populate simulation
            var rand = new Random();
            for (int i = 0; i < 500; i++)
                PhysicsEngine.InstantiateRandomParticle(new Vector2(rand.Next(1000) + 100, 
                                                                    rand.Next(600) + 100));
            

            GraphicsManager.Init(); // Initialize Program

        }
    }

}