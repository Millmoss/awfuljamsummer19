using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public int atk, def, hp, mana;
    protected int max_hp;
    
    public UIHP hp_bar;
    public UIStatus ui_status;
    protected UnitStatusEnums.UnitStatus status;

    public void Start()
    {
        hp_bar.Restart(hp);
        max_hp = hp;
    }

    public void SetStatus(UnitStatusEnums.UnitStatus st)
    {
        status = st;
        ui_status.UpdateStatus(st);
        if (st == UnitStatusEnums.UnitStatus.poisoned)
            InvokeRepeating("TickPoison", 1, 1);
        else
            CancelInvoke();
    }

    //Return the calculated attack value
    public int GetAttack()
    {
        return atk;
    }

    //Use this and private the AddHp / AddMana functions.
    public void Hurt(int val)
    {
        if (val - def < 0)
            AddHp(-1);
        else
           AddHp(val - def);
    }

    public void AddHp(int val)
    {
        hp += val;
        if (hp > max_hp)
            hp = max_hp;
        if (hp < 0)
            hp = 0;
        hp_bar.UpdateBar(hp);
        if (hp == 0)
            SetStatus(UnitStatusEnums.UnitStatus.dead);
    }

    private void TickPoison()
    {
        AddHp(-1);
    }

}
