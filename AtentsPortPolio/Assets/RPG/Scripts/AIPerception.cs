using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AIPerception : MonoBehaviour
{
    public LayerMask enemyMask;
    public Transform myTarget = null;

    // public Monster ... �� ���� Ŀ�ø��� �Ͼ�� �ʵ��� ��������Ʈ ���
    // ���� �߰����� �� �����ؾ� �� �Լ��� �ϴܿ� �ִ� ��������Ʈ�� ���
    public UnityEvent findEnemy;
    public UnityEvent lostEnemy;

    private void OnTriggerEnter(Collider other)
    {
        // Layer����ũ�� ����ǵ���
        if ((1 << other.gameObject.layer & enemyMask) != 0)
        {
            if (myTarget == null)
            {
                // ó�� ���� �߰�
                myTarget = other.transform;
                findEnemy?.Invoke();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((1 << other.gameObject.layer & enemyMask) != 0)
        {
            // ���� �� Ÿ���� ���� ������ ���
            if (myTarget == other.transform)
            {
                // �������� ���� ������ ��
                myTarget = null;
                lostEnemy?.Invoke();
            }
        }
    }
}
