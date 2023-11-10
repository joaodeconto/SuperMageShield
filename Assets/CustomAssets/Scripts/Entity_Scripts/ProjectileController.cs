using System.Collections;
using UnityEngine;

namespace SuperMageShield
{
    public class ProjectileController : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField] private ProjectileSO _projectileData;
        private SpriteRenderer _spriteRenderer;
        private Rigidbody2D _rigidbody;
        private float _projectileResistance;
        private float _canHitOffset = 1f;
        private bool _canHit = false;
        private bool _hitShield = false;


        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
        private void OnEnable()
        {
            _hitShield = false;
            _spriteRenderer.color = _projectileData.entityColor;
            transform.localScale = Vector3.one;
            _rigidbody.velocity = Vector3.zero;
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

        public bool CanHitEnemy { get { return _hitShield; } }

        public bool Hit()
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
            return _canHit;
        }

        public void HitShield(float deflectionForce)
        {
            if (_canHit)
            {
                _hitShield = true;
                //transform.localScale *= 1.2f;
                _rigidbody.velocity *= deflectionForce;
                _spriteRenderer.color = Color.magenta;
                //StartCoroutine(AwaitToHit());
            }
        }
    }
}