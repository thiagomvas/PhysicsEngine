using Raylib_cs;

namespace PhysicsEngine.Models
{
    public class AttractionRule
    {
        public Color ColorGroup1;
        public Color ColorGroup2;
        public float Force;

        public AttractionRule(Color colorGroup1, Color colorGroup2, float force)
        {
            ColorGroup1 = colorGroup1;
            ColorGroup2 = colorGroup2;
            Force = force;
        }
    }
}
