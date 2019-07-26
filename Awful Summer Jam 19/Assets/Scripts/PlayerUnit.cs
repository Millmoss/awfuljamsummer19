using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : Unit
{
    public UIBar mana_bar;
    public int cur_wep_atk, max_mana;
	public itemtype selectedItem = itemtype.sword;

    //if hp is 0, don't let it be 0 please.
    private new void Start()
    {
        base.Start();
        mana_bar.Restart(hp);
        max_mana = mana;
    }
    
    //Return the calculated attack value
    public new int GetAttack()
    {
        return cur_wep_atk +  atk;
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
            AddHp(35);
            print(hp);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            AddHp(-35);
            print(hp);
        }
    }
}

public enum itemtype { sword, dagger, torch, none }