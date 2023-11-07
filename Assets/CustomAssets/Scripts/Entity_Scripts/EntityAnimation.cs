using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SuperMageShield
{
    public class EntityAnimation : MonoBehaviour
    {

        [SerializeField] protected Sprite[] animatonSprites;
        [SerializeField] protected float animationTime;
        [SerializeField] protected EntitySO entitySO;

        protected SpriteRenderer _spriteRenderer;
        protected int _animationFrame;


        protected virtual void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
        // Start is called before the first frame update
        void Start()
        {
            InvokeRepeating(nameof(AnimateSprite), animationTime, animationTime);
        }

        protected virtual void AnimateSprite()
        {
            _animationFrame++;

            if (_animationFrame >= animatonSprites.Length)
            {
                _animationFrame = 0;
            }

            _spriteRenderer.sprite = this.animatonSprites[_animationFrame];
        }
    }
}