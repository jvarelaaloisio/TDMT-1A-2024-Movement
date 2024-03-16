using UnityEngine;

namespace Movement
{
    public class CharacterAnimatorView : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private Rigidbody rigidBody;
        [SerializeField] private string horSpeedParameter = "hor_speed";
        
        private void Update()
        {
            var velocity = rigidBody.velocity;
            velocity.y = 0;
            var speed = velocity.magnitude;
            animator.SetFloat(horSpeedParameter, speed);
        }
    }
}
