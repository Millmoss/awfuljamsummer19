using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ONO : MonoBehaviour
{
	int h = 10;
	public Animator a;
	public float t = 0;
	public GameObject pp;
	public AudioSource aa;
	public AudioSource b;

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
			b.Play();
		}

		if (t != 0)
			t += Time.deltaTime;

		if (t > 60f)
		{
			SceneManager.LoadScene("THATSIT");
		}

		if ((pp.transform.position - transform.position).magnitude < 20)
		{
			if (t == 0)
				aa.Play();
			t += Time.deltaTime;
		}
    }

	void OnTriggerEnter(Collider c)
	{
		h--;
	}
}
