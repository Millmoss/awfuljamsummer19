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

    public void Restart(int val)
    {
        cur_val = val;
        end_val = val;
        max_val = val;
        rate = val;
    }

    public void UpdateBar(int new_val)
    {
        end_val = new_val;
        if (!moving)
            moving = true;
    }

    private void Update()
    {
        if (moving)
        {
            if (cur_val == end_val)
                moving = false;
            else
            {
                if (cur_val < end_val)
                    cur_val += rate * Time.deltaTime;
                else
                    cur_val -= rate * Time.deltaTime;
                SetBarSize(bar, cur_val);
            }
        }
    }

    //Takes in the transform, and the wanted value.
    protected void SetBarSize(Transform b, float val)
    {
        b.localScale = new Vector3( (val / max_val), 
            b.localScale.y, 
            b.localScale.z);
    }
}
