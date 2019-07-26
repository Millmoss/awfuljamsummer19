using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Weapon : Item
{
    public WeaponTypeEnum.WeaponType type { get; set; }
    public Item_Weapon(Sprite s, string nam, WeaponTypeEnum.WeaponType t) : 
        base(s, nam)
    {
        type = t;
    }
}
