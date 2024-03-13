using System;
using Inputs;
using UnityEngine;

namespace Movement
{
    public class ControllerBrain : MonoBehaviour
    {
        [SerializeField] private CharacterBody body;
        [SerializeField] private InputReader inputReader;
        private Vector3 _desiredDirection;
        [SerializeField] private float speed = 10;
        [SerializeField] private float acceleration = 4;

        private void OnEnable()
        {
            if (body == null)
            {
                Debug.LogError($"{name}: {nameof(body)} is null!" +
                               $"\nDisabling object to avoid errors.");
                enabled = false;
                return;
            }
            inputReader.onMovementInput += HandleMovementInput;
        }
        private void OnDisable()
        {
            inputReader.onMovementInput -= HandleMovementInput;
        }

        private void HandleMovementInput(Vector2 input)
        {
            if (_desiredDirection.magnitude > .001f
                && input.magnitude < .001f)
            {
                body.RequestBrake();
            }

            _desiredDirection = new Vector3(input.x, 0, input.y);
            body.SetMovement(new MovementRequest(_desiredDirection, speed, acceleration));
        }
    }
}
