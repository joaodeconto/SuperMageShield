using System.Collections;
using UnityEngine;

namespace SuperMageShield
{
    public class ShieldController : MonoBehaviour
    {
        [SerializeField] private ShieldSO _shieldData;

        private SpriteRenderer _shieldRenderer;
        private bool _isShieldRaised = false;

        public float ShieldRaiseSpeed { get { return _shieldData.shieldRaiseSpeed; } }
        public float ShieldDuration { get { return _shieldData.shieldDuration; } }
        public float ShieldSize { get { return _shieldData.shieldSize; } }


        public void Start()
        {
            InitShield();
            transform.localScale = Vector2.zero;
        }

        private void InitShield()
        {
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
