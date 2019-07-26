using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public Camera cam;
	public float deadzone = 0.15f;
	public LayerMask clickMask;
	public float castDist = 20f;
	public float slowDist = 0.5f;
	public Animator anim;
	public GameObject playerPhysics;
	public PlayerUnit pu;

	private Vector3 moveDirection = Vector3.zero;
	private Collisions playerCollisions;

    void Start()
    {
		playerCollisions = playerPhysics.GetComponent<Collisions>();
		pu = GetComponent<PlayerUnit>();
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

		if (Input.GetKeyDown(KeyCode.Mouse1))
		{
			if (pu.selectedItem == itemtype.sword)
			{
				anim.SetTrigger("Attack_Cut");
			}
			else if (pu.selectedItem == itemtype.dagger)
			{
				anim.SetTrigger("Attack_Stab");
			}
		}
		if (Input.GetKey(KeyCode.Mouse1))
		{
			if (pu.selectedItem == itemtype.torch)
			{
				anim.SetFloat("TorchOut", 1);
			}
		}
		else if (pu.selectedItem == itemtype.torch)
		{
			anim.SetFloat("TorchOut", 0);
		}

		float spd = playerCollisions.GetSpeed();
		if (spd < .25f)
			spd = 0;
		anim.SetFloat("Speed", spd, 0.05f, Time.deltaTime);

		transform.position = Vector3.Lerp(transform.position, playerPhysics.transform.position, 0.5f * Time.deltaTime * 60);
		transform.rotation = Quaternion.Slerp(transform.rotation, playerPhysics.transform.rotation, 0.5f * Time.deltaTime * 60);
	}

	void FixedUpdate()
	{
		playerCollisions.SetDirection(moveDirection);
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
}
