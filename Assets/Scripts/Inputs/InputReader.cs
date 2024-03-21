using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Inputs
{
    public class InputReader : MonoBehaviour
    {
        public event Action<Vector2> onMovementInput = delegate { };
        public event Action onJumpInput = delegate { };
        public void HandleMovementInput(InputAction.CallbackContext ctx)
        {
            onMovementInput.Invoke(ctx.ReadValue<Vector2>());
        }
        public void HandleJumpInput(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
                onJumpInput.Invoke();
        }
    }
}
