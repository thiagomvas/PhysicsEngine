using PhysicsEngine.Models;
using Raylib_cs;

namespace PhysicsEngine
{
    public static class InputManager
    {
        public enum MouseControlMode { Attraction , CreateRope , SpawnParticles }
        public static MouseControlMode mouseMode = MouseControlMode.SpawnParticles;
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
                case KeyboardKey.KEY_ONE:
                    mouseMode = MouseControlMode.Attraction;
                    break;
                case KeyboardKey.KEY_TWO: 
                    mouseMode = MouseControlMode.CreateRope; 
                    break;
                case KeyboardKey.KEY_THREE:
                    mouseMode = MouseControlMode.SpawnParticles;
                    break;
            }

            if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT))
            {
                switch (mouseMode)
                {
                    case MouseControlMode.CreateRope:
                        Utils.StartDrawingChain();
                        break;
                }
            }
            if (Raylib.IsMouseButtonUp(MouseButton.MOUSE_BUTTON_LEFT))
            {
                switch (mouseMode)
                {
                    case MouseControlMode.CreateRope:
                        Utils.StopDrawingChain();
                        break;
                }
            }
            if (Raylib.IsMouseButtonDown(MouseButton.MOUSE_BUTTON_LEFT))
            {
                switch (mouseMode)
                {
                    case MouseControlMode.Attraction:
                        PhysicsEngine.AttractParticlesToMouse(PhysicsEngine.repelAttractForce);
                        break;
                    case MouseControlMode.SpawnParticles:
                        PhysicsEngine.InstantiateRandomParticle(Raylib.GetMousePosition());
                        break;
                }
            }

            if (Raylib.IsMouseButtonDown(MouseButton.MOUSE_BUTTON_RIGHT))
            {
                switch (mouseMode)
                {
                    case MouseControlMode.Attraction:
                        PhysicsEngine.AttractParticlesToMouse(-PhysicsEngine.repelAttractForce);
                        break;
                    case MouseControlMode.CreateRope:
                        break;
                    case MouseControlMode.SpawnParticles:
                        PhysicsEngine.InstantiateRandomParticle(Raylib.GetMousePosition());
                        break;
                }
            }

        }
    }
}
