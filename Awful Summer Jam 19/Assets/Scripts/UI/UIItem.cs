using UnityEngine;
using UnityEngine.UI;

public class UIItem : MonoBehaviour
{
    public Inventory inv;
    public Image icon, dur, border;
    public int max_val = 60;
    private float cur_val;
    private bool selected = false;
    public bool equipped { get; private set; } = false;
    private Color init_col, border_init_col;
    public Color border_equip_col;

    private void Start()
    {
        cur_val = max_val;
        init_col = icon.color;
        border_init_col = border.color;
        border.enabled = false;
        dur.enabled = false;
        icon.enabled = false;
    }

    public void EquipItem()
    {
        border.color = border_equip_col;
        equipped = true;
        dur.enabled = true;
    }

    public void UnequipItem()
    {
        border.color = border_init_col;
        equipped = false;
    }

    public void Select()
    {
        selected = true;
        border.enabled = true;
    }

    public void Deselect()
    {
        selected = false;
        border.enabled = false;
    }

    public void RemoveItem()
    {
        icon.sprite = null;
        border.color = border_init_col;
        icon.enabled = false;
    }

    public void SetItem(Item i)
    {
        icon.sprite = i.img;
        cur_val = max_val;
        icon.color = init_col;
        dur.enabled = false;
        dur.fillAmount = 1;
        icon.enabled = true;
    }

    private void Update()
    {
        if(equipped)
        { 
            cur_val -= Time.deltaTime;
            dur.fillAmount = cur_val / max_val;
            icon.color = Color.Lerp(init_col, 
                Color.red, 
                (1 - cur_val / max_val));
        }
        if(cur_val <0)
        {
            RemoveItem();
            Start();
        }
    }
}
