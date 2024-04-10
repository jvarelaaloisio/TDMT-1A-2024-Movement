using UnityEngine;

namespace Movement.Jump
{
    public class CharacterSetup : MonoBehaviour
    {
        [SerializeField] private JumpModelContainer modelContainer;
        [SerializeField] private JumpBehaviour jumpBehaviour;

        private void OnEnable()
        {
            if (jumpBehaviour && modelContainer)
            {
                jumpBehaviour.Model = modelContainer.Model;
                jumpBehaviour.enabled = true;
            }
        }
    }
}