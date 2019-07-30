using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : Item
{
    public int armor_amount { get; set; }

    public Armor(Sprite s, string nam, ItemTypeEnums.values type, int amt, GameObject o) 
        : base(s, nam, type, o)
    {
        armor_amount = amt;
    }

}
