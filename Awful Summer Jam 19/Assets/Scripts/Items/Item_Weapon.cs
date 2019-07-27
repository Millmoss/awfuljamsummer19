using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Weapon : Item
{
    public ItemTypeEnums.values item_type { get; set; }
    public Item_Weapon(Sprite s, string nam, ItemTypeEnums.values t) : 
        base(s, nam, t)
    {
        type = t;
    }
}
