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
                break;
            case State.Battle:
                FollowTarget(myPerception.myTarget, MyBattleStat.Range, AttackCheck);
                break;
            case State.Dead:
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
                float rand = Random.Range(0.0f, 360.0f);
                // ���ʹϾ� ���͸� ���ϸ� ���Ͱ� ȸ���Ѵٰ� ���� �� �� �ִ�.
                Vector3 randDir = Quaternion.Euler(0, rand, 0) * Vector3.forward * Random.Range(0.0f, 5.0f);
                Vector3 randPos = startPos + randDir;
                // �ϴܰ� ���� ������ ������ observer �����̶�� �Ѵ�.
                MoveToPos(randPos, () => { StartCoroutine(DelayChangeNormal(playTime)); });
                playTime = Random.Range(1.0f, 3.0f);
                ChangeState(State.Roaming);
                break;
            case State.Roaming:
                break;
            case State.Battle:
                curAttackDelay += Time.deltaTime;
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

    // ���ʹϾ�, ����Ƽ���� ȸ���� ��Ÿ�� �� ����ϴ� 4���� ü��.
    // �� �� w�� ���(���� �� -1�� �Ǵ� ��)�� ���� ȸ���� ǥ�� ����
    // ���� ū �����δ� ������ ������ ���ϱ� ���ؼ� �̿��Ѵ�.

    void Start()
    {
        startPos = transform.position;
        ChangeState(State.Normal);
    }

    void Update()
    {
        StateProcess();
    }

    public void FindTarget()
    {
        ChangeState(State.Battle);
    }

    public void LostTarget()
    {
        ChangeState(State.Normal);
    }
}
