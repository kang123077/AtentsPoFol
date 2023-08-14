using System.Collections;
using UnityEngine;
using UnityEngine.Events;

// MyAction = �������� �Ϸ�ǰ� ���� �� ��������Ʈ �Լ�
public delegate void MyAction();

[System.Serializable]
public struct MoveStat
{
    public float moveSpeed;
    public float rotSpeed;
}

public class CharacterMovement : CharacterProperty
{
    // SerializeField�� ����ؼ� �ν����Ϳ� ����ȭ �Ϸ��� ������,
    // ������ �ʴ� ������ ����ü�� ����ȭ �Ǿ� ���� �ʱ� ����
    // �Ϲ������� MonoBehaviour��ü�� ����ȭ �Ǿ� �ִ�.
    // ������ MoveStat ����ü�� [System.Serializable]�� ����ȭ ���ش�.
    [SerializeField] MoveStat myMoveStat;

    public void MoveToPos(Vector3 pos, MyAction done)
    {
        // ������ ���� �ִ� �ڷ�ƾ ��� ����
        StopAllCoroutines();
        StartCoroutine(Moving(pos, done));
    }

    IEnumerator Rotating(Vector3 dir)
    {
        // Rotating, ������ ������ ������ �̿��ϴ� ����
        // ȸ�� ���� ��� �� ���� ���̰� ���� �� ������ �������� ����� �� �ִ�. Cos�Լ��� �뺯�ȴ�.
        // ���� : �������� �ʴ� �� ���ʹ� �� ��� ���� �����ϴµ�, �� ����� ������ ���͸� ���ϴ� ���� ����.
        // ���⼭ ������ ������ ������ ������ �׻� �޼� ��Ģ�� �ؼ��Ѵ�.

        // float d = Vector3.Dot(transform.forward, dir);
        // float r = Mathf.Acos(d);
        // float angle = 180.0f * (r / Mathf.PI);
        // or
        // float angle = r * Mathf.Rad2Deg; (Rad���� ���Ϸ������� ��ȯ�Ѱ�)

        // �� ������ ���հ�
        float angle = Vector3.Angle(transform.forward, dir);
        // �������� ����
        float rotDir = 1.0f;
        if (Vector3.Dot(transform.right, dir) < 0.0f)
        {
            rotDir = -1.0f;
        }
        // �ѹ���
        // transform.Rotate(Vector3.up * angle);

        // õõ��
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

        // 3D ���ӿ��� ��ü�� �̵� ��ų �� ��ǥ, ��ġ���ٴ� ���� + �Ÿ��� �����ϴ� ���� �̻���
        dir = target - transform.position;
        dist = dir.magnitude;
        dir.Normalize();

        StartCoroutine(Rotating(dir));

        // yield return = ���� ��ȯ, �Լ��� ������� �ʰ� ���� �����ӿ��� ����
        // ���� ����� �ڵ带 �Ʒ��� ��ü�ص� �ٸ� �� ���� �۵��Ѵ�.
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
            // target�� ��ġ�� �ٲ� �� �����Ƿ� ��¿ �� ����
            // �� �����Ӹ��� ���Ӱ� ��ġ, ����, �Ÿ��� �����ؾ� �Ѵ�.
            Vector3 dir = target.position - transform.position;
            float dist = dir.magnitude;
            dir.Normalize();

            float delta = 0.0f;
            // �̵����� 0�� �ƴϸ� �̵�
            if (!Mathf.Approximately(dist, 0.0f))
            {
                myAnim.SetBool("isMove", true);
                delta = Time.deltaTime * myMoveStat.moveSpeed;
                if (delta > dist) delta = dist;
                transform.Translate(dir * delta, Space.World);
            }
            else
            {
                myAnim.SetBool("isMove", false);
                act?.Invoke();
            }

            // Angle�� ���ؼ� ���� ���
            float angle = Vector3.Angle(transform.forward, dir);
            // ȸ������ 0�� �ƴϸ� ȸ��
            if (!Mathf.Approximately(angle, 0.0f))
            {
                // Dot�� ���ؼ� ��, �� ����(+-) ��� �� ���׿����ڷ� ��ȣ ����
                float rotDir = Vector3.Dot(transform.right, dir) < 0.0f ? -1.0f : 1.0f;
                // ������ �̵��� �������Ƿ� delta ����
                delta = Time.deltaTime * myMoveStat.rotSpeed;
                if (delta > angle) delta = angle;
                transform.Rotate(Vector3.up * delta * rotDir, Space.World);
            }

            yield return null;
        }
    }
}
