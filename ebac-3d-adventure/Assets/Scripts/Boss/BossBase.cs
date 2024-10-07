using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.StateMachine;
using DG.Tweening;
//using static UnityEditor.Experimental.GraphView.GraphView;

namespace Boss
{
    public enum BossAction
    {
        INIT,
        IDLE,
        WALK,
        ATTACK,
        DEATH
    }

    public class BossBase : MonoBehaviour, IDamageable
    {
        [Header("Animation")]
        public float startAnimationDuration = .5f;
        public Ease startAnimationEase = Ease.OutBack;
        public bool startWithSpawnAnimation = true;

        [Header("Attack")]
        public int attackAmount = 5;
        public float attackSpeed = .5f;
        public bool lookAtPlayer;
        private bool _isPlayerInAttackRange = false;

        public float speed = 10f;
        public List<Transform> waypoints;

        public HealthBase healthBase;
        public Collider attackAreaCollider;
        public GameObject player;
        public GunBase gunbase;
        public FlashColor flashColor;
        public ParticleSystem damageParticleSystem;

        private StateMachine<BossAction> stateMachine;

        private void OnValidate()
        {
            if (healthBase == null) healthBase = GetComponent<HealthBase>();
        }

        private void Awake()
        {
            OnValidate();
            Init();
            healthBase.OnKillAction += OnBossKill;
            //healthBase.OnDamageAction += Damage;
        }

        private void Update()
        {
            BossAttack();
            LookAtPlayer();
        }

        private void Init()
        {
            if (startWithSpawnAnimation) StartInitAnimation();

            stateMachine = new StateMachine<BossAction>();
            stateMachine.Init();

            stateMachine.RegisterStates(BossAction.INIT, new BossStateInit());
            stateMachine.RegisterStates(BossAction.WALK, new BossStateWalk());
            stateMachine.RegisterStates(BossAction.ATTACK, new BossStateAttack());
            stateMachine.RegisterStates(BossAction.DEATH, new BossStateDeath());
        }

        private void OnBossKill(HealthBase hb)
        {
            SwitchState(BossAction.DEATH);
        }

        #region ATTACK
        public void StartAttack(Action endCallback = null)
        {
            StartCoroutine(AttackCoroutine(endCallback));
        }

        IEnumerator AttackCoroutine(Action endCallback)
        {
            int attack = 0;
            while (attack < attackAmount)
            {
                attack++;
                transform.DOScale(1.1f, .1f).SetLoops(2, LoopType.Yoyo);
                yield return new WaitForSeconds(attackSpeed);
            }

            endCallback?.Invoke();
        }

        public void BossAttack()
        {
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
                //StartInitAnimation();
            }
        }

        private void OnTriggerExit(Collider collider)
        {
            if (collider.gameObject == player)
            {
                _isPlayerInAttackRange = false;
            }
        }

        public void LookAtPlayer()
        {
            if (lookAtPlayer)
            {
                transform.LookAt(player.transform.position);
            }
        }
        #endregion

        #region WALK
        public void GoToRandomPoints(Action onArrive = null)
        {
            StartCoroutine(GoToPointCoroutine(waypoints[UnityEngine.Random.Range(0, waypoints.Count)], onArrive));
        }

        IEnumerator GoToPointCoroutine(Transform t, Action onArrive = null)
        {
            while (Vector3.Distance(transform.position, t.position) > 1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, t.position, Time.deltaTime * speed);
                yield return new WaitForEndOfFrame();
            }
            onArrive?.Invoke();
        }
        #endregion

        #region ANIMATION
        public void StartInitAnimation()
        {
            transform.DOScale(1, startAnimationDuration).SetEase(startAnimationEase);

            //transform.DOScale(1, startAnimationDuration).SetEase(startAnimationEase).OnComplete(() => stateMachine.SwitchState(BossAction.WALK));
        }
        #endregion

        #region HEALTH
        public void OnDamage(int damage)
        {
            if (flashColor != null) flashColor.Flash();
            if (damageParticleSystem != null) damageParticleSystem.Play();

            //transform.position -= transform.forward;

            healthBase._currentLife -= damage;

            if (healthBase._currentLife <= 0)
            {
                healthBase.Kill();
            }
        }

        public void Damage(int damage)
        {
            OnDamage(damage);
        }

        public void Damage(int damage, Vector3 direction)
        {
            OnDamage(damage);
            transform.DOMove(transform.position - direction, .1f);
        }
        #endregion

        #region DEBUG
        [NaughtyAttributes.Button]
        private void SwitchInit()
        {
            SwitchState(BossAction.INIT);
        }


        /////////////////////////////////


        [NaughtyAttributes.Button]
        private void SwitchWalk()
        {
            SwitchState(BossAction.WALK);
        }

        /////////////////////////////////

        [NaughtyAttributes.Button]
        private void SwitchAttack()
        {
            SwitchState(BossAction.ATTACK);
        }
        #endregion

        #region STATE MACHINE
        public void SwitchState(BossAction state)
        {
            stateMachine.SwitchState(state, this);
        }
        #endregion
    }
}
