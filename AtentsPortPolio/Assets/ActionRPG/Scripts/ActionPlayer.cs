using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPlayer : CharacterProperty
{
    public LayerMask enemyMask;
    public Transform myWeapon;
    bool isComboChecking = false;
    int clickCount = 0;

    void Start()
    {

    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        myAnim.SetFloat("x", x);
        myAnim.SetFloat("y", y);

        if (isComboChecking)
        {
            if (Input.GetMouseButton(0))
            {
                clickCount++;
            }

        }
        else
        {
            if (Input.GetMouseButton(0))
            {
                myAnim.SetTrigger("Attack");
            }
        }
    }

    public void ComboCheckStart()
    {
        clickCount = 0;
        myAnim.SetBool("ComboStop", false);
        isComboChecking = true;
    }

    public void ComboCheckEnd()
    {
        isComboChecking = false;
        if(clickCount == 0)
        {
            myAnim.SetBool("ComboStop", true);
        }
    }

    public void OnAttack()
    {
        // OverlapSphere �� ���� �ش� ��ġ�� ������ collider�� ����� ����üũ�� �� �� ����ش�
        // �ش� ����� ��� Rigidbody�� ��� ��� �����ϴ�

        Collider[] list = Physics.OverlapSphere(myWeapon.position, 1.0f, enemyMask);
        foreach(Collider col in list)
        {
            IBattle ib = col.GetComponent<IBattle>();
            if(ib != null)
            {
                ib.OnDamage();
            }
        }
    }
}
