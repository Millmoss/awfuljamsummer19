using UnityEngine;
using UnityEngine.UI;

public class UIItem : MonoBehaviour
{
    public Inventory inv;
    public Image icon, dur, border;
    private ItemTypeEnums.values slot;
    public int max_val = 60;
    private float cur_val;
    private bool selected = false;
    public bool equipped { get; set; } = false;
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
        slot = ItemTypeEnums.values.consumable;
    }

    public void EquipItem(ItemTypeEnums.values t)
    {
        border.color = border_equip_col;
        equipped = true;
        print("E");
        slot = t;
        dur.enabled = true;
    }

    public void UnequipItem()
    {
        border.color = border_init_col;
        print("F");
        slot = ItemTypeEnums.values.consumable;
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
        if(inv.plyr.equips.ContainsKey(slot))
            inv.plyr.equips[slot] = null;
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
