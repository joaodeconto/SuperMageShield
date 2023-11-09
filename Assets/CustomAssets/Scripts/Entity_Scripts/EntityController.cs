
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace SuperMageShield
{
    public class EntityController : MonoBehaviour
    {
        [SerializeField] protected EntitySO _entityData;

        [SerializeField] protected TMP_Text _healthFeedback;

        [SerializeField] protected TMP_Text _pointsFeedback;

        protected Collider2D _collider;

        protected GameObject _projectileObj;

        protected float _healthCurrent;


        public static UnityAction<EntitySO> OnEntityDefeated;

        protected void Awake()
        {
            _collider = GetComponent<Collider2D>();
            _healthCurrent = _entityData.entityHealth;

            if (_healthFeedback != null)
                _healthFeedback.text = _healthCurrent.ToString();
        }

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            _projectileObj = collision.gameObject;
            if (_projectileObj.CompareTag("Projectile"))
            {
                ProjectileController pc = _projectileObj.GetComponent<ProjectileController>();
                pc.Hit();
                _healthCurrent -= pc.ProjectileDamage;
                if (!CheckHealth())
                    DoDestroy();
            }
        }
        protected virtual void OnCollisionEnter2D(Collision2D collision)
        {
            _projectileObj = collision.gameObject;
            if (_projectileObj.CompareTag("Projectile"))
            {
                ProjectileController pc = _projectileObj.GetComponent<ProjectileController>();
                pc.Hit();
                _healthCurrent -= pc.ProjectileDamage;
                if (!CheckHealth())
                    DoDestroy();
            }
        }

        protected bool CheckHealth()
        {
            if (_healthFeedback != null)
                _healthFeedback.text = _healthCurrent.ToString();

            return _healthCurrent > 0;
        }

        protected virtual void DoDestroy()
        {
            OnEntityDefeated(_entityData);

            if (_pointsFeedback != null)
                _pointsFeedback.text = _entityData.entityScore.ToString();

            gameObject.SetActive(false);
        }
    }
}