
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
        protected Vector2 _initialPosition;
        public static UnityAction<EntitySO> OnEntityDefeated;

        protected virtual void Awake()
        {            
            _collider = GetComponent<Collider2D>();
            _healthCurrent = _entityData.entityHealth;

            if (_healthFeedback != null)
                _healthFeedback.text = _healthCurrent.ToString();

            GameStateManager.OnStateChanged += HandleState;
            HandleState(GameStateManager.Instance._currentState);
        }
        protected virtual void Start()
        {
            _initialPosition = transform.position;
        }
        private void OnDestroy()
        {
            GameStateManager.OnStateChanged -= HandleState;
        }

        protected virtual void HandleState(GameState gameState)
        {
            switch (gameState)
            {
                case (GameState)0:
                    ResetStats();
                    //transform.position = _initialPosition;
                    break;
                case (GameState)1:
                    break;
                case (GameState)2:
                    break;
                case (GameState)3:
                case (GameState)4:
                    break;
            }
        }

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            _projectileObj = collision.gameObject;
            if (_projectileObj.CompareTag("Projectile"))
            {
                ProjectileController pc = _projectileObj.GetComponent<ProjectileController>();
                _healthCurrent -= pc.Hit() && pc.CanHitEnemy ? pc.ProjectileDamage : 0;

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
                _healthCurrent -= pc.Hit() && pc.CanHitEnemy ? pc.ProjectileDamage : 0;
                if (!CheckHealth())
                    DoDestroy();
            }
        }

        protected virtual void ResetStats()
        {
            gameObject.SetActive(true);
            _healthCurrent = _entityData.entityHealth;
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