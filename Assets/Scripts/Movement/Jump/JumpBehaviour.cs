using System;
using UnityEngine;

namespace Movement.Jump
{
    public class JumpBehaviour : MonoBehaviour
    {
        [SerializeField] private CharacterBody body;
        [SerializeField] private bool enableLog = true;
        public int CurrentJumpQty { get; private set; } = 0;

        public event Action onJump = delegate { };
        public event Action onLand = delegate { };
        
        public JumpModel Model { get; set; }
        
        private void Reset()
        {
            body = GetComponent<CharacterBody>();
        }

        public bool TryJump()
        {
            if (CurrentJumpQty >= Model.MaxQty)
            {
                return false;
            }

            if (enableLog)
                Debug.Log($"{name}: jumped!");
            CurrentJumpQty++;
            body.RequestImpulse(new ImpulseRequest(Vector3.up, Model.Force));
            onJump.Invoke();
            return true;
        }

        private void OnCollisionEnter(Collision collision)
        {
            var contact = collision.contacts[0];
            var contactAngle = Vector3.Angle(contact.normal, Vector3.up);
            if (contactAngle <= Model.FloorAngle)
            {
                CurrentJumpQty = 0;
                if (enableLog)
                    Debug.Log($"{name}: jump count reset!");
                onLand.Invoke();
            }

            if (enableLog)
                Debug.Log($"{name}: Collided with normal angle: {contactAngle}");
        }
    }
}
