using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringArm : MonoBehaviour
{
    public LayerMask crashMask;
    public float rotSpeed = 360.0f;
    // ������ -60, 80�̾���
    public Vector2 lookUpRange = new Vector2(-30, 80);
    public Vector2 zoomRange = new Vector2(1, 8);
    public float camDist = 0.0f;
    float targetDist = 0.0f;
    void Start()
    {
        // ī�޶�� SpringArm�� ���̰���
        targetDist = camDist = Mathf.Abs(Camera.main.transform.localPosition.z);
    }

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            float vertical = -Input.GetAxis("Mouse Y");
            // �� ������ 360�� ���ư������ϱ� ������. �̰� ������� ������ ���� ���� �˾ƾ� �Ѵ�.
            Vector3 rot = transform.localRotation.eulerAngles;
            // Quaternion���� ��ȯ���� �� �������� ������ �ʴ´�. -60`��� �ϸ� 300`�� ���̴�.
            // �׷� ��� �ұ�? �������� �ʿ�� �ϴ� ��� 300` - 360`�� ó���Ѵ�.
            if (rot.x > 180.0f) rot.x -= 360.0f;
            rot.x += vertical * rotSpeed * Time.deltaTime;
            rot.x = Mathf.Clamp(rot.x, lookUpRange.x, lookUpRange.y);
            // ����ȸ������ �ٲ���ϱ⶧���� localRotation���� ó��
            transform.localRotation = Quaternion.Euler(rot);

            float horizontal = Input.GetAxis("Mouse X");
            // �¿� ȸ���� ��� �ڱ� �ڽ��� �θ� ȸ����ų ����
            transform.parent.Rotate(Vector3.up * horizontal * rotSpeed * Time.deltaTime, Space.World);
        }

        targetDist -= Input.GetAxis("Mouse ScrollWheel") * 3.0f;
        targetDist = Mathf.Clamp(targetDist, zoomRange.x, zoomRange.y);

        // ray�� world���� üũ�ϴ� ���̱� ������ ���÷� X
        Ray ray = new Ray(transform.position, -transform.forward);
        // ī�޶� lerp 3.0f�� ������ �����̵��ϵ���
        camDist =  Mathf.Lerp(camDist, targetDist, Time.deltaTime * 3.0f);
        // ī�޶� ���δ� ������ �� (ī�޶��� ũ��?) �� �ִٰ� ����
        float offset = 1f;
        if (Physics.Raycast(ray, out RaycastHit hit, camDist + offset, crashMask))
        {
            camDist = hit.distance - offset;
        }
        Camera.main.transform.localPosition = new Vector3(0, 0, -camDist);
    }
}
