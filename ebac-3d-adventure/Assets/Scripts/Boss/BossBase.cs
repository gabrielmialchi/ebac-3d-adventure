using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.StateMachine;
using DG.Tweening;

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

    public class BossBase : MonoBehaviour
    {
        [Header("Animation")]
        public float startAnimationDuration = .5f;
        public Ease startAnimationEase = Ease.OutBack;
        public bool startWithSpawnAnimation = true;


        [Header("Attack")]
        public int attackAmount = 5;
        public float attackSpeed = .5f;

        public float speed = 10f;
        public List<Transform> waypoints;

        public HealthBase healthBase;

        public SphereCollider attackArea;

        private StateMachine<BossAction> stateMachine;

        private void Awake()
        {
            Init();
            healthBase.OnKillAction += OnBossKill;
        }

        private void Update()
        {
            
        }

        private void OnTriggerEnter(Collider other)
        {
            //Init();
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
            transform.DOScale(0, startAnimationDuration).SetEase(startAnimationEase).From();
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
