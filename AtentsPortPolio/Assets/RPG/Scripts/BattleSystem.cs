using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public struct BattleStat
{
    public float AttackPower;
    public float AttackRange;
    public float AttackDelay;

    public float MaxHealPoint;
}

public interface IAlarms
{
    event UnityAction deadAlarms;
}

public interface ITranform
{
    Transform transform
    {
        get;
    }
}

public interface IDamage
{
    void OnDamage(float dmg);
}

public interface IBattle : ITranform, IDamage
{

}


public class BattleSystem : AIMovement, IBattle, IAlarms
{
    [SerializeField] protected BattleStat myBattleStat;
    protected float curAttackDelay = 0.0f;
    protected UnityAction<float> changeHp;
    float _curHp = 0.0f;
    protected float curHealPoint
    {
        get => _curHp;
        set
        {
            _curHp = Mathf.Clamp(value, 0.0f, myBattleStat.MaxHealPoint);
            changeHp?.Invoke(_curHp / myBattleStat.MaxHealPoint);
        }
    }
    protected IBattle myTarget = null;
    public event UnityAction deadAlarms;

    protected void Initialize()
    {
        curHealPoint = myBattleStat.MaxHealPoint;
    }

    protected void AttackCheck()
    {
        if (curAttackDelay >= myBattleStat.AttackDelay)
        {
            curAttackDelay = 0.0f;
            myAnim.SetTrigger("Attack");
        }
    }

    protected virtual void OnDead()
    {
        deadAlarms?.Invoke();
    }

    public void OnAttack()
    {
        if (myTarget != null) myTarget.OnDamage(myBattleStat.AttackPower);
    }

    public void OnDamage(float dmg)
    {
        curHealPoint -= dmg;
        if (Mathf.Approximately(curHealPoint, 0.0f))
        {
            myAnim.SetTrigger("Dead");
            OnDead();
        }
        else
        {
            myAnim.SetTrigger("Damage");
        }
    }
}
