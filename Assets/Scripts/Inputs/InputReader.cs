using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Inputs
{
    public class InputReader : MonoBehaviour
    {
        public event Action<Vector2> onMovementInput = delegate { };
        public void HandleMovementInput(InputAction.CallbackContext ctx)
        {
            onMovementInput.Invoke(ctx.ReadValue<Vector2>());
        }
    }
}
