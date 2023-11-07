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
        public float HeroSpeed { get { return _heroData.heroSpeed; } }

        public bool CanMove
        {
            get
            {
                if(_currentHeroMove.x > 0)
                    return transform.position.x < 1;
                else if(_currentHeroMove.x < 0)
                    return transform.position.x > -1; 
                else return true;
            }
        }

        private void Awake()
        {
            _heroRenderer = GetComponent<SpriteRenderer>();
        }
        void Start()
        {

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
            transform.position += HeroSpeed * (Vector3)dir * Time.deltaTime;

            if (_lastHeroMov.magnitude < 0 && !_heroRenderer.flipX)
                _heroRenderer.flipX = true;
            else if (_lastHeroMov.magnitude > 0 && _heroRenderer.flipX)
                _heroRenderer.flipX = false;

        }
    }
}
