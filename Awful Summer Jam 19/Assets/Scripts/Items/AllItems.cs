using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Hi, I'm a singleton. I highlander all copies of me.
public class AllItems : MonoBehaviour
{
    private static AllItems instance = null;

    public Dictionary<int, Item> items;
    public Sprite[] sprites;
    public GameObject[] objs;
    public Sprite[] weapons;

    public void InstantiateValues()
    {
        items = new Dictionary<int, Item>();
        Item tmp;
        tmp = new Armor(sprites[0], "head", ItemTypeEnums.values.head, 1, objs[0]);
        items[0] = tmp;
        tmp = new Armor(sprites[1], "shirt", ItemTypeEnums.values.arms, 2, objs[1]);
        items[1] = tmp;
        tmp = new Armor(sprites[2], "chest", ItemTypeEnums.values.legs, 3, objs[2]);
        items[2] = tmp;
        tmp = new Armor(sprites[3], "pants", ItemTypeEnums.values.chest,4, objs[3]);
        items[3] = tmp;
        tmp = new Armor(sprites[4], "shoulderguard", ItemTypeEnums.values.boots,5, objs[4]);
        items[4] = tmp;
        
        tmp = new Item(weapons[0], "Sword", ItemTypeEnums.values.sword, null);
        tmp = new Item(weapons[1], "Sword", ItemTypeEnums.values.dagger, null);
        
    }

    public static AllItems Instance
    {
        get
        {
            return instance;
        }
    }
    
    private void Awake()
    {
        // if the singleton hasn't been initialized yet
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        instance = this;
        InstantiateValues();
        DontDestroyOnLoad(gameObject);
    }
}
