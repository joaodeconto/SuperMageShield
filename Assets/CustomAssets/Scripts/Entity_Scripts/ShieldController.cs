using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace SuperMageShield
{
    public class ShieldController : MonoBehaviour
    {
        [SerializeField] private ShieldSO _shieldData;

        private SpriteRenderer _shieldRenderer;
        private bool _isShieldRaised = false;

        public static UnityAction<float> OnReflectedProjectile;

        public float ShieldRaiseSpeed { get { return _shieldData.shieldRaiseSpeed; } }
        public float ShieldDuration { get { return _shieldData.shieldDuration; } }
        public float ShieldSize { get { return _shieldData.shieldSize; } }

        public float ShieldDeflectionForce { get { return _shieldData.shieldDeflectionMultiplier; } }


        public void Start()
        {
            transform.localScale = Vector2.zero;
        }

        protected void OnCollisionEnter2D(Collision2D collision)
        {
            GameObject _projectileObj = collision.gameObject;
            if (_projectileObj.CompareTag("Projectile"))
            {
                ProjectileController pc = _projectileObj.GetComponent<ProjectileController>();
                pc.HitShield(ShieldDeflectionForce);
                OnReflectedProjectile(1);
            }
        }

        public void OnRaiseShield()
        {
            if (!_isShieldRaised)
                StartCoroutine(RaisingShield());
        }

        private IEnumerator RaisingShield()
        {
            _isShieldRaised = true;
            while (transform.localScale.x < ShieldSize)
            {
                transform.localScale += new Vector3(ShieldRaiseSpeed * Time.deltaTime, ShieldRaiseSpeed * Time.deltaTime, 0);
                yield return null;
            }
            yield return StartCoroutine(ShieldActivity());
        }

        private IEnumerator ShieldActivity()
        {
            yield return new WaitForSeconds(ShieldDuration);
            StartCoroutine(DeactivateShield());
        }

        private IEnumerator DeactivateShield()
        {
            while (transform.localScale.x >= 0)
            {
                transform.localScale -= new Vector3(ShieldRaiseSpeed * Time.deltaTime, ShieldRaiseSpeed * Time.deltaTime, 0);
                yield return null;
            }
            transform.localScale = Vector2.zero;
            _isShieldRaised = false;
        }
    }
}
