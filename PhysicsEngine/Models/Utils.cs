using Raylib_cs;

namespace PhysicsEngine.Models
{
    public static class Utils
    {
        public static bool IsSameColor(Color a, Color b)
            => (a.r == b.r && a.g == b.g && a.b == b.b);
    }
}
