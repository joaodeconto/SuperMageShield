using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
namespace SuperMageShield
{
    public class HeroController : MonoBehaviour
    {
        [SerializeField] private HeroSO _heroData;
        
        private SpriteRenderer _heroRenderer;
        private Vector2 _lastHeroMov;
        private Vector3 _currentHeroMove;
        private bool _isMoving;
        private bool _buffAffecting;
        private float _buffSpeed;
        private float _buffPower;
        private float _buffShield;
        public float HeroSpeed 
        {
            get 
            {
                if(_buffAffecting)
                {
                    return _heroData.heroSpeed * _buffSpeed;
                }
                return _heroData.heroSpeed;
            } 
        }

        public bool CanMove
        {
            get
            {
                if(_currentHeroMove.x > 0)
                    return transform.position.x < _heroData.offBounds;
                else if(_currentHeroMove.x < 0)
                    return transform.position.x > _heroData.offBounds *-1; 
                else return true;
            }
        }

        private void Awake()
        {
            _heroRenderer = GetComponent<SpriteRenderer>();
        }

        public void SpeedBuff(float buffValue, float buffDuration)
        {
            _buffSpeed = buffValue;
            StartCoroutine(BuffDuration(buffDuration));
        }
        public void PowerBuff(float buffValue, float buffDuration)
        {
            _buffPower = buffValue;
            StartCoroutine(BuffDuration(buffDuration));
        }
        public void ShieldBuff(float buffValue, float buffDuration)
        {
            _buffShield = buffValue;
            StartCoroutine(BuffDuration(buffDuration));
        }

        private IEnumerator BuffDuration(float buffValue)
        {
            _buffAffecting = true;
            yield return new WaitForSeconds(buffValue);
        }
        

        private void Update()
        {
            if (_isMoving && CanMove)
                DoMoveHero(_currentHeroMove);
        }

        public void OnMoveHero(InputAction.CallbackContext context)
        {
            if (context.phase != InputActionPhase.Canceled)
            {                
                _currentHeroMove = context.ReadValue<Vector2>();
                _isMoving = true;
            }
            else
                _isMoving = false;
        }

        private void DoMoveHero(Vector2 dir)
        {
            _lastHeroMov = dir;
            dir.y = 0;
            transform.position += HeroSpeed * (Vector3)dir * Time.deltaTime;

            if (_lastHeroMov.magnitude < 0 && !_heroRenderer.flipX)
                _heroRenderer.flipX = true;
            else if (_lastHeroMov.magnitude > 0 && _heroRenderer.flipX)
                _heroRenderer.flipX = false;

        }
    }
}
