using UnityEngine;

namespace SuperMageShield
{
    [CreateAssetMenu(fileName = "Projectile", menuName = "Entities/Projectile")]
    public class ProjectileSO : EntitySO
    {
        public float projectileResistance;
        public float projectileDamage;
    }
}
