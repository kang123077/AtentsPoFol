using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // 함수에 해당되는 부분이 Code 영역에 저장 된다
    // 코드에 저장 되기 때문에 delegate와 같은 구조에 함수를 저장할 수 있다.
    // 이것은 컴파일이 될 때 메모리를 할당받고, 등록된다.
    // Data영역에는 정적(Static) 변수, 상수가 저장된다.
    // const가 아닌 문자열, 직접 적은 것들을 리터럴 상수라고 한다. >> data영역
    // 얘네도 컴파일 단계에서 메모리 크기가 결정된다 (런타임중에 변경 X)
    // Stack : 블록 스코프 안에 존재하는 지역변수. 무조건 그렇지는 않다.
    // Heap : 인스턴스가 저장되는 영역 ex ) new vector3 ... 블록스코프 안에 있어도 Heap에 저장된다.
    // 인스턴스는 반드시 참조를 해야 사용할 수 있는 참조형 변수인데, 참조자가 사라지면 찾을 수 없다.
    // 이와 같은 것들을 C#에서는 가비지라고 부르고, C++에서는 커스텀 가능하다.
    // 그러나 C++에선 이를 제대로 처리하지 않을 경우 메모리 누수가 일어났기떄문에
    // 생산성 등을 이유로 C#에서는 GC라는 개념을 사용하기로 한 것.
    // 그러나 GC는 우리가 제어할 수 없기 때문에 엄밀함은 조금 떨어진다.

    // 떄문에 GC를 사용 할 때 가장 효과적인 방법은 가비지를 발생시키지 않는 것이다.
    // 물체를 생성할 때 Instantiate가 아닌 오브젝트풀을 사용함으로서 이 부분을 줄일 수 있지만
    // 자주 사용하는 것들에 대해서 한정해야 한다.


    bool isFire = false;
    float dist = 0.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isFire)
        {
            float delta = 100.0f * Time.deltaTime;
            transform.Translate(Vector3.forward * delta);
            dist += delta;
            if (dist > 100.0f)
            {
                ObjectPool.inst.ReleaseObject<Bullet>(gameObject);
            }
        }
    }

    public void OnFire()
    {
        transform.localPosition = Vector3.zero;
        dist = 0.0f;
        transform.SetParent(null);
        isFire = true;
    }
}
