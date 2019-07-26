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
        for (int i = 0; i < sprites.Length; i++)
        {
            tmp = new Item(sprites[i], i.ToString());
            items[i] = tmp;
        }
        tmp = new Item_Weapon(weapons[0], "Sword", WeaponTypeEnum.WeaponType.cut);
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
