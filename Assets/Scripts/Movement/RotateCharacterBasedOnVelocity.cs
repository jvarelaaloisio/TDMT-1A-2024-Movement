using System;
using UnityEngine;

namespace Movement
{
    public class RotateCharacterBasedOnVelocity : MonoBehaviour
    {
        [SerializeField] private Rigidbody rigidBody;
        [SerializeField] private float rotationSpeed = 1;

        private void Update()
        {
            var velocity = rigidBody.velocity;
            if (velocity.magnitude < Mathf.Epsilon)
                return;
            //The angle is always positive, we need to decide whether we need it to be negative or not
            var rotationAngle = Vector3.SignedAngle(transform.forward, velocity, Vector3.up);
            transform.Rotate(Vector3.up, rotationAngle * rotationSpeed * Time.deltaTime);
        }
    }
}
