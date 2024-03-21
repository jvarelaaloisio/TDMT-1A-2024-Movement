using UnityEngine;

namespace Movement
{
    public readonly struct ImpulseRequest
    {
        public readonly Vector3 Direction;
        public readonly float Force;

        public ImpulseRequest(Vector3 direction, float force)
        {
            Direction = direction;
            Force = force;
        }

        public Vector3 GetForceVector() => Force * Direction;
    }
}