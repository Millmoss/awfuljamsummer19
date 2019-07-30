using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ONO : MonoBehaviour
{
	int h = 10;
	public Animator a;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (h <= 0)
		{
			a.SetFloat("Blend", 1);
			gameObject.AddComponent<Rigidbody>().AddForce(new Vector3(Random.value * 10, Random.value * 10, Random.value * 10));
		}
    }

	void OnTriggerEnter(Collider c)
	{
		h--;
	}
}
