using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStatus : MonoBehaviour
{
    public Image[] icons;
    public int next_free_icon = 0;

    private void Awake()
    {
        foreach(Image i in icons)
            i.enabled = false;
    }

    //Rn just the poison icon
    public Sprite status_poison;
    public void UpdateStatus(UnitStatusEnums.UnitStatus status)
    {
        if(next_free_icon < icons.Length)
            if(status == UnitStatusEnums.UnitStatus.poisoned)
            {
                icons[next_free_icon].enabled = true;
                icons[next_free_icon].sprite = status_poison;
            }
        next_free_icon++;
    }
}
