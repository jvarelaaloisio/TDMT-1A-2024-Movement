using UnityEngine;

namespace Movement.Jump
{
    [CreateAssetMenu(menuName = "Models/Jump", fileName = "JM_")]
    public class JumpModelContainer : ScriptableObject
    {
        [field: SerializeField] public JumpModel Model { get; private set; }
    }
}