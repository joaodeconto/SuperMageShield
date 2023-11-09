
using UnityEngine;


namespace SuperMageShield
{
    [CreateAssetMenu(fileName = "Hero", menuName = "Entities/Hero")]
    public class HeroSO : EntitySO
    {
        public float heroSpeed;
        public float heroMaxHealth;
        public float speedBuff;
        public float offBounds;
    }
}