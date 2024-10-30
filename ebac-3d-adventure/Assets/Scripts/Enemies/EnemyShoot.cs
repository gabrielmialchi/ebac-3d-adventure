using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyShoot : EnemyBase
    {
        public GunBase gunbase;
        public GameObject player;
        public SphereCollider attackAreaCollider;

        [Header("Shoot Sound")]
        public AudioClip attackClip;

        private bool _isPlayerInAttackRange = false;

        protected override void Init()
        {
            base.Init();
            //gunbase.StartShoot();
        }

        public override void Update()
        {
            base.Update();
            if (_isPlayerInAttackRange)
            {
                gunbase.StartShoot();
            }
            else
            {
                gunbase.StopShoot();
            }

        }

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject == player)
            {
                _isPlayerInAttackRange = true;
            }
        }

        private void OnTriggerExit(Collider collider)
        {
            if (collider.gameObject == player)
            {
                _isPlayerInAttackRange = false;
            }
        }

    }
}
