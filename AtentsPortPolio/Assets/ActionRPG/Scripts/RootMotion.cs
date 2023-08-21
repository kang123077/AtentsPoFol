using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootMotion : MonoBehaviour
{
    public Animator myAnim = null;
    Vector3 deltaPosition = Vector3.zero;
    Quaternion deltaRotation = Quaternion.identity; // 0����� ��, �⺻��

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        // ������ ��� 0.02, 1�ʸ��� 50�� ���������� ����ǳ� update�� �ƴ�
        // ���� �����Ÿ��� ������ ���ַ��� �����ӵ� FixedUpdate�� ����� ��
        // transform.parent.rotation = deltaRotation;
        transform.parent.position += deltaPosition;
        transform.parent.rotation *= deltaRotation;
        deltaPosition = Vector3.zero;
        deltaRotation = Quaternion.identity;
    }

    // Animator�� ��Ʈ����� ������ �� ����, �ڽ��� �ƴ϶� �θ� �����̵��� �ϰ� �ʹ�.
    private void OnAnimatorMove()
    {
        /*
        // �ϴ��� �� ���� Update���� ����� �� ����, �ش� �Լ������� ��� �� �� �ִ�. �ٸ������� �׳� 0
        transform.parent.Translate(myAnim.deltaPosition, Space.World);
        // ���ʹϾ��� ���ϱ� ���길 �̿�
        transform.parent.rotation *= myAnim.deltaRotation;
        */

        // ���� ���� ���� �ʰ�, �� �����Ӹ��� ������ ���� FixedUpdate���� �ݿ��Ǵ� ���
        deltaPosition += myAnim.deltaPosition;
        deltaRotation *= myAnim.deltaRotation;
    }
}
