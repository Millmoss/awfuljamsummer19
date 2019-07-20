using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : Unit
{
    public UIBar mana_bar;
    public UIHP hp_bar;
    //if hp is 0, don't let it be 0 please.
    private void Start()
    {
        hp_bar.Restart(hp);
        mana_bar.Restart(hp);
        max_hp = hp;
        max_mana = mana;
        print(hp);
    }

    public new void AddHp(int val)
    {
        hp += val;
        if (hp > max_hp)
            hp = max_hp;
        if (hp < 0)
            hp = 0;
        hp_bar.UpdateBar(hp);
    }

    public new void AddMana(int val)
    {
        mana += val;
        if (mana > max_mana)
            mana = max_mana;
        if (mana < 0)
            mana = 0;
    }

    private void Update()
    { 
        if (Input.GetKeyDown(KeyCode.Q))
        {
            AddHp(1);
            print(hp);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            AddHp(-1);
            print(hp);
        }
    }
}
