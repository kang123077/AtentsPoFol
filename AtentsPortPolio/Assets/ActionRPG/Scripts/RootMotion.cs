using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootMotion : MonoBehaviour
{
    public Animator myAnim = null; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Animator의 루트모션을 제어할 수 있음, 자식이 아니라 부모가 움직이도록 하고 싶다.
    private void OnAnimatorMove()
    {
        // 하단의 두 값은 Update에서 사용할 수 없고, 해당 함수에서만 사용 할 수 있다. 다른곳에선 그냥 0
        transform.parent.Translate(myAnim.deltaPosition, Space.World);
        // 쿼터니언은 곱하기 연산만 이용
        transform.parent.rotation *= myAnim.deltaRotation;
    }
}
