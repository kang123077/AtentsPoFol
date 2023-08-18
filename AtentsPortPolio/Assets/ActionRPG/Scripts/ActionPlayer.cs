using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPlayer : CharacterProperty
{
    public LayerMask enemyMask;
    public Transform myWeapon;
    bool IsComboChecking = false;
    int clickCount = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        myAnim.SetFloat("x", x);
        myAnim.SetFloat("y", y);

        if (IsComboChecking)
        {
            if (Input.GetMouseButtonDown(0))
            {
                clickCount++;
            }
        }
        else
        {
            if (!myAnim.GetBool("isAttacking") && Input.GetMouseButtonDown(0))
            {
                myAnim.SetTrigger("Attack");
            }
        }
    }

    public void ComboCheckStart()
    {
        clickCount = 0;
        myAnim.SetBool("ComboStop", false);
        IsComboChecking = true;
    }

    public void ComboCheckEnd()
    {
        IsComboChecking = false;
        if (clickCount == 0)
        {
            myAnim.SetBool("ComboStop", true);
        }
    }

    public void OnAttack()
    {
        Collider[] list = Physics.OverlapSphere(myWeapon.position, 1.0f, enemyMask);
        foreach (Collider col in list)
        {
            IBattle ib = col.GetComponent<IBattle>();
            if (ib != null)
            {
                ib.OnDamage(0.5f);
            }
        }
    }
}
