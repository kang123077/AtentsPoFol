using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // ¸µÅ©?
    public GameObject orgBullet;
    public GameObject orgBomb;
    void Start()
    {
        
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ObjectPool.inst.GetObject<Bullet>(orgBullet, transform).GetComponent<Bullet>().OnFire();
        }
        if (Input.GetKeyDown(KeyCode.F1))
        {
            ObjectPool.inst.GetObject<Bomb>(orgBomb, transform).GetComponent<Bomb>().OnFire();
        }
    }
}
