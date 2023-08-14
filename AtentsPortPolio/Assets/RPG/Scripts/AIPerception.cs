using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AIPerception : MonoBehaviour
{
    public LayerMask enemyMask;
    public Transform myTarget = null;

    // public Monster ... 와 같은 커플링이 일어나지 않도록 델리게이트 사용
    // 적을 발견했을 때 실행해야 할 함수를 하단에 있는 델리게이트에 등록
    public UnityEvent findEnemy;
    public UnityEvent lostEnemy;

    private void OnTriggerEnter(Collider other)
    {
        // Layer마스크만 적용되도록
        if ((1 << other.gameObject.layer & enemyMask) != 0)
        {
            if (myTarget == null)
            {
                // 처음 적을 발견
                myTarget = other.transform;
                findEnemy?.Invoke();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((1 << other.gameObject.layer & enemyMask) != 0)
        {
            // 지금 내 타겟이 추적 영역을 벗어남
            if (myTarget == other.transform)
            {
                // 공격중인 적을 놓쳤을 때
                myTarget = null;
                lostEnemy?.Invoke();
            }
        }
    }
}
