using PhysicsEngine.Models;
using Raylib_cs;

namespace PhysicsEngine
{
    public static class GraphicsManager
    {
        public static void Init(int Width = 1200, int Height = 900)
        {
            Raylib.InitWindow(Width, Height, "Physics Engine Test");
            Raylib.SetTargetFPS(60);

            while (!Raylib.WindowShouldClose()) // While window is running
            {
                PhysicsEngine.Update(Raylib.GetFrameTime()); // Trigger Physics Update every frame redraw
                InputManager.CheckInput();                   // Checks for input

                Raylib.BeginDrawing();

                Raylib.ClearBackground(new Color(5, 5, 5, 255));
                Raylib.DrawRectangle((int)PhysicsEngine.WorldBorder.X, 
                                     (int)PhysicsEngine.WorldBorder.Y, 
                                     Raylib.GetScreenWidth() - (int)PhysicsEngine.WorldBorder.X * 2, 
                                     Raylib.GetScreenHeight() - (int)PhysicsEngine.WorldBorder.Y * 2, 
                                     Color.BLACK);


                foreach (BaseObject obj in PhysicsEngine.Objects)
                {
                    DrawObject(obj);
                }

                Raylib.EndDrawing();
            }
        }
        public static void DrawObject(BaseObject obj) => Raylib.DrawCircleV(obj.CurrentPosition, obj.Radius, obj.Color);

    }
}
