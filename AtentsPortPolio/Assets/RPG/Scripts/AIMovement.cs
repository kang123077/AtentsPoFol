using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// NavMesh�� ��������, �Ϲ������� ���̱⶧���� ���������� �Ϻθ� ���� ���
public class AIMovement : CharacterMovement
{
    NavMeshPath myPath = null;
    public void MoveToPosByNav(Vector3 pos, MyAction act)
    {
        StopAllCoroutines();
        if (myPath == null) myPath = new NavMeshPath();
        // NavMesh.AllAreas : ������ LayerMask, �� ������ �������� ����� �����ؼ� �ּҰ��� �����ϵ��� �� �� �ִ�.
        if (NavMesh.CalculatePath(transform.position, pos, NavMesh.AllAreas, myPath))
        {
            switch (myPath.status)
            {
                case NavMeshPathStatus.PathComplete: // ����
                case NavMeshPathStatus.PathPartial: // ���� ����
                    StartCoroutine(MovingByPath(myPath.corners));
                    break;
                case NavMeshPathStatus.PathInvalid: // ����
                    break;
            }
        }
    }

    // list : ����� ������ �޾ƿ´�
    IEnumerator MovingByPath(Vector3[] list)
    {
        int curPath = 1;
        while (curPath < list.Length)
        {
            // �ڷ�ƾ���� yield return�� StartCoroutine(Moving(list[curPath++], null));
            // �� ���� �Ǹ� yield return�� ~~ �� ���������� ��ٸ��Ƿ�
            // list ������ �� ������ ���ؼ� Moving�� �ǽ� �� ���������� ��ٸ���.
            Debug.Log("MovingByPath");
            yield return StartCoroutine(Moving(list[curPath++], null));
        }
        yield return null;
    }
}
