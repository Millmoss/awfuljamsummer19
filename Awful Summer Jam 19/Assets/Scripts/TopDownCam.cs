using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCam : MonoBehaviour
{
	//INSTRUCTIONS : Place camera in position desired. At start of play, this script will take the rotation and zoom values in and work from there

	//public values
	public Transform player = null;
	public Camera cam = null;
	public Transform camFocus = null;
	public float rps = 120f;            //rotation per second
	public float lerpSpeed = 0.5f;
	public float lerpFollow = 0.3f;

	//private values
	private float yr = -1;              //y rotation of the camera
	private float yrLag = -1;
	private float zoom = -1;			//distance from player

    void Start()
    {
		Vector3 look = Quaternion.LookRotation(player.position - cam.transform.position, Vector3.up).eulerAngles;
		yr = look.y;
		yrLag = look.y;
		cam.transform.eulerAngles = new Vector3(look.x, look.y, 0);

		zoom = (cam.transform.position - player.position).magnitude;
		camFocus.position = player.position;
    }
	
    void Update()
	{
		float rotate = 0;
		if (Input.GetButton("camera_rotate_left"))
			rotate += 1;
		if (Input.GetButton("camera_rotate_right"))
			rotate -= 1;

		yr += rps * Time.deltaTime * rotate;
		if (yr > 180f)
		{
			yr -= 360f;
			yrLag -= 360f;
		}
		if (yr < -180f)
		{
			yr += 360f;
			yrLag += 360f;
		}

		yrLag = Mathf.Lerp(yrLag, yr, lerpSpeed * Time.deltaTime * 60f);

		cam.transform.eulerAngles = new Vector3(cam.transform.eulerAngles.x, yrLag, 0);
		cam.transform.position = camFocus.position - cam.transform.forward * zoom;

		camFocus.position = Vector3.Lerp(camFocus.position, player.position, lerpFollow * Time.deltaTime * 60f);
	}
}
