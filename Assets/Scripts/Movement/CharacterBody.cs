using System;
using UnityEngine;

namespace Movement
{
    /// <summary>
    /// This class interfaces with rigidBody to control a character's movement through forces
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class CharacterBody : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        private MovementRequest _currentMovement = MovementRequest.InvalidRequest;
        private bool _isBrakeRequested = false;
        [SerializeField] private float brakeMultiplier = 1;

        private void Reset()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnValidate()
        {
            _rigidbody ??= GetComponent<Rigidbody>();
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (_isBrakeRequested)
            {
                _rigidbody.AddForce(-_rigidbody.velocity * brakeMultiplier, ForceMode.Impulse);
                _isBrakeRequested = false;
                Debug.Log($"{name}: Brake!");
            }
            if (!_currentMovement.IsValid()
                || _rigidbody.velocity.magnitude >= _currentMovement.GoalSpeed)
                return;
            _rigidbody.AddForce(_currentMovement.GetAccelerationVector(), ForceMode.Force);
        }

        public void SetMovement(MovementRequest movementRequest)
        {
            _currentMovement = movementRequest;
        }

        public void RequestBrake()
        {
            _isBrakeRequested = true;
        }
    }
}
