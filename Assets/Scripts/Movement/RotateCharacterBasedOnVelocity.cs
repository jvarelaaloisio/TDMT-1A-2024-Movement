using UnityEngine;

namespace Movement
{
    public class RotateCharacterBasedOnVelocity : MonoBehaviour
    {
        [SerializeField] private Rigidbody rigidBody;
        [SerializeField] private float rotationSpeed = 1;
        [SerializeField] private float minimumSpeedForRotation = 0.001f;

        private void Update()
        {
            var velocity = rigidBody.velocity;
            velocity.y = 0;
            if (velocity.magnitude < minimumSpeedForRotation)
                return;
            //The angle is always positive, we need to decide whether we need it to be negative or not
            var rotationAngle = Vector3.SignedAngle(transform.forward, velocity, Vector3.up);
            transform.Rotate(Vector3.up, rotationAngle * rotationSpeed * Time.deltaTime);
        }
    }
}