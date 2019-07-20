using UnityEngine;

//Similar to UIBar, but we have an "impact" bar as bar.
//bar_hp is what gets immediately changed first.
public class UIHP : UIBar
{
    public Transform bar_hp;
    public float time_before_decrease;
    private bool decreasing = false;

    //Apparently method hiding is a bad idea? IDK if protected works here, too lazy
    //to find out rn tbh.
    public new void UpdateBar(int new_val)
    {
        if (moving)
            moving = false;
        if (new_val < end_val)
        {
            decreasing = true;
            SetBarSize(bar_hp, new_val);
            Invoke("StartDecay", time_before_decrease);
        }
        else
        { 
            decreasing = false;
            StartDecay();
        }
        end_val = new_val;
        
    }
    
    private void StartDecay()
    {
        moving = true;
    }

    private void Update()
    {
        if (moving)
        {
            if (Mathf.Abs(cur_val - end_val) < 0.1 )
            { 
                moving = false;
                cur_val = end_val;
                SetBarSize(bar, cur_val);
            }
            else
            {
                if (!decreasing)
                {
                    cur_val += rate * Time.deltaTime;
                    SetBarSize(bar_hp, cur_val);
                    SetBarSize(bar, cur_val);
                }
                else
                {
                    cur_val -= rate * Time.deltaTime;
                    print(cur_val);
                    SetBarSize(bar, cur_val);
                }
            }
        }
    }
}
