using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct BattleStat
{
    public float Attack;
    public float Range;
    public float Delay;
}

public class BattleSystem : AIMovement
{
    [SerializeField] protected BattleStat MyBattleStat;
    protected float curAttackDelay = 0.0f;

    protected void AttackCheck()
    {
        if (curAttackDelay >= MyBattleStat.Delay)
        {
            curAttackDelay = 0;
            myAnim.SetTrigger("Attack");
        }
    }
}
