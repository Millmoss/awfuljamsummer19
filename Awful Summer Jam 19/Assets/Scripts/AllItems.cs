using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllItems : MonoBehaviour
{
    private static AllItems instance = null;

    public Dictionary<int, Item> items;
    public Sprite[] sprites;

    public void InstantiateValues()
    {
        items = new Dictionary<int, Item>();
        for (int i = 0; i < sprites.Length; i++)
        { 
            Item tmp = new Item(sprites[i]);
            items[i] = tmp;
        }
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
