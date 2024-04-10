using System;
using System.Collections;
using Movement.Jump;
using UnityEngine;

namespace Movement
{
    public class CharacterAnimatorView : MonoBehaviour
    {
        [Header("References")] [SerializeField]
        private Animator animator;

        [SerializeField] private Rigidbody rigidBody;
        [SerializeField] private JumpBehaviour jumpBehaviour;
        [SerializeField] private CharacterBody body;

        [Header("Animator Parameters")] [SerializeField]
        private string jumpTriggerParameter = "jump";

        [SerializeField] private string isFallingParameter = "is_falling";
        [SerializeField] private string horSpeedParameter = "hor_speed";
        [SerializeField] private float spinSpeed = 1f;
        [SerializeField] private Transform spinPivot;
        [SerializeField] private float spinResetDuration = .25f;
        private Vector3 _defaultLocalPosition;
        private Quaternion _defaultLocalRotation;

        private void OnEnable()
        {
            transform.GetLocalPositionAndRotation(out _defaultLocalPosition, out _defaultLocalRotation);
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
            if (jumpBehaviour.CurrentJumpQty > 1)
            {
                StartCoroutine(SpinCharacter());
            }
        }

        private IEnumerator SpinCharacter()
        {
            while (!destroyCancellationToken.IsCancellationRequested
                   && body.IsFalling)
            {
                transform.RotateAround(spinPivot.position, spinPivot.right, Time.deltaTime * spinSpeed);
                yield return null;
            }
            transform.SetLocalPositionAndRotation(_defaultLocalPosition, _defaultLocalRotation);
        }
    }
}