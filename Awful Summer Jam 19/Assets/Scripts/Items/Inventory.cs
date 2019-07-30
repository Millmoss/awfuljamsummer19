using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public UIInventory iv;
    public PlayerUnit plyr;
    private int max_items = 10;
    private int cur_empty_slot = 0, cur_item_pos;
    private Item[] cur_items;
    public string cur_eapon_value;
    private bool selected = false;

    private void Start()
    {
        max_items = iv.item_slots.Length;
        cur_items = new Item[max_items];
        cur_eapon_value = "no";
    }

    //Done w movign l/r to iterate through items
    public void FinishSelecting()
    {
        selected = false;
        iv.SelectItem(-1);
    }

    public void MoveSelection(bool isLeft)
    {
        if(!selected)
            selected = true;
        if (!isLeft)
        { 
            if (cur_item_pos + 1 >= max_items)
                cur_item_pos = 0;
            else
                cur_item_pos += 1;
        }
        else
        {
            if (cur_item_pos - 1 <0)
                cur_item_pos = max_items - 1;
            else
                cur_item_pos -= 1;
        }
        iv.SelectItem(cur_item_pos);
    }

    //Press once to begin looking through items; twice to selectthe curent items elected.
    public void SelectItem()
    {
        if (!selected)
        { 
            selected = true;
            iv.SelectItem(cur_item_pos);
        }
        else
        {
            if (cur_items[cur_item_pos] == null)
                return;
            switch(cur_items[cur_item_pos].type)
            {
                case ItemTypeEnums.values.sword:
                    cur_eapon_value = "swd";
                    break;
                case ItemTypeEnums.values.dagger:
                    cur_eapon_value = "DAG";
                    break;
                case ItemTypeEnums.values.torch:
                    cur_eapon_value = "fir e";
                    break;

                case ItemTypeEnums.values.consumable:
                    RemoveItem(cur_item_pos);
                    break;
                //NOTe: This is badly impelemnted. FUTURE: One tag for 'armor',
                //One for what slot it goe si in.
                case ItemTypeEnums.values.arms:
                    ToggleArmor();
                    break;
                case ItemTypeEnums.values.legs:
                    ToggleArmor();
                    break;
                case ItemTypeEnums.values.chest:
                    ToggleArmor();
                    break;
                case ItemTypeEnums.values.head:
                    ToggleArmor();
                    break;
                case ItemTypeEnums.values.boots:
                    ToggleArmor();
                    break;
            }
        }
    }
    
    //Two methods; one for the items position and one for the igven item
    public void RemoveItem(int pos)
    {
        iv.RemoveItem(pos);
        cur_items[pos] = null;
        for (int i = 0; i < cur_items.Length; i++)
        {
            if (cur_items[i] == null)
            {
                cur_empty_slot = i;
                return;
            }
        }
        print("ERROR: Tried to remove an item but have no free space!");
    }

    public void ToggleArmor()
    {
        if(plyr.CanEquipArmor((Armor)cur_items[cur_item_pos]))
        {
            print("put on");
            iv.ToggleEquip(cur_item_pos, cur_items[cur_item_pos]);
            plyr.AddArmor((Armor)cur_items[cur_item_pos]);
            return;
        }
            
    }

    public void RemoveItem(Item itm)
    {
        for(int i=0;i < cur_items.Length;i++)
            if(cur_items[i] == itm)
            {
                RemoveItem(i);
                return;
            }
        print("ERROR: Attmpeted removed item not found in inventory!");
    }

    //TODOO: Fix so when an item is removed in the middle of the
    //inventory stack it doesn't fuck EVERYHTING.
    public void AddItem(Item itm)
    {
        if (cur_empty_slot < max_items)
        {
            iv.AddItem(itm, cur_empty_slot);
            cur_items[cur_empty_slot] = itm;
            bool changed = false;
            for (int i = 0; i < cur_items.Length; i++)
            {
                if (cur_items[i] == null)
                {
                    changed = true;
                    cur_empty_slot = i;
                    break;
                }
            }
            if (!changed)
                cur_empty_slot = 99;
        }
        else
        {
            print("ERROR! TOO MANY ITEMS");
        }
    }
}
