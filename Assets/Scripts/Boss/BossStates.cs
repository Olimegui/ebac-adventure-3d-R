using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.StateMachine;

namespace Boss
{
    public class BossStateBase : StateBase
    {
        protected BossBase boss;


        public override void OnStateEnter(params object[] objs)
        {
            base.OnStateEnter(objs);
            if (objs.Length > 0 && objs[0] is BossBase bossBase)
            {
                boss = bossBase;
            }

            if (boss == null)
            {
                Debug.LogError("Boss reference is null!");
                return;
            }
            boss.StartInitAnimation();
        }
    }

    public class BossStateInit : BossStateBase
    {
        public override void OnStateEnter(params global::System.Object[] objs)
        {
            base.OnStateEnter(objs);
            boss.StartInitAnimation();
            Debug.Log("Enter");
        }
    }

    public class BossStateWalk : BossStateBase
    {
        public override void OnStateEnter(params global::System.Object[] objs)
        {
            base.OnStateEnter(objs);
            boss.GoToRandomPoint(BossAction.ATTACK);
        }

        private void OnArrive()
        {
            boss.SwitchState(BossAction.ATTACK);
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            Debug.Log("Exit Walk");
            boss.StopAllCoroutines();
        }
    }

    public class BossStateAttack : BossStateBase
    {
        public override void OnStateEnter(params global::System.Object[] objs)
        {
            base.OnStateEnter(objs);
            boss.StartAttack(EndAttacks);
        }

        private void EndAttacks()
        {
            boss.SwitchState(BossAction.WALK);
        }


        public override void OnStateExit()
        {
            Debug.Log("Exit Attack");
            base.OnStateExit();
            boss.StopAllCoroutines();
        }
    }

    public class BossStateDeath : BossStateBase
    {
        public override void OnStateEnter(params global::System.Object[] objs)
        {
            base.OnStateEnter(objs);
            Debug.Log("Enter Death");
            boss.transform.localScale = Vector3.one * .2f;
        }

    }
}