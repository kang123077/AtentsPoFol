using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class Monster : BattleSystem
{
    // ���ѻ��±�� �������� ( �ʱ� ���ؿ��� ��� )
    // �������� ���ؼ� ���¸� ������ ����, ������ ���°� ��ø�Ǿ �� ��.
    public enum State
    {
        Create, Normal, Roaming, Battle, Dead
    }

    public State myState = State.Create;
    Vector3 startPos = Vector3.zero;
    float playTime = 0.0f;
    public AIPerception myPerception;
    public Transform barPoint;

    /// <summary>
    /// ���°� �ٲ� �� ���
    /// </summary>
    void ChangeState(State s)
    {
        if (myState == s) return;
        myState = s;
        switch (myState)
        {
            case State.Create:
                break;
            case State.Normal:
                float rnd = Random.Range(0.0f, 360.0f);
                Vector3 rndDir = Quaternion.Euler(0, rnd, 0) * Vector3.forward * Random.Range(0.0f, 5.0f);
                Vector3 rndPos = startPos + rndDir;
                MoveToPos(rndPos, () => { StartCoroutine(DelayChangeNormal(playTime)); });
                playTime = Random.Range(1.0f, 3.0f);
                ChangeState(State.Roaming);
                break;
            case State.Roaming:
                break;
            case State.Battle:
                FollowTarget(myPerception.myTarget, myBattleStat.AttackRange, AttackCheck);
                break;
            case State.Dead:
                StopAllCoroutines();
                StartCoroutine(DisAearing(2.0f));
                break;
        }
    }

    /// <summary>
    /// Update������ �� ������ ���� ó���Ǵ� State ó�� �Լ�
    /// </summary>
    void StateProcess()
    {
        switch (myState)
        {
            case State.Create:
                break;
            case State.Normal:
                break;
            case State.Roaming:
                break;
            case State.Battle:
                if (!myAnim.GetBool("isAttacking")) curAttackDelay += Time.deltaTime;
                break;
            case State.Dead:
                break;
        }
    }

    // ������ ���� : ���õ� �����ڵ��� �ڵ带 �м��� ��� ������ �����µ�
    // �̸� å���� �Ⱓ�� ���� ������ �����̶�� å�̾���.

    // observer ���� : �� ������ ���� �˻��ϴ� ���� �ƴ϶�,
    // ��� ���� ������ �� ��ȣ�� �����ϴ� ���. HP�ٿ� ���� ��Ȳ�� ���

    // MyAction�� ��ȯ��, �Ķ���Ͱ� ���� ��������Ʈ �Լ��̹Ƿ� ���缭 �ۼ�
    // �� ����Լ��� �þ�� ���� ���ϱ� ���ؼ� ��ܿ��� �͸��Լ�(����)�� ���.
    void RoamingDone()
    {
        StartCoroutine(DelayChangeNormal(playTime));
    }

    IEnumerator DelayChangeNormal(float t)
    {
        yield return new WaitForSeconds(t);
        ChangeState(State.Normal);
    }

    // ������ ������� �ڷ�ƾ
    IEnumerator DisAearing(float t)
    {
        yield return new WaitForSeconds(t);
        float dist = 1.0f;
        while (dist < 0.0f)
        {
            float delta = Time.deltaTime;
            if (delta > dist) delta = dist;
            transform.Translate(Vector3.down * delta, Space.World);
            dist -= delta;
            yield return null;
        }
        Destroy(gameObject);
    }

    // ���ʹϾ�, ����Ƽ���� ȸ���� ��Ÿ�� �� ����ϴ� 4���� ü��.
    // �� �� w�� ���(���� �� -1�� �Ǵ� ��)�� ���� ȸ���� ǥ�� ����
    // ���� ū �����δ� ������ ������ ���ϱ� ���ؼ� �̿��Ѵ�.

    void Start()
    {
        GameObject obj = Instantiate(Resources.Load("UI\\HpBar") as GameObject, SceneData.inst.hpBars);
        obj.GetComponent<HpBar>().Initialize(barPoint);
        // changeHp += obj.GetComponentInParent<HpBar>().UpdateValue;
        changeHp += obj.GetComponent<IChangeValue>().UpdateValue;
        deadAlarms += obj.GetComponent<IDeadMsg>().OnDead;

        base.Initialize();
        startPos = transform.position;
        ChangeState(State.Normal);
    }

    void Update()
    {
        StateProcess();
    }

    public void FindTarget()
    {
        myTarget = myPerception.myTarget.GetComponent<IBattle>();
        IAlarms alarm = myPerception.myTarget.GetComponent<IAlarms>();
        if (alarm != null)
        {
            alarm.deadAlarms += () =>
            {
                StopAllCoroutines();
                ChangeState(State.Normal);
            };
        }
        ChangeState(State.Battle);
    }

    protected override void OnDead()
    {
        base.OnDead();
        ChangeState(State.Dead);
    }
}
