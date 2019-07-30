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

	public GameObject sword;
	public GameObject dagger;
	public GameObject torch;
	public GameObject torchLight;

	private Vector3 moveDirection = Vector3.zero;
	private Collisions playerCollisions;

    void Start()
    {
		playerCollisions = playerPhysics.GetComponent<Collisions>();
		pu = GetComponent<PlayerUnit>();
    }
	
    void Update()
    {
		if (pu.ItemSwitched())
			EnableItem(pu.heldItem);

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
<<<<<<< HEAD
			if (pu.heldItem == ItemTypeEnums.values.sword)
			{
				anim.SetTrigger("Attack_Cut");
			}
			else if (pu.heldItem == ItemTypeEnums.values.dagger)
=======
			if (pu.selectedItem == PlayerUnit.itemtype.sword)
			{
				anim.SetTrigger("Attack_Cut");
			}
			else if (pu.selectedItem == PlayerUnit.itemtype.dagger)
>>>>>>> 67f42a404fceaaf1f084a458573de54c9d4b8226
			{
				anim.SetTrigger("Attack_Stab");
			}
		}
		if (Input.GetKey(KeyCode.Mouse1))
		{
<<<<<<< HEAD
			if (pu.heldItem == ItemTypeEnums.values.torch)
=======
			if (pu.selectedItem == PlayerUnit.itemtype.torch)
>>>>>>> 67f42a404fceaaf1f084a458573de54c9d4b8226
			{
				anim.SetFloat("TorchOut", 1, 0.2f, Time.deltaTime);
			}
		}
<<<<<<< HEAD
		else if (pu.heldItem == ItemTypeEnums.values.torch)
=======
		else if (pu.selectedItem == PlayerUnit.itemtype.torch)
>>>>>>> 67f42a404fceaaf1f084a458573de54c9d4b8226
		{
			anim.SetFloat("TorchOut", 0, 0.2f, Time.deltaTime);
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

		//if (dist < slowDist)
		//{
		//	moveDirection *= dist / slowDist;
		//}
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

	void EnableItem(ItemTypeEnums.values typ)
	{
		sword.SetActive(false);
		torch.SetActive(false);
		dagger.SetActive(false);
		torchLight.SetActive(false);
		if (typ == ItemTypeEnums.values.sword)
		{
			sword.SetActive(true);
		}
		else if (typ == ItemTypeEnums.values.dagger)
		{
			dagger.SetActive(true);
		}
		else if (typ == ItemTypeEnums.values.torch)
		{
			torch.SetActive(true);
			torchLight.SetActive(true);
		}
	}

	void OnTriggerEnter(Collider c)
	{
		if (c.tag == "Bite")
		{
			bool attacking = c.GetComponent<Bitey>().gOOOO;
			if (attacking)
			{
				print("ouch owie");
				return;     //cause damage
			}
		}
		if (c.tag == "Fright")
		{
			bool attacking = c.GetComponent<Bitey>().gOOOO;
			if (attacking)
			{
				print("ouch scary");
				return;     //cause bleed
			}
		}
	}
}
