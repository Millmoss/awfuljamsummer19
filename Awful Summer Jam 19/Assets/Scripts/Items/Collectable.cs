using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public Inventory iv;
    public Item it;

    private void Start()
    {
        it = AllItems.Instance.items[1];
    }

    // Update is called once per frame
    void Update()
    {
        //TOOD: put this in a collision.
        if (Input.GetKeyDown(KeyCode.N))
            iv.AddItem(it);
    }
}
