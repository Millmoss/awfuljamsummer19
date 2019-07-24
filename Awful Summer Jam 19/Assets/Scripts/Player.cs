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
	public GameObject mesh;

    //For animations.
    public Animator anim;
    private enum attack_style { stab, cut};
    private attack_style attackStyle;

    private Vector3 moveDirection = Vector3.zero;

    void Start()
    {
        attackStyle = attack_style.stab;
    }
	
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            ClickAttack();
        }

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

		anim.SetFloat("Speed", playerBody.velocity.magnitude / speed);
	}

	void FixedUpdate()
	{
		playerBody.velocity = Vector3.Lerp(playerBody.velocity, moveDirection * speed, accel * Time.deltaTime * 60f);
	}

    void ClickAttack()
    {
        anim.SetTrigger("Attack_Cut");
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
