using System;
using System.Collections.Generic;
using UnityEngine;

namespace Movement
{
    /// <summary>
    /// This class interfaces with rigidBody to control a character's movement through forces
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class CharacterBody : MonoBehaviour
    {
        [SerializeField] private float brakeMultiplier = 1;
        [SerializeField] private bool enableLog = true;
        private Rigidbody _rigidbody;
        private MovementRequest _currentMovement = MovementRequest.InvalidRequest;
        private bool _isBrakeRequested = false;
        private readonly List<ImpulseRequest> _impulseRequests = new();

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
                Break();

            ManageMovement();
            ManageImpulseRequests();
        }

        public void SetMovement(MovementRequest movementRequest)
        {
            _currentMovement = movementRequest;
        }

        public void RequestBrake()
        {
            _isBrakeRequested = true;
        }

        public void RequestImpulse(ImpulseRequest request)
        {
            _impulseRequests.Add(request);
        }

        private void Break()
        {
            _rigidbody.AddForce(-_rigidbody.velocity * brakeMultiplier, ForceMode.Impulse);
            _isBrakeRequested = false;
            if (enableLog)
                Debug.Log($"{name}: Brake processed.");
        }

        private void ManageMovement()
        {
            var velocity = _rigidbody.velocity;
            velocity.y = 0;
            if (!_currentMovement.IsValid()
                || velocity.magnitude >= _currentMovement.GoalSpeed)
                return;
            _rigidbody.AddForce(_currentMovement.GetAccelerationVector(), ForceMode.Force);
        }

        private void ManageImpulseRequests()
        {
            foreach (var request in _impulseRequests)
            {
                _rigidbody.AddForce(request.GetForceVector(), ForceMode.Impulse);
            }
            _impulseRequests.Clear();
        }
    }
}
