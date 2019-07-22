using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Display : MonoBehaviour
{
    bool near_player = false;
    public GameObject text_name;
    public Transform cam;

    private void Display()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
            Display();
        text_name.transform.LookAt(cam,Vector3.up);
    }
}
