﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerUnit : Unit
{
    public UIBar mana_bar;
    public Inventory inv;
    public int cur_wep_atk, max_mana;
    private Dictionary<ItemTypeEnums.values, Armor> equips;

    //if hp is 0, don't let it be 0 please.
    private new void Start()
    {
        base.Start();
        mana_bar.Restart(hp);
        max_mana = mana;
        
        equips = new Dictionary<ItemTypeEnums.values, Armor>()
        {
            {
                ItemTypeEnums.values.head, null
            },
            {
                ItemTypeEnums.values.chest, null
            },
            {
                ItemTypeEnums.values.arms, null
            },
            {
                ItemTypeEnums.values.legs, null
            },
            {
                ItemTypeEnums.values.boots, null
            }
        };

    }

    public void AddArmor(Armor a)
    {
        if (equips.ContainsKey(a.type))
            if (equips[a.type] == null)
            {
                equips[a.type] = a;
                def += a.armor_amount;
            }
            else
                print("alread have thigne quip");
    }

    public void RemoveArmor(Armor a)
    {
        if (equips.ContainsKey(a.type))
            if (equips[a.type] != null)
            {
                equips[a.type] = null;
                def -= a.armor_amount;
                inv.RemoveItem(a);
            }
    }

    public void BeginErodeArmor()
    {
        InvokeRepeating("ErodeArmor", 60, 60);
    }

    private void ErodeArmor()
    {
        bool all_null = true;
        foreach(ItemTypeEnums.values v in equips.Keys)
        {
            if (equips[v] != null)
            {
                all_null = false;
                def -= equips[v].armor_amount;
                equips[v] = null;
                return;
            }
        }
        if (all_null)
            CancelInvoke("ErodeArmor");
    }
    
    //Return the calculated attack value
    public new int GetAttack()
    {
        return cur_wep_atk +  atk;
    }
    public void AddMana(int val)
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
