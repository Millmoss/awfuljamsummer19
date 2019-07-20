using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public int atk, def, hp, mana;
    protected int max_hp, max_mana;

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
