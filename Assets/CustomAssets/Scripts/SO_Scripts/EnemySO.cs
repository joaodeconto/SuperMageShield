using UnityEngine;

namespace SuperMageShield
{
    [CreateAssetMenu(fileName = "Enemy", menuName = "Entities/Enemy")]
    public class EnemySO : EntitySO
    {
        public GameObject enemyProjectile;
        public float enemyProjectileSpeed;
        public float enemyAnimationSpeed;
        public int enemyLevel;
    }
}