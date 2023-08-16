using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootMotion : MonoBehaviour
{
    public Animator myAnim = null;
    Vector3 deltaPosition = Vector3.zero;
    Quaternion deltaRotation = Quaternion.identity; // 0도라는 뜻, 기본값

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        // 물리의 경우 0.02, 1초마다 50번 고정적으로 실행되나 update는 아님
        // 따라서 버벅거리는 현상을 없애려면 움직임도 FixedUpdate로 해줘야 함
        transform.parent.position = deltaPosition;
        // transform.parent.rotation = deltaRotation;
        deltaPosition = Vector3.zero;
        deltaRotation = Quaternion.identity;
    }

    // Animator의 루트모션을 제어할 수 있음, 자식이 아니라 부모가 움직이도록 하고 싶다.
    private void OnAnimatorMove()
    {
        /*
        // 하단의 두 값은 Update에서 사용할 수 없고, 해당 함수에서만 사용 할 수 있다. 다른곳에선 그냥 0
        transform.parent.Translate(myAnim.deltaPosition, Space.World);
        // 쿼터니언은 곱하기 연산만 이용
        transform.parent.rotation *= myAnim.deltaRotation;
        */

        // 위와 같이 하지 않고, 매 프레임마다 누적된 값이 FixedUpdate마다 반영되는 방식
        deltaPosition += myAnim.deltaPosition;
        deltaRotation *= myAnim.deltaRotation;
    }
}
