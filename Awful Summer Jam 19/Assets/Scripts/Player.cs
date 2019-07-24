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
	public LayerMask collisionMask;
	public LayerMask clickMask;
	public float castDist = 20f;
	public float slowDist = 0.5f;
	public Rigidbody playerBody;
	public Animator anim;
	public GameObject mesh;
	public float characterRadius;

	private Vector3 moveDirection = Vector3.zero;
	private Vector3 moveVelocity = Vector3.zero;
	private bool decelerate = false;
	private Collider[] playerColliders;

    void Start()
    {
		playerColliders = GetComponents<Collider>();
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

		if (!decelerate)
			moveVelocity = Vector3.Lerp(moveVelocity, moveDirection * speed, accel * Time.deltaTime * 60f);
		else
			moveVelocity = Vector3.Lerp(moveVelocity, Vector3.zero, accel * Time.deltaTime * 60f / 5f);

		transform.position += moveVelocity * Time.deltaTime;

		ToggleColliders(false);

		Ray floorRay = new Ray(transform.position, Vector3.down);
		RaycastHit h;
		bool success = Physics.Raycast(floorRay, out h, 2f, collisionMask);

		if (success)
		{
			transform.position = h.point + Vector3.up;

			floorRay = new Ray(transform.position + moveDirection * speed * 0.5f, Vector3.down);
			success = Physics.Raycast(floorRay, out h, 2f, collisionMask);
			if (success)
			{
				decelerate = false;
			}
			else if ((h.point - transform.position).magnitude < 1f)
			{
				decelerate = true;
			}
		}

		/*
		Ray collisionRay = new Ray(transform.position, Vector3.down);
		success = Physics.SphereCast(transform.position - moveDirection * 1f, characterRadius, moveDirection, out h, speed, collisionMask);

		if (success && (h.point - transform.position).magnitude < 1.5f)
		{
			decelerate = true;
		}
		else
		{
			decelerate = false;
		}*/

		ToggleColliders(true);
	}

	void FixedUpdate()
	{
		ToggleColliders(false);

		Ray floorRay = new Ray(transform.position, Vector3.down);
		RaycastHit h;
		bool success = Physics.Raycast(floorRay, out h, 2f, collisionMask);

		if (success)
		{
			transform.position = h.point + Vector3.up;

			floorRay = new Ray(transform.position + moveDirection * speed * 0.5f, Vector3.down);
			success = Physics.Raycast(floorRay, out h, 2f, collisionMask);
			if (success)
			{
				decelerate = false;
			}
			else if ((h.point - transform.position).magnitude < 1f)
			{
				decelerate = true;
			}
		}

		/*
		Ray collisionRay = new Ray(transform.position, Vector3.down);
		success = Physics.SphereCast(transform.position - moveDirection * 1f, characterRadius, moveDirection, out h, speed, collisionMask);

		if (success && (h.point - transform.position).magnitude < 1.5f)
		{
			decelerate = true;
		}
		else
		{
			decelerate = false;
		}*/

		ToggleColliders(true);
	}

	void ClickMove()
	{
		Ray click = cam.ScreenPointToRay(Input.mousePosition);
		RaycastHit h;
		bool success = Physics.Raycast(click, out h, castDist, clickMask);

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

	void ToggleColliders(bool toggle)
	{
		for (int i = 0; i < playerColliders.Length; i++)
		{
			playerColliders[i].enabled = toggle;
		}
	}
}
