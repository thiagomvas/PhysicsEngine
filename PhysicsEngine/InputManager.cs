using Raylib_cs;

namespace PhysicsEngine
{
    public static class InputManager
    {
        public static void CheckInput()
        {
            //Check for keyboard key pressed
            switch((KeyboardKey) Raylib.GetKeyPressed())
            {
                case KeyboardKey.KEY_G: 
                    PhysicsEngine.useGravity = !PhysicsEngine.useGravity;
                    break;
                case KeyboardKey.KEY_C:
                    PhysicsEngine.enableCollisions = !PhysicsEngine.enableCollisions;
                    break;
            }

            if (Raylib.IsKeyDown(KeyboardKey.KEY_SPACE)) PhysicsEngine.InstantiateRandomParticle();

            PhysicsEngine.attractToMouse = Raylib.IsMouseButtonDown(MouseButton.MOUSE_BUTTON_LEFT);
            PhysicsEngine.repelFromMouse = Raylib.IsMouseButtonDown(MouseButton.MOUSE_BUTTON_RIGHT);
        }
    }
}
