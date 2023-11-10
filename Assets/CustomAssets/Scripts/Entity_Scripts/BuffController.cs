using UnityEngine;

namespace SuperMageShield
{
    public class BuffController : EntityController
    {
        private BuffSO _buffData;
        private Rigidbody2D _rb;
        protected override void Start()
        {
            base.Start();
            _buffData = _entityData as BuffSO;
            _rb = GetComponent<Rigidbody2D>();
            _rb.velocity = (_buffData.buffFallSpeed * -1 * Vector2.up);
        }
        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            _projectileObj = collision.gameObject;
            if (_projectileObj.CompareTag("Player"))
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/health bar");
                HandleBuff();
            }
        }
        protected override void OnCollisionEnter2D(Collision2D collision)
        {
            //dont
        }

        private void HandleBuff()
        {
            switch (_buffData.type)
            {
                case (BuffType)0:
                    _projectileObj.GetComponent<HeroController>().SpeedBuff(_buffData.buffValue, _buffData.buffDuration);
                    break;
                case (BuffType)1:
                    _projectileObj.GetComponent<HeroController>().ShieldBuff(_buffData.buffValue, _buffData.buffDuration);
                    break;
                case (BuffType)2:
                    _projectileObj.GetComponent<HeroController>().PowerBuff(_buffData.buffValue, _buffData.buffDuration);
                    break;
            }
            this.gameObject.SetActive(false);
        }
    }

}