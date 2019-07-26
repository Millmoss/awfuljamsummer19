using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStatus : MonoBehaviour
{
    public Image icon;
    private void Awake()
    {
        icon.enabled = false;
    }

    //Rn just the poison icon
    public Sprite status_poison;
    public void UpdateStatus(UnitStatusEnums.UnitStatus status)
    {
        if(status == UnitStatusEnums.UnitStatus.poisoned)
        {
            icon.enabled = true;
            icon.sprite = status_poison;
        }
    }

    private void Update()
    {
    }
}
