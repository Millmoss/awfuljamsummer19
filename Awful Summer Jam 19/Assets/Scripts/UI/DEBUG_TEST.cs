using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

//DEBUG PURPOSES ONLY
public class DEBUG_TEST : MonoBehaviour
{
    public Text txt;
    public PlayerUnit pl;
    public Inventory pl_i;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            string x = "ADD.1";
            try
            {
                if (x.Substring(0, 4).ToUpper() == "ADD.")
                {
                    pl_i.AddItem(
                        AllItems.Instance.items[int.Parse(x.Substring(4, 1))]);
                }
            }
            catch
            {
                print("DEBUG : ERROR! INVALID COMMAND!");
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            pl_i.SelectItem();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            pl_i.MoveSelection(true);
        if (Input.GetKeyDown(KeyCode.RightArrow))
            pl_i.MoveSelection(false);
    }
}
