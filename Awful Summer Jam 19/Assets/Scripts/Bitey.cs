using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bitey : MonoBehaviour
{
	public bool state = false;
	public bool prevstate = false;
	private float t = 0;
	public bool gOOOO = false;
	public AudioSource hit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (state != prevstate && state)
		{
			prevstate = state;
		}

		if (state == prevstate && prevstate)
		{
			t += Time.deltaTime;
			if (t > 1f)
			{
				gOOOO = true;
			}
		}
    }

	public void SetState(string s)
	{
		if (s == "ATTACK")
			state = true;
		else
		{
			state = false;
			prevstate = false;
			gOOOO = false;
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			hit.Play();
		}
	}
}
