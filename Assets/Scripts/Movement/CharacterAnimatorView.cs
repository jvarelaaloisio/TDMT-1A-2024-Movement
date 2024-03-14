using UnityEngine;

namespace Movement
{
    public class CharacterAnimatorView : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private Rigidbody rigidBody;
        [SerializeField] private string horSpeedParameter = "hor_speed";
        [SerializeField] private float rotationSpeed = 1;
        
        private void Update()
        {
            var velocity = rigidBody.velocity;
            velocity.y = 0;
            var speed = velocity.magnitude;
            animator.SetFloat(horSpeedParameter, speed);
            //This is probably wrong
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(velocity), speed);
        }
    }
}
