using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public Rigidbody myRigid;
    public float power;

    void Start()
    {
    }

    void Update()
    {
        if (transform.position.y < -1.0f)
        {
            ObjectPool.inst.ReleaseObject<Bomb>(gameObject);
        }
    }

    public void OnFire()
    {
        // �θ� ���� 0, 0, 0 ���� ������ �ϸ� �Ǹ� �θ� ������ �� ���־�� �Ѵ�.
        transform.localPosition = Vector3.zero;
        myRigid.velocity = Vector3.zero;
        transform.SetParent(null);
        myRigid.AddForce(new Vector3(0, 1, 1) * power);
    }
}
