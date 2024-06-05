using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Enemy
{
    public class EnemyBase : MonoBehaviour
    {
        public int startLife = 10;
        public int maxLife = 100;

        [SerializeField] private int _currentLife;

        [Header("Start Animation")]
        public float startAnimationDuration = .2f;
        public Ease startAnimationEase = Ease.OutBack;
        public bool startWithSpawnAnimation = true;

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
            Destroy(gameObject);
        }

        public void OnDamage(int damage)
        {
            _currentLife -= damage;

            if (_currentLife <= 0)
            {
                Kill();
            }
        }

        #region ANIMATIONS
        private void SpawnAnimation()
        {
            transform.DOScale(0, startAnimationDuration).SetEase(startAnimationEase).From();
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
    }
}
