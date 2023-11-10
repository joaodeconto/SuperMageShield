using UnityEngine;


namespace SuperMageShield
{
    [CreateAssetMenu(fileName = "Shield", menuName = "Entities/Shield")]
    public class ShieldSO : EntitySO
    {
        public float shieldRaiseSpeed;
        public float shieldDuration;
        public float shieldSize;
        public float shieldDeflectionMultiplier;
    }
}
