using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCam : MonoBehaviour
{
	//INSTRUCTIONS : Place camera in position desired. At start of play, this script will take the rotation and zoom values in and work from there

	//public values
	public Transform player = null;
	public Transform camParent = null;
	public Camera cam = null;
	public Transform followParent = null;
	public Transform follow = null;
	public float rps = 120f;            //rotation per second
	public float lerpSpeed = 0.5f;

	//private values
	private float yr = -1;				//y rotation of the camera
	private float zoom = -1;			//distance from player

    void Start()
    {
		Vector3 look = Quaternion.LookRotation(player.transform.position - cam.transform.position, Vector3.up).eulerAngles;
		yr = look.y;
		camParent.eulerAngles = new Vector3(0, yr, 0);
		followParent.transform.eulerAngles = new Vector3(0, yr, 0);
		cam.transform.eulerAngles = new Vector3(look.x, 0, 0);
		follow.transform.eulerAngles = new Vector3(look.x, 0, 0);

		zoom = (cam.transform.position - player.transform.position).magnitude;
		followParent.transform.position = camParent.transform.position;
    }
	
    void Update()
	{
		yr += rps * Time.deltaTime * Input.GetAxis("Mouse X");
		if (yr > 180f)
			yr -= 360f;
		if (yr < -180f)
			yr += 360f;

		camParent.transform.eulerAngles = new Vector3(0, yr, 0);
		camParent.transform.position = player.position - camParent.transform.forward * zoom;

		//camParent.rotation = Quaternion.Slerp(camParent.rotation, follow.rotation, lerpSpeed * Time.deltaTime * 60f);
		//camParent.position = Vector3.Lerp(camParent.position, follow.position, lerpSpeed * Time.deltaTime * 60f);
	}
}
