using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // �Լ��� �ش�Ǵ� �κ��� Code ������ ���� �ȴ�
    // �ڵ忡 ���� �Ǳ� ������ delegate�� ���� ������ �Լ��� ������ �� �ִ�.
    // �̰��� �������� �� �� �޸𸮸� �Ҵ�ް�, ��ϵȴ�.
    // Data�������� ����(Static) ����, ����� ����ȴ�.
    // const�� �ƴ� ���ڿ�, ���� ���� �͵��� ���ͷ� ������ �Ѵ�. >> data����
    // ��׵� ������ �ܰ迡�� �޸� ũ�Ⱑ �����ȴ� (��Ÿ���߿� ���� X)
    // Stack : ��� ������ �ȿ� �����ϴ� ��������. ������ �׷����� �ʴ�.
    // Heap : �ν��Ͻ��� ����Ǵ� ���� ex ) new vector3 ... ��Ͻ����� �ȿ� �־ Heap�� ����ȴ�.
    // �ν��Ͻ��� �ݵ�� ������ �ؾ� ����� �� �ִ� ������ �����ε�, �����ڰ� ������� ã�� �� ����.
    // �̿� ���� �͵��� C#������ ��������� �θ���, C++������ Ŀ���� �����ϴ�.
    // �׷��� C++���� �̸� ����� ó������ ���� ��� �޸� ������ �Ͼ�⋚����
    // ���꼺 ���� ������ C#������ GC��� ������ ����ϱ�� �� ��.
    // �׷��� GC�� �츮�� ������ �� ���� ������ �������� ���� ��������.

    // ������ GC�� ��� �� �� ���� ȿ������ ����� �������� �߻���Ű�� �ʴ� ���̴�.
    // ��ü�� ������ �� Instantiate�� �ƴ� ������ƮǮ�� ��������μ� �� �κ��� ���� �� ������
    // ���� ����ϴ� �͵鿡 ���ؼ� �����ؾ� �Ѵ�.


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
