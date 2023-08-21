using System.Collections;
using UnityEngine;
using UnityEngine.Events;

// MyAction = 움직임이 완료되고 실행 될 델리게이트 함수
public delegate void MyAction();

[System.Serializable]
public struct MoveStat
{
    public float moveSpeed;
    public float rotSpeed;
}

public class CharacterMovement : CharacterProperty
{
    // SerializeField를 사용해서 인스펙터에 직렬화 하려고 했지만,
    // 보이지 않는 이유는 구조체가 직렬화 되어 있지 않기 때문
    // 일반적으론 MonoBehaviour자체가 직렬화 되어 있다.
    // 때문에 MoveStat 구조체를 [System.Serializable]로 직렬화 해준다.
    [SerializeField] MoveStat myMoveStat;

    public void MoveToPos(Vector3 pos, MyAction done)
    {
        // 이전에 돌고 있는 코루틴 모두 정지
        StopAllCoroutines();
        StartCoroutine(Moving(pos, done));
    }

    IEnumerator Rotating(Vector3 dir)
    {
        // Rotating, 벡터의 내적과 외적을 이용하는 로직
        // 회전 값을 계산 할 때는 길이가 같은 두 벡터의 내접값을 사용할 수 있다. Cos함수로 대변된다.
        // 외적 : 평행하지 않는 두 벡터는 한 평면 위에 존재하는데, 이 평면의 수직인 벡터를 구하는 것이 외적.
        // 여기서 나오는 수직인 벡터의 방향은 항상 왼손 법칙을 준수한다.

        // float d = Vector3.Dot(transform.forward, dir);
        // float r = Mathf.Acos(d);
        // float angle = 180.0f * (r / Mathf.PI);
        // or
        // float angle = r * Mathf.Rad2Deg; (Rad값을 오일러값으로 변환한것)

        // 두 벡터의 사잇값
        float angle = Vector3.Angle(transform.forward, dir);
        // 외적값의 방향
        float rotDir = 1.0f;
        if (Vector3.Dot(transform.right, dir) < 0.0f)
        {
            rotDir = -1.0f;
        }
        // 한번에
        // transform.Rotate(Vector3.up * angle);

        // 천천히
        while (angle > 0.0f)
        {
            float delta = Time.deltaTime * myMoveStat.rotSpeed;
            if (delta > angle) delta = angle;
            angle -= delta;
            transform.Rotate(Vector3.up * delta * rotDir);
            yield return null;
        }
    }

    protected IEnumerator Moving(Vector3 target, MyAction done)
    {
        myAnim.SetBool("isMove", true);
        Vector3 dir;
        float dist;

        // 3D 게임에서 물체를 이동 시킬 때 좌표, 위치보다는 방향 + 거리로 생각하는 것이 이상적
        dir = target - transform.position;
        dist = dir.magnitude;
        dir.Normalize();

        StartCoroutine(Rotating(dir));

        // yield return = 지연 반환, 함수가 종료되지 않고 현재 프레임에서 멈춤
        // 따라서 상단의 코드를 아래로 대체해도 다를 것 없이 작동한다.
        while (dist > 0.0f)
        {
            // Do something
            float delta = Time.deltaTime * myMoveStat.moveSpeed;
            if (delta > dist) delta = dist;
            // transform.position += dir * delta;
            transform.Translate(dir * delta, Space.World);
            dist -= delta;
            yield return null;
        }
        myAnim.SetBool("isMove", false);
        done?.Invoke();
    }

    protected void FollowTarget(Transform target, float range, UnityAction act)
    {
        StopAllCoroutines();
        StartCoroutine(Following(target, range, act));
    }

    IEnumerator Following(Transform target, float range, UnityAction act)
    {
        while (target != null)
        {
            // target의 위치가 바뀔 수 있으므로 어쩔 수 없이
            // 매 프레임마다 새롭게 위치, 방향, 거리를 측정해야 한다.
            Vector3 dir = target.position - transform.position;
            float dist = dir.magnitude;
            dir.Normalize();

            float delta = 0.0f;
            // 이동값이 0이 아니면 이동
            if (dist > 0.01f)
            {
                myAnim.SetBool("isMove", true);
                delta = Time.deltaTime * myMoveStat.moveSpeed;
                if (delta > dist) delta = dist;
                if (!myAnim.GetBool("isAttacking")) transform.Translate(dir * delta, Space.World);
            }
            else
            {
                myAnim.SetBool("isMove", false);
                act?.Invoke();
            }

            // Angle을 통해서 각도 계산
            float angle = Vector3.Angle(transform.forward, dir);
            // 회전값이 0이 아니면 회전
            if (!Mathf.Approximately(angle, 0.0f))
            {
                // Dot를 통해서 좌, 우 각도(+-) 계산 및 삼항연산자로 부호 결정
                float rotDir = Vector3.Dot(transform.right, dir) < 0.0f ? -1.0f : 1.0f;
                // 위에서 이동을 마쳤으므로 delta 재사용
                delta = Time.deltaTime * myMoveStat.rotSpeed;
                if (delta > angle) delta = angle;
                transform.Rotate(Vector3.up * delta * rotDir, Space.World);
            }

            yield return null;
        }
    }
}
