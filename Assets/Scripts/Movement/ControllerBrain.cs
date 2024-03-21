using Inputs;
using UnityEngine;

namespace Movement
{
    public class ControllerBrain : MonoBehaviour
    {
        [SerializeField] private CharacterBody body;
        [SerializeField] private InputReader inputReader;
        [SerializeField] private JumpBehaviour jumpBehaviour;
        [SerializeField] private float speed = 10;
        [SerializeField] private float acceleration = 4;
        [SerializeField] private Transform cameraTransform;
        [SerializeField] private bool enableLog = true;
        private Vector3 _desiredDirection;

        private void Awake()
        {
            if (!cameraTransform && Camera.main)
                cameraTransform = Camera.main.transform;

            if (!cameraTransform)
                Debug.LogError($"{name}: {nameof(cameraTransform)} is null!");
        }

        private void OnEnable()
        {
            if (body == null)
            {
                Debug.LogError($"{name}: {nameof(body)} is null!" +
                               $"\nDisabling object to avoid errors.");
                enabled = false;
                return;
            }

            if (!inputReader)
            {
                Debug.LogError($"{name}: {nameof(inputReader)} is null!");
                return;
            }
            inputReader.onMovementInput += HandleMovementInput;
            inputReader.onJumpInput += HandleJumpInput;
        }
        private void OnDisable()
        {
            if (!inputReader)
                return;
            inputReader.onMovementInput -= HandleMovementInput;
            inputReader.onJumpInput -= HandleJumpInput;
        }

        private void HandleMovementInput(Vector2 input)
        {
            if (_desiredDirection.magnitude > Mathf.Epsilon
                && input.magnitude < Mathf.Epsilon)
            {
                if (enableLog)
                {
                    Debug.Log($"{nameof(_desiredDirection)} magnitude: {_desiredDirection.magnitude}\t{nameof(input)} magnitude: {input.magnitude}");
                }
                body.RequestBrake();
            }

            _desiredDirection = new Vector3(input.x, 0, input.y);
            if (cameraTransform)
            {
                _desiredDirection = cameraTransform.TransformDirection(_desiredDirection);
                _desiredDirection.y = 0;
            }
            body.SetMovement(new MovementRequest(_desiredDirection, speed, acceleration));
        }

        private void HandleJumpInput()
        {
            jumpBehaviour.TryJump();
        }
    }
}
