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
        // 부모 기준 0, 0, 0 으로 가도록 하면 되며 부모 설정을 잘 해주어야 한다.
        transform.localPosition = Vector3.zero;
        myRigid.velocity = Vector3.zero;
        transform.SetParent(null);
        myRigid.AddForce(new Vector3(0, 1, 1) * power);
    }
}
