using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ActionPlayer : BattleSystem
{
    public LayerMask enemyMask;
    public Transform myWeapon;
    bool IsComboChecking = false;
    int clickCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        changeHp += SceneData.inst.playerHpUI.UpdateValue;
        base.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        myAnim.SetFloat("x", x);
        myAnim.SetFloat("y", y);

        // 이것은 event가 동작하는 UI위에 마우스가 올라가있다는 뜻.
        if (!EventSystem.current.IsPointerOverGameObject())
        {
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

    public override void OnAttack()
    {
        Collider[] list = Physics.OverlapSphere(myWeapon.position, 1.0f, enemyMask);
        foreach (Collider col in list)
        {
            IBattle ib = col.GetComponent<IBattle>();
            if (ib != null)
            {
                ib.OnDamage(50.0f);
            }
        }
    }
}
