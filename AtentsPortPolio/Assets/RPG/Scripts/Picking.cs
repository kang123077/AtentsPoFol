using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Picking : MonoBehaviour
{
    public Animator myAnim;
    // LayerMask는 32비트 운영체제에서 비트 형태로 사용되기 때문에 총 nothing 포함 32개의 레이어 마스크만 제공한다.
    public LayerMask clickMask;
    public LayerMask objectMask;

    // 코루틴 함수를 참조하는 변수
    IEnumerator move = null;

    // StartCoroutine 을 사용해도 내부적으로 가비지가 발생하지만, 참조형 변수이기 때문에
    // 작은 값의 가비지만 생성되서 상대적으로 효율적이다.
    // Coroutine coMove = null;

    // 아래와 같이 사용하게 되면 커플링 형태가 되어 객체지향을 위반하게 된다.
    // public CharacterMovement characterMovement = null;

    // 아래에서는 커플링을 피하기 위해 델리게이트 구조를 사용한다.
    // 하단의 코드들은 code 메모리 영역에 저장되는데, 델리게이트는 일종의 code 메모리 영역의 함수참조자라고 볼 수 있다.
    // UnityEvent는 다른 함수를 연동할 수 있도록 지원한다.
    // 아래는 제네릭값으로 Vector3값과 MyAction을 가지는 델리게이트 함수
    public UnityEvent<Vector3, MyAction> clickActs;
    // 하단에는 오브젝트를 클릭했을 때 사용 될 함수가 등록됨
    public UnityEvent<Transform> objectActs;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, clickMask | objectMask))
            {
                // if (clickActs != null) clickActs.Invoke(hit.point);
                // ? : null 검사, 상단과 동일한 코드
                // 델리게이트에 참조되어 있는 함수를 실행하도록 만든다.
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

                // 코루틴 참조 변수에 코루틴과 매개변수를 할당
                // move = Moving(hit.point);
            }

            if (move != null)
            {
                // 아래와 같이 사용하게 되면, yield return까지 실행한다.
                // 특수한 경우 사용할 수 있으며, 일반적으론 StartCoroutine으로 동작하면 된다.
                // move.MoveNext();
            }

            // 코루틴이 아래와 같이 Update내에서 조건문 없이 사용 될 경우, 오버로딩과 같은 문제를 일으킨다.
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

    // Stack 영역에서 생성 된 변수들은 참조자가 사라지면 휘발되어버린다. 따라서 가비지가 발생하지 않는다.
    // 그러나 Heap 영역에 있는 배열(C#), 인스턴스는 참조자가 사라지면 휘발되지 않고 가비지가 발생한다.
    // 이렇게 생긴 가비지가 일정 이상 비율로 쌓이게 되면 GC가 실행 되며, 가비지를 청소하는데 이 때 오버로딩이 발생한다.
    // 왜냐하면 GC가 사용 되는 중에는 다른 어떤 행동도 시스템이 하지 않기 때문이며, 이 찰나는 순식간이지만 다수 발생하게 되면 문제가 된다.

    // 코루틴의 장점 중 하나는, 이러한 특성을 이용하여 코루틴 내에서 변수를 지역변수(Stack영역)로 생성하며 가비지 발생을 줄일 수 있다.

    // 객체지향 Solid 5원칙
    // 단일책임의 원칙 : 각 클래스와 함수는 각각 하나의 책임만 가진다.
}