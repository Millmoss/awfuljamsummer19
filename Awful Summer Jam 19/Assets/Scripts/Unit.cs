using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public int atk, def, hp, mana;
    protected int max_hp, max_mana;
    public int cur_wep_atk;

    //Return the calculated attack value
    public int GetAttack()
    {
        return cur_wep_atk + atk;
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
    }

    public void AddMana(int val)
    {
        mana += val;
        if (mana > max_mana)
            mana = max_mana;
        if (mana < 0)
            mana = 0;
    }
}
