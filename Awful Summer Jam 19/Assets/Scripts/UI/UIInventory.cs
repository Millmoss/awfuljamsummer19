using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    public UIItem[] item_slots;

    public void AddItem(Item i, int cur_empty_slot)
    {
        item_slots[cur_empty_slot].SetItem(i);
    }

    public void RemoveItem(int pos)
    {
        item_slots[pos].RemoveItem();
    }

    public bool ToggleEquip (int pos)
    {
        if (item_slots[pos].equipped == false)
        { 
            item_slots[pos].EquipItem();
            return true;
        }
        else
            item_slots[pos].UnequipItem();
        return false;
    }

    //this is really really badly done, dont look at it!
    public void SelectItem(int pos)
    {
        for(int i=0;i < item_slots.Length;i++)
        {
            if (i != pos)
                item_slots[i].Deselect();
            else
                item_slots[i].Select();
        }
    }
    
}
