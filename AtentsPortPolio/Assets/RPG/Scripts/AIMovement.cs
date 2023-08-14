using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// NavMesh는 편리하지만, 일반적으로 무겁기때문에 현직에서는 일부만 따로 사용
public class AIMovement : CharacterMovement
{
    NavMeshPath myPath = null;
    public void MoveToPosByNav(Vector3 pos, MyAction act)
    {
        StopAllCoroutines();
        if (myPath == null) myPath = new NavMeshPath();
        // NavMesh.AllAreas : 일종의 LayerMask, 각 지형의 종류마다 비용을 설정해서 최소값을 산정하도록 할 수 있다.
        if (NavMesh.CalculatePath(transform.position, pos, NavMesh.AllAreas, myPath))
        {
            switch (myPath.status)
            {
                case NavMeshPathStatus.PathComplete: // 성공
                case NavMeshPathStatus.PathPartial: // 가다 막힘
                    StartCoroutine(MovingByPath(myPath.corners));
                    break;
                case NavMeshPathStatus.PathInvalid: // 실패
                    break;
            }
        }
    }

    // list : 경로의 정점을 받아온다
    IEnumerator MovingByPath(Vector3[] list)
    {
        int curPath = 1;
        while (curPath < list.Length)
        {
            // 코루틴에서 yield return시 StartCoroutine(Moving(list[curPath++], null));
            // 를 쓰게 되면 yield return이 ~~ 이 끝날때까지 기다리므로
            // list 내부의 각 정점에 대해서 Moving을 실시 및 끝날때까지 기다린다.
            Debug.Log("MovingByPath");
            yield return StartCoroutine(Moving(list[curPath++], null));
        }
        yield return null;
    }
}
