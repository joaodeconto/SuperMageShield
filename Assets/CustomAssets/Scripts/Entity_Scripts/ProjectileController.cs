using System.Collections;
using UnityEngine;

namespace SuperMageShield
{
    public class ProjectileController : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField] private ProjectileSO _projectileData;
        private SpriteRenderer _spriteRenderer;
        private float _projectileResistance;
        private float _canHitOffset = 1f;
        private bool _canHit = false;

        private void OnEnable()
        {
            if(_spriteRenderer == null)
                _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.color = _projectileData.entityColor;
            transform.localScale = Vector3.one;
            StartCoroutine(AwaitToHit());
        }
        private IEnumerator AwaitToHit()
        {
            _canHit = false;
            yield return new WaitForSeconds(_canHitOffset);
            _canHit = true;
        }
        public float ProjectileDamage
        {
            get { return _projectileData.projectileDamage; }
        }

        public void Hit()
        {
            if (_canHit)
            {            
                _projectileResistance -= 1;
                if (_projectileResistance <= 0)
                {
                    this.gameObject.SetActive(false);
                    gameObject.transform.position = Vector3.zero;
                }
                else
                    StartCoroutine(AwaitToHit());
            }
        }

        public void HitShield()
        {
            if (_canHit)
            {
                transform.localScale *= 1.2f;
                _spriteRenderer.color = Color.magenta;
                StartCoroutine(AwaitToHit());
            }
        }

    }
}