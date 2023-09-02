namespace PhysicsEngine.Models
{
    public class ChainLink
    {
        public VerletObject[] Links = new VerletObject[2];

        public void Solve()
        {
            VerletObject obj1 = Links[0], obj2 = Links[1];
            var dir = (obj2.CurrentPosition - obj1.CurrentPosition);
            var dist = dir.Length();
            var dirnormal = dir / dist;

            if (dist > (obj1.Radius + obj2.Radius))
            {
                var realDist = dist - (obj1.Radius + obj2.Radius);
                if (!obj1.IsFixedPoint) obj1.CurrentPosition += dirnormal * realDist * 0.5f;
                if (!obj2.IsFixedPoint) obj2.CurrentPosition += -dirnormal * realDist * 0.5f;
            }
        }
    }
}
