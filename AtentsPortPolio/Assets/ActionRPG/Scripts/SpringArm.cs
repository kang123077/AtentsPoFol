using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringArm : MonoBehaviour
{
    public LayerMask crashMask;
    public float rotSpeed = 360.0f;
    // 이전엔 -60, 80이었음
    public Vector2 lookUpRange = new Vector2(-30, 80);
    public Vector2 zoomRange = new Vector2(1, 8);
    public float camDist = 0.0f;
    float targetDist = 0.0f;
    void Start()
    {
        // 카메라와 SpringArm의 사이간격
        targetDist = camDist = Mathf.Abs(Camera.main.transform.localPosition.z);
    }

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            float vertical = -Input.GetAxis("Mouse Y");
            // 다 좋은데 360도 돌아가버리니까 문제다. 이걸 막아줘야 하지만 현재 각을 알아야 한다.
            Vector3 rot = transform.localRotation.eulerAngles;
            // Quaternion값은 변환했을 때 음수값이 나오지 않는다. -60`라고 하면 300`인 셈이다.
            // 그럼 어떻게 할까? 음수값을 필요로 하는 경우 300` - 360`로 처리한다.
            if (rot.x > 180.0f) rot.x -= 360.0f;
            rot.x += vertical * rotSpeed * Time.deltaTime;
            rot.x = Mathf.Clamp(rot.x, lookUpRange.x, lookUpRange.y);
            // 로컬회전값을 바꿔야하기때문에 localRotation으로 처리
            transform.localRotation = Quaternion.Euler(rot);

            float horizontal = Input.GetAxis("Mouse X");
            // 좌우 회전의 경우 자기 자신의 부모를 회전시킬 예정
            transform.parent.Rotate(Vector3.up * horizontal * rotSpeed * Time.deltaTime, Space.World);
        }

        targetDist -= Input.GetAxis("Mouse ScrollWheel") * 3.0f;
        targetDist = Mathf.Clamp(targetDist, zoomRange.x, zoomRange.y);

        // ray는 world에서 체크하는 것이기 때문에 로컬로 X
        Ray ray = new Ray(transform.position, -transform.forward);
        // 카메라가 lerp 3.0f의 값으로 보간이동하도록
        camDist =  Mathf.Lerp(camDist, targetDist, Time.deltaTime * 3.0f);
        // 카메라를 감싸는 가상의 구 (카메라의 크기?) 가 있다고 가정
        float offset = 1f;
        if (Physics.Raycast(ray, out RaycastHit hit, camDist + offset, crashMask))
        {
            camDist = hit.distance - offset;
        }
        Camera.main.transform.localPosition = new Vector3(0, 0, -camDist);
    }
}
