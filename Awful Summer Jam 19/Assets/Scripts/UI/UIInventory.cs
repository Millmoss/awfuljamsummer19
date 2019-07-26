using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public UIItem[] item_slots;
    private int cur_empty_slot = 0;

    public void AddItem(Item i)
    {
        item_slots[cur_empty_slot].icon.sprite = i.img;
    }
    
}
