using UnityEngine;
using TMPro;

namespace SuperMageShield
{
    public class VillageController : EntityController
    {
        private VillageSO _villageData;
        protected override void Start()
        {
            base.Start();
            _villageData =  _entityData as VillageSO;
        }
        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            _projectileObj = collision.gameObject;
            if (_projectileObj.CompareTag("Projectile"))
            {
                ProjectileController pc = _projectileObj.GetComponent<ProjectileController>();
                _healthCurrent -= pc.Hit() ? pc.ProjectileDamage : 0;

                if (!CheckHealth())
                    DoDestroy();
            }
        }
    }
}
