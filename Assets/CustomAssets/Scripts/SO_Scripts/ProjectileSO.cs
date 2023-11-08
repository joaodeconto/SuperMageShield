using UnityEngine;

namespace SuperMageShield
{
    [CreateAssetMenu(fileName = "Projectile", menuName = "Entities/Projectile")]
    public class ProjectileSO : EntitySO
    {
        public GameObject projectilePrefab;
        public float projectilePower;
    }
}
