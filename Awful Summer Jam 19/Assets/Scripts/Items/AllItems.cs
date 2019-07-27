using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Hi, I'm a singleton. I highlander all copies of me.
public class AllItems : MonoBehaviour
{
    private static AllItems instance = null;

    public Dictionary<int, Item> items;
    public Sprite[] sprites;
    public Sprite[] weapons;

    public void InstantiateValues()
    {
        items = new Dictionary<int, Item>();
        Item tmp;
        tmp = new Armor(sprites[0], "head", ItemTypeEnums.values.head, 1);
        items[0] = tmp;
        tmp = new Armor(sprites[1], "ar", ItemTypeEnums.values.arms, 2);
        items[1] = tmp;
        tmp = new Armor(sprites[2], "lweg", ItemTypeEnums.values.legs, 3);
        items[2] = tmp;
        tmp = new Armor(sprites[3], "che", ItemTypeEnums.values.chest,4);
        items[3] = tmp;
        tmp = new Armor(sprites[4], "boot", ItemTypeEnums.values.boots,5);
        items[4] = tmp;
        /*
        for (int i = 0; i < sprites.Length; i++)
        {
            tmp = new Item(sprites[i], i.ToString(),ItemTypeEnums.values.arms);
            items[i] = tmp;
        }
        tmp = new Item_Weapon(weapons[0], "Sword", ItemTypeEnums.values.cut);
        */
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
