namespace PhysicsEngine.Models
{
    public class ChainLink
    {
        public VerletObject[] Links = new VerletObject[2];
        public float targetDist = 20;

        public void Solve()
        {
            VerletObject obj1 = Links[0], obj2 = Links[1];
            var axis = obj1.CurrentPosition - obj2.CurrentPosition;
            var dist = axis.Length();
            var n = axis/dist;
            var delta = targetDist - dist;
            if (!obj1.IsFixedPoint) obj1.CurrentPosition += 0.5f * delta * n;
            if (!obj2.IsFixedPoint) obj2.CurrentPosition -= 0.5f * delta * n;
        }
    }
}
