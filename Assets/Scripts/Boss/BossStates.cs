using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Ebac.StateMachine;

namespace Boss
{
    public enum BossAction
    {
        INIT,
        WALK,
        ATTACK,
        DEATH
    }

    public class BossBase : MonoBehaviour
    {
        [Header("Animation")]
        public float startAnimationDuration = .5f;
        public Ease startAnimationEase = Ease.OutBack;

        [Header("Attack")]
        public int attackAmount = 5;
        public float timeBetweenAttacks = .5f;

        public float speed = 5f;
        public List<Transform> waypoints;
        public HealthBase healthBase;

        private StateMachine<BossAction> stateMachine;

        private void Awake()
        {
            Init();
            OnValidate();
            healthBase.OnKill += OnBossKill;
        }

        private void OnValidate()
        {
            if (healthBase == null) healthBase = GetComponent<HealthBase>();
        }

        private void Init()
        {
            stateMachine = new StateMachine<BossAction>(BossAction.INIT);
            stateMachine.Init();
            stateMachine.RegisterStates(BossAction.INIT, new BossStateInit());
            stateMachine.RegisterStates(BossAction.WALK, new BossStateWalk());
            stateMachine.RegisterStates(BossAction.ATTACK, new BossStateAttack()); // única versão válida
            stateMachine.RegisterStates(BossAction.DEATH, new BossStateDeath());
        }

        public void SwitchState(BossAction state)
        {
            stateMachine.SwitchState(state, this);
        }

        private void OnBossKill(HealthBase h)
        {
            SwitchState(BossAction.DEATH);
        }

        #region ATTACK
        public void StartAttack(Action endCallback = null)
        {
            StartCoroutine(AttackCoroutine(endCallback));
        }

        private IEnumerator AttackCoroutine(Action endCallback)
        {
            int attacks = 0;
            while (attacks < attackAmount)
            {
                attacks++;
                transform.DOScale(1.1f, .1f).SetLoops(2, LoopType.Yoyo);
                yield return new WaitForSeconds(timeBetweenAttacks);
            }
            endCallback?.Invoke();
        }
        #endregion

        #region WALK
        public void GoToRandomPoint(BossAction? onArrive = null)
        {
            Transform t = waypoints[UnityEngine.Random.Range(0, waypoints.Count)];
            StartCoroutine(GoToPointCoroutine(t, onArrive));
        }

        private IEnumerator GoToPointCoroutine(Transform t, BossAction? onArrive = null)
        {
            while (Vector3.Distance(transform.position, t.position) > 1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, t.position, Time.deltaTime * speed);
                yield return null;
            }

            if (onArrive.HasValue)
                stateMachine.SwitchState(onArrive.Value);
        }
        #endregion

        #region ANIMATION
        public void StartInitAnimation()
        {
            transform.DOScale(0, startAnimationDuration).SetEase(startAnimationEase).From();
        }
        #endregion
    }

    // ================== ESTADOS ==================

    public class BossStateBase : Ebac.StateMachine.StateBase

    {
        protected BossBase boss;

        public override void OnStateEnter(params object[] args)
        {
            base.OnStateEnter(args);
            if (args.Length > 0 && args[0] is BossBase b) boss = b;
        }
    }

    public class BossStateInit : BossStateBase
    {
        public override void OnStateEnter(params object[] args)
        {
            base.OnStateEnter(args);
            boss.StartInitAnimation();
        }
    }

    public class BossStateWalk : BossStateBase
    {
        public override void OnStateEnter(params object[] args)
        {
            base.OnStateEnter(args);
            boss.GoToRandomPoint(BossAction.ATTACK);
        }

        public override void OnStateExit()
        {
            boss.StopAllCoroutines();
        }
    }

    public class BossStateAttack : BossStateBase
    {
        public override void OnStateEnter(params object[] args)
        {
            base.OnStateEnter(args);
            Debug.Log("👊 Boss começou o ataque!");

            boss.StartAttack(() =>
            {
                boss.SwitchState(BossAction.WALK);
            });
        }

        public override void OnStateExit()
        {
            Debug.Log("⛔ Boss terminou o ataque!");
        }
    }

    public class BossStateDeath : BossStateBase
    {
        public override void OnStateEnter(params object[] args)
        {
            base.OnStateEnter(args);
            boss.transform.localScale = Vector3.one * 0.2f;
        }
    }
}