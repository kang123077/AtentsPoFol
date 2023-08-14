using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Picking : MonoBehaviour
{
    public Animator myAnim;
    // LayerMask�� 32��Ʈ �ü������ ��Ʈ ���·� ���Ǳ� ������ �� nothing ���� 32���� ���̾� ����ũ�� �����Ѵ�.
    public LayerMask clickMask;
    public LayerMask objectMask;

    // �ڷ�ƾ �Լ��� �����ϴ� ����
    IEnumerator move = null;

    // StartCoroutine �� ����ص� ���������� �������� �߻�������, ������ �����̱� ������
    // ���� ���� �������� �����Ǽ� ��������� ȿ�����̴�.
    // Coroutine coMove = null;

    // �Ʒ��� ���� ����ϰ� �Ǹ� Ŀ�ø� ���°� �Ǿ� ��ü������ �����ϰ� �ȴ�.
    // public CharacterMovement characterMovement = null;

    // �Ʒ������� Ŀ�ø��� ���ϱ� ���� ��������Ʈ ������ ����Ѵ�.
    // �ϴ��� �ڵ���� code �޸� ������ ����Ǵµ�, ��������Ʈ�� ������ code �޸� ������ �Լ������ڶ�� �� �� �ִ�.
    // UnityEvent�� �ٸ� �Լ��� ������ �� �ֵ��� �����Ѵ�.
    // �Ʒ��� ���׸������� Vector3���� MyAction�� ������ ��������Ʈ �Լ�
    public UnityEvent<Vector3, MyAction> clickActs;
    // �ϴܿ��� ������Ʈ�� Ŭ������ �� ��� �� �Լ��� ��ϵ�
    public UnityEvent<Transform> objectActs;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, clickMask | objectMask))
            {
                // if (clickActs != null) clickActs.Invoke(hit.point);
                // ? : null �˻�, ��ܰ� ������ �ڵ�
                // ��������Ʈ�� �����Ǿ� �ִ� �Լ��� �����ϵ��� �����.
                if ((1 << hit.transform.gameObject.layer & objectMask) != 0)
                {
                    objectActs?.Invoke(hit.transform);
                }
                else
                {
                    clickActs?.Invoke(hit.point, null);
                }

                /*
                // transform.position = hit.point;
                if (coMove != null)
                {
                    StopAllCoroutines();
                    //StopCoroutine(coMove);
                    coMove = StartCoroutine(Moving(hit.point));
                }
                else
                {
                    coMove = StartCoroutine(Moving(hit.point));
                }
                */

                // �ڷ�ƾ ���� ������ �ڷ�ƾ�� �Ű������� �Ҵ�
                // move = Moving(hit.point);
            }

            if (move != null)
            {
                // �Ʒ��� ���� ����ϰ� �Ǹ�, yield return���� �����Ѵ�.
                // Ư���� ��� ����� �� ������, �Ϲ������� StartCoroutine���� �����ϸ� �ȴ�.
                // move.MoveNext();
            }

            // �ڷ�ƾ�� �Ʒ��� ���� Update������ ���ǹ� ���� ��� �� ���, �����ε��� ���� ������ ����Ų��.
            // StartCoroutine(Moving(hit.point));
        }

        /*
        if (dist > 0.0f)
        {
            float delta = Time.deltaTime * 3.0f;
            if (delta > dist) delta = dist;
            // transform.position += dir * delta;
            transform.Translate(dir * delta);
            dist -= delta;
        }
        */
    }

    // Stack �������� ���� �� �������� �����ڰ� ������� �ֹߵǾ������. ���� �������� �߻����� �ʴ´�.
    // �׷��� Heap ������ �ִ� �迭(C#), �ν��Ͻ��� �����ڰ� ������� �ֹߵ��� �ʰ� �������� �߻��Ѵ�.
    // �̷��� ���� �������� ���� �̻� ������ ���̰� �Ǹ� GC�� ���� �Ǹ�, �������� û���ϴµ� �� �� �����ε��� �߻��Ѵ�.
    // �ֳ��ϸ� GC�� ��� �Ǵ� �߿��� �ٸ� � �ൿ�� �ý����� ���� �ʱ� �����̸�, �� ������ ���İ������� �ټ� �߻��ϰ� �Ǹ� ������ �ȴ�.

    // �ڷ�ƾ�� ���� �� �ϳ���, �̷��� Ư���� �̿��Ͽ� �ڷ�ƾ ������ ������ ��������(Stack����)�� �����ϸ� ������ �߻��� ���� �� �ִ�.

    // ��ü���� Solid 5��Ģ
    // ����å���� ��Ģ : �� Ŭ������ �Լ��� ���� �ϳ��� å�Ӹ� ������.
}