using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class AttackArea : MonoBehaviour
    {
        public GunBase gunBase;
        public GameObject player;
        public Collider attackAreaCollider;

        [SerializeField] private bool _isPlayerInttaAttackRange = false;

        private void Update()
        {
            if (_isPlayerInttaAttackRange)
            {
                gunBase.StartShoot();
            }

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == player)
            {
                _isPlayerInttaAttackRange = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject == player)
            {
                _isPlayerInttaAttackRange = false;
            }
        }

    }

}
