using UnityEngine;
using UnityEngine.UI;

public class UIItem : MonoBehaviour
{
    public Image icon, dur;
    public int max_val = 60;
    private float cur_val;
    private Color init_col;

    private void Start()
    {
        cur_val = max_val;
        init_col = icon.color;
    }

    private void Update()
    {
        cur_val -= Time.deltaTime;
        dur.fillAmount = cur_val / max_val;
        icon.color = Color.Lerp(init_col, 
            Color.red, 
            (1 - cur_val / max_val));
    }
}
