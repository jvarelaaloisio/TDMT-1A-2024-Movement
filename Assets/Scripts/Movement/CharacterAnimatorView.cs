using System;
using UnityEngine;

namespace Movement
{
    public class CharacterAnimatorView : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Animator animator;
        [SerializeField] private Rigidbody rigidBody;
        [SerializeField] private JumpBehaviour jumpBehaviour;
        [SerializeField] private CharacterBody body;
        
        [Header("Animator Parameters")]
        [SerializeField] private string jumpTriggerParameter = "jump";
        [SerializeField] private string isFallingParameter = "is_falling";
        [SerializeField] private string horSpeedParameter = "hor_speed";

        private void OnEnable()
        {
            if (jumpBehaviour)
            {
                jumpBehaviour.onJump += HandleJump;
            }
        }

        private void OnDisable()
        {
            if (jumpBehaviour)
            {
                jumpBehaviour.onJump -= HandleJump;
            }
        }

        private void Update()
        {
            if (!rigidBody)
                return;
            var velocity = rigidBody.velocity;
            velocity.y = 0;
            var speed = velocity.magnitude;
            if (animator)
                animator.SetFloat(horSpeedParameter, speed);
            if (animator && body)
            {
                animator.SetBool(isFallingParameter, body.IsFalling);
            }
        }

        private void HandleJump()
        {
            if (animator)
            {
                animator.SetTrigger(jumpTriggerParameter);
            }
        }
    }
}
