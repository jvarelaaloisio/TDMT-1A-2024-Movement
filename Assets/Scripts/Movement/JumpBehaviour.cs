using System;
using UnityEngine;

namespace Movement
{
    public class JumpBehaviour : MonoBehaviour
    {
        [SerializeField] private CharacterBody body;
        [SerializeField] private float jumpForce = 10;
        [SerializeField] private int maxJumpQty = 1;
        private int _currentJumpQty = 0;
        [SerializeField] private float floorAngle = 30;
        [SerializeField] private bool enableLog = true;

        public event Action onJump = delegate { };
        public event Action onLand = delegate { };
        private void Reset()
        {
            body = GetComponent<CharacterBody>();
        }

        public bool TryJump()
        {
            if (_currentJumpQty >= maxJumpQty)
            {
                return false;
            }

            if (enableLog)
                Debug.Log($"{name}: jumped!");
            _currentJumpQty++;
            body.RequestImpulse(new ImpulseRequest(Vector3.up, jumpForce));
            onJump.Invoke();
            return true;
        }

        private void OnCollisionEnter(Collision collision)
        {
            var contact = collision.contacts[0];
            var contactAngle = Vector3.Angle(contact.normal, Vector3.up);
            if (contactAngle <= floorAngle)
            {
                _currentJumpQty = 0;
                if (enableLog)
                    Debug.Log($"{name}: jump count reset!");
                onLand.Invoke();
            }

            if (enableLog)
                Debug.Log($"{name}: Collided with normal angle: {contactAngle}");
        }
    }
}
