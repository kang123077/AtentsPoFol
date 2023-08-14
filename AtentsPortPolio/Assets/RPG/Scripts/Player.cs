using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : BattleSystem
{
    Transform myTarget = null;

    private void Update()
    {
        if (myTarget != null)
        {
            if (!myAnim.GetBool("isAttacking")) curAttackDelay += Time.deltaTime;
        }
    }

    public void AttackTarget(Transform target)
    {
        myTarget = target;
        FollowTarget(myTarget, MyBattleStat.Range, AttackCheck);
    }
}
