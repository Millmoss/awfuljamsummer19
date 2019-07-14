using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public Camera cam;
	public float speed = 1;
	public float accel = 0.7f;
	public float rps = 360;				//degrees of player rotation allowed in a second
	public float deadzone = 0.15f;
	public LayerMask clickMask;
	public float castDist = 20f;
	public float slowDist = 0.5f;
	public Rigidbody playerBody;
	public Animator anim;
	public GameObject mesh;

	private Vector3 moveDirection = Vector3.zero;

    void Start()
    {

    }
	
    void Update()
    {
		if (Input.GetKey(KeyCode.Mouse0))
		{
			ClickMove();
		}
		else if (Mathf.Abs(Input.GetAxis("x_move")) > deadzone || Mathf.Abs(Input.GetAxis("z_move")) > deadzone)
		{
			StickMove();
		}
		else
		{
			moveDirection = Vector3.zero;
		}

		//if (moveDirection != Vector3.zero)
		//mesh.transform.rotation = Quaternion.RotateTowards(mesh.transform.rotation, Quaternion.LookRotation(moveDirection, Vector3.up), rps * Time.deltaTime);
		if (moveDirection != Vector3.zero)
			mesh.transform.rotation = Quaternion.RotateTowards(mesh.transform.rotation, Quaternion.LookRotation(moveDirection, Vector3.up), rps * Time.deltaTime);

		anim.SetFloat("Speed", playerBody.velocity.magnitude / speed);
	}

	void FixedUpdate()
	{
		playerBody.velocity = Vector3.Lerp(playerBody.velocity, moveDirection * speed, accel * Time.deltaTime * 60f);
	}

	void ClickMove()
	{
		Ray click = cam.ScreenPointToRay(Input.mousePosition);
		RaycastHit h;
		bool success = Physics.Raycast(click, out h, castDist, clickMask);

		print(h.point);

		if (!success)
		{
			Debug.LogError("Raycast Click Failure");
			return;
		}

		moveDirection = h.point - transform.position;
		moveDirection.y = 0;
		float dist = moveDirection.magnitude;
		moveDirection.Normalize();

		if (dist < slowDist)
		{
			moveDirection *= dist / slowDist;
		}
	}

	void StickMove()
	{
		float x = Input.GetAxis("x_move");
		if (x <= deadzone)
			x = 0;
		float z = Input.GetAxis("z_move");
		if (z <= deadzone)
			z = 0;

		moveDirection = new Vector3(x, 0, z);	//direction needs to be changed to world coordinates
	}
}
