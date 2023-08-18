using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : BattleSystem
{
    void Start()
    {
        base.Initialize();
    }
    void Update()
    {
        if (myTarget != null)
        {
            if (!myAnim.GetBool("IsAttacking")) curAttackDelay += Time.deltaTime;
        }
    }

    public void AttackTarget(Transform target)
    {
        myTarget = target.GetComponent<IBattle>();
        IAlarms alarm = target.GetComponent<IAlarms>();
        if (alarm != null)
        {
            alarm.deadAlarms += () =>
            {
                StopAllCoroutines();
                myTarget = null;
            };
        }

        FollowTarget(target, myBattleStat.AttackRange, AttackCheck);
    }
}
