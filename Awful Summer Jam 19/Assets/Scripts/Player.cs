using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public Camera cam;
	public float speed = 1;
	public float accel = 0.7f;
	public float slerpVal = 0.9f;
	public float deadzone = 0.15f;
	public LayerMask clickMask;
	public float castDist = 20f;
	public float slowDist = 0.5f;
	public Rigidbody playerBody;
	public Animator anim;
	public GameObject mesh;

	private Vector3 moveDirection = Vector3.zero;
	private Vector3 moveVelocity = Vector3.zero;
	private bool decellerate = false;

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
			DirMove();
		}
		else
		{
			moveDirection = Vector3.zero;
		}

		if (moveDirection != Vector3.zero)
			mesh.transform.rotation = Quaternion.Slerp(mesh.transform.rotation, Quaternion.LookRotation(moveDirection, Vector3.up), slerpVal * Time.deltaTime * 60f);

		anim.SetFloat("Speed", moveVelocity.magnitude / speed);

		if (!decellerate)
		moveVelocity = Vector3.Lerp(moveVelocity, moveDirection * speed, accel * Time.deltaTime * 60f);

		transform.position += moveVelocity * Time.deltaTime;

		Ray floorRay = new Ray(transform.position, Vector3.down);
		RaycastHit h;
		bool success = Physics.Raycast(floorRay, out h, 2f, 10);

		if (success)
		{
			transform.position = h.point;

			floorRay = new Ray(transform.position + moveVelocity * 0.5f, Vector3.down);
			success = Physics.Raycast(floorRay, out h, 2f, 10);
			if (success)
			{
				decellerate = false;
			}
			else
			{
				decellerate = true;
			}
		}
	}

	void FixedUpdate()
	{
		//playerBody.velocity = Vector3.Lerp(playerBody.velocity, moveDirection * speed, accel * Time.deltaTime * 60f);
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

	void DirMove()
	{
		float x = Input.GetAxis("x_move");
		if (Mathf.Abs(x) <= deadzone)
			x = 0;
		float z = Input.GetAxis("z_move");
		if (Mathf.Abs(z) <= deadzone)
			z = 0;

		Vector3 forward = cam.transform.forward;
		forward.y = 0;
		forward.Normalize();
		Vector3 right = cam.transform.right;
		right.y = 0;
		right.Normalize();

		moveDirection = x * right + z * forward;
	}
}
