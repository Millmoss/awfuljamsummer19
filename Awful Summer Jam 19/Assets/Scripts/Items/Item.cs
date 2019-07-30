using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public Sprite img;
    public string name;
    public ItemTypeEnums.values type;
    public GameObject obj;

    public Item(Sprite s, string nam, ItemTypeEnums.values _type, GameObject o)
    {
        img = s;
        type = _type;
        name = nam;
        obj = o;
    }
    
}
