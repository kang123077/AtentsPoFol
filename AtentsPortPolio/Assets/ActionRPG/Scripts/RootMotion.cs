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

    // Animator�� ��Ʈ����� ������ �� ����, �ڽ��� �ƴ϶� �θ� �����̵��� �ϰ� �ʹ�.
    private void OnAnimatorMove()
    {
        // �ϴ��� �� ���� Update���� ����� �� ����, �ش� �Լ������� ��� �� �� �ִ�. �ٸ������� �׳� 0
        transform.parent.Translate(myAnim.deltaPosition, Space.World);
        // ���ʹϾ��� ���ϱ� ���길 �̿�
        transform.parent.rotation *= myAnim.deltaRotation;
    }
}
