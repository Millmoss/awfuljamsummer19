using UnityEngine;

public class UIBar : MonoBehaviour
{
    public Transform bar;
    public int max_val = 100;
    //Rate should be multiples of max_val;
    protected float rate = 100f;
    protected float end_val;
    protected float cur_val;
    protected bool moving = false;
    protected bool decreasing = false;

    //This should also be start...
    public void Restart(int val)
    {
        cur_val = val;
        end_val = val;
        max_val = val;
        rate = val;
    }

    public void UpdateBar(int new_val)
    {
        if (new_val < end_val)
            decreasing = true;
        else
            decreasing = false;
        end_val = new_val;
        if (!moving)
            moving = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
            UpdateBar((int)end_val - 35);
        if (Input.GetKeyDown(KeyCode.P))
            UpdateBar((int)end_val + 35);
        if (moving)
        {

            if (!decreasing)
            {
                cur_val += rate * Time.deltaTime;
                if (end_val - cur_val < 0)
                {
                    moving = false;
                    cur_val = end_val;
                    SetBarSize(bar, cur_val);
                }
                SetBarSize(bar, cur_val);
            }
            else
            {
                cur_val -= rate * Time.deltaTime;
                if (cur_val < end_val)
                {
                    moving = false;
                    cur_val = end_val;
                    SetBarSize(bar, cur_val);
                }
                SetBarSize(bar, cur_val);
            }

        }
    }

    //Takes in the transform, and the wanted value.
    protected void SetBarSize(Transform b, float val)
    {
        float tot = (val / max_val);
        if (tot < 0)
            tot = 0;
        b.localScale = new Vector3( tot, 
            b.localScale.y, 
            b.localScale.z);
    }
}
