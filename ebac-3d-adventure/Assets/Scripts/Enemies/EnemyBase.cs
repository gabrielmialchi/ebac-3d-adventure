using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Animation;

namespace Enemy
{
    public class EnemyBase : MonoBehaviour, IDamageable
    {
        public Collider enemyCollider;

        public FlashColor flashColor;

        public ParticleSystem damageParticleSystem;

        public int startLife = 10;
        public int maxLife = 100;

        [SerializeField] private int _currentLife;

        [Header("Animation")]
        [SerializeField] private AnimationBase _animationBase;

        [Header("Spawn Animation")]
        public float spawnAnimationDuration = .2f;
        public Ease spawnAnimationEase = Ease.OutBack;
        public bool startWithSpawnAnimation = true;

        [SerializeField] private float _delayToDestroy = 3f;

        private void Awake()
        {
            Init();
        }

        protected virtual void Init()
        {
            ResetLife();
            if (startWithSpawnAnimation) SpawnAnimation();
        }

        protected virtual void ResetLife()
        {
            _currentLife = startLife;
        }

        protected virtual void Kill()
        {
            OnKill();
        }

        protected virtual void OnKill()
        {
            if (enemyCollider != null) enemyCollider.enabled = false;
            Destroy(gameObject, _delayToDestroy);
            PlayAnimationByTrigger(AnimationType.DEATH);
        }

        public void OnDamage(int damage)
        {
            if (flashColor != null) flashColor.Flash();
            if(damageParticleSystem != null) damageParticleSystem.Play();

            _currentLife -= damage;

            if (_currentLife <= 0)
            {
                Kill();
            }
        }

        #region ANIMATIONS
        private void SpawnAnimation()
        {
            transform.DOScale(0, spawnAnimationDuration).SetEase(spawnAnimationEase).From();
        }

        public void PlayAnimationByTrigger(AnimationType animationType)
        {
            _animationBase.PlayAnimationByTrigger(animationType);
        }

        #endregion

        // debug
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                OnDamage(1);
            }
        }

        public void Damage(int damage)
        {
            Debug.Log("Damage");
            OnDamage(damage);
        }
    }
}
