﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Similar to UIBar, but we have an "impact" bar as bar.
//bar_hp is what gets immediately changed first.
public class UIHP : UIBar
{
    public Transform bar_hp;
    public float time_before_decrease;

    //Apparently method hiding is a bad idea? IDK if protected works here, too lazy
    //to find out rn tbh.
    public new void UpdateBar(int new_val)
    {
        
        if (new_val < end_val)
        {
            SetBarSize(bar_hp, new_val);
            Invoke("StartDecay", time_before_decrease);
        }
        else
            StartDecay();
        end_val = new_val;
    }
    
    private void StartDecay()
    {
        moving = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            UpdateBar(1);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            UpdateBar(99);
        }
        if (moving)
        {
            if (cur_val == end_val)
                moving = false;
            else
            {
                if (cur_val < end_val)
                {
                    cur_val += rate * Time.deltaTime;
                    SetBarSize(bar_hp, cur_val);
                    SetBarSize(bar, cur_val);
                }
                else
                {
                    cur_val -= rate * Time.deltaTime;
                    SetBarSize(bar, cur_val);
                }
            }
        }
    }
}
