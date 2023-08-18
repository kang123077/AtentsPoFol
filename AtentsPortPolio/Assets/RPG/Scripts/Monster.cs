using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class Monster : BattleSystem
{
    // 유한상태기계 전략패턴 ( 초급 수준에서 사용 )
    // 열거형을 통해서 상태를 나누고 시작, 각각의 상태가 중첩되어선 안 됨.
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
    /// 상태가 바뀔 때 사용
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
    /// Update문에서 매 프레임 마다 처리되는 State 처리 함수
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

    // 디자인 패턴 : 숙련된 개발자들의 코드를 분석한 결과 패턴이 보였는데
    // 이를 책으로 출간한 것이 디자인 패턴이라는 책이었다.

    // observer 패턴 : 매 프레임 마다 검사하는 것이 아니라,
    // 어떠한 일이 끝났을 때 신호를 전달하는 방식. HP바와 같은 상황에 사용

    // MyAction은 반환값, 파라미터가 없는 델리게이트 함수이므로 맞춰서 작성
    // 단 멤버함수가 늘어나는 것을 피하기 위해서 상단에서 익명함수(람다)를 사용.
    void RoamingDone()
    {
        StartCoroutine(DelayChangeNormal(playTime));
    }

    IEnumerator DelayChangeNormal(float t)
    {
        yield return new WaitForSeconds(t);
        ChangeState(State.Normal);
    }

    // 죽으면 사라지는 코루틴
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

    // 쿼터니언, 유니티에서 회전을 나타낼 때 사용하는 4원수 체계.
    // 이 중 w는 허수(제곱 시 -1이 되는 수)를 통해 회전을 표현 가능
    // 가장 큰 이유로는 짐벌락 현상을 피하기 위해서 이용한다.

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
