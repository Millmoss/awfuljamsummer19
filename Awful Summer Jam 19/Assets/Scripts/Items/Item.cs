using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public Sprite img;
    public string name;
    public ItemTypeEnums.values type;

    public Item(Sprite s, string nam, ItemTypeEnums.values _type)
    {
        img = s;
        type = _type;
        name = nam;
    }
    
}
