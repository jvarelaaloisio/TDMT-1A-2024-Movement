using UnityEngine;

namespace CameraControl
{
    public class FollowTarget : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 offset = new(0, 3.5f, -5f);
        [SerializeField] private bool shouldSetOffsetInAwake;
        [SerializeField] private float speed = 10;
        public float rotationSpeed = 5;
        [SerializeField] private float minimumDistanceToRotate = .1f;

        private void Awake()
        {
            if (!target)
            {
                Debug.LogError($"{name}: Target is null!");
                enabled = false;
                return;
            }

            if (shouldSetOffsetInAwake)
            {
                offset = target.position - transform.position;
            }
        }

        private void FixedUpdate()
        {
            var offsetPosition = target.TransformPoint(offset);
            transform.position = Vector3.Lerp(transform.position, offsetPosition, Time.deltaTime * speed);
            //-----
            
            // var directionToTarget = target.TransformPoint(offset) - transform.position;
            // transform.position += directionToTarget * (speed * Time.fixedDeltaTime);
            if (Vector3.Distance(transform.position, offsetPosition) < minimumDistanceToRotate)
                return;
            
            var desiredRotation = target.rotation * Quaternion.Euler(transform.rotation.eulerAngles.x, 0, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * rotationSpeed);
        }
    }
}