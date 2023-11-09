
using UnityEngine;

namespace SuperMageShield
{
    public enum BuffType
    {
        Speed,
        Cure,
        Power
    }

    [CreateAssetMenu(fileName = "Buff", menuName = "Entities/Buff")]
    public class BuffSO : EntitySO
    {

        public BuffType type;
        public float buffFallSpeed = 3f;
        public float buffDuration = 3.0f;
        public float buffValue = 2f;
        

    }
    
}
