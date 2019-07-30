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
	public Inventory iiiiii;

	public GameObject sword;
	public GameObject dagger;
	public GameObject torch;
	public GameObject torchLight;

	public AudioSource a;
	public AudioSource aa;
	public AudioSource aaa;
	public AudioSource aaaa;

	private Vector3 moveDirection = Vector3.zero;
	private Collisions playerCollisions;

	public float timtam = 0;

	public List<GameObject> aaaaaa;

    void Start()
    {
		playerCollisions = playerPhysics.GetComponent<Collisions>();
		pu = GetComponent<PlayerUnit>();
    }
	
    void Update()
    {
		if (pu.hp <= 0)
			aaa.Play();

		
		if (timtam > .7f && moveDirection != Vector3.zero)
		{
			timtam += Time.deltaTime;
			a.Play();
			timtam = 0;
		}

		EnableItem(iiiiii.cur_eapon_value);

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
			if (pu.heldItem == ItemTypeEnums.values.sword)
			{
				anim.SetTrigger("Attack_Cut");
				aa.Play();
			}
			else if (pu.heldItem == ItemTypeEnums.values.dagger)
			{
				anim.SetTrigger("Attack_Stab");
				aa.Play();
			}
		}
		if (Input.GetKey(KeyCode.Mouse1))
		{
			if (pu.heldItem == ItemTypeEnums.values.torch)
			{
				anim.SetFloat("TorchOut", 1, 0.2f, Time.deltaTime);
				aa.Play();
			}
		}
		else if (pu.heldItem == ItemTypeEnums.values.torch)
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

	void EnableItem(string typ)
	{
		sword.SetActive(false);
		torch.SetActive(false);
		dagger.SetActive(false);
		torchLight.SetActive(false);
		if (typ == "swd")
		{
			sword.SetActive(true);
		}
		else if (typ == "DAG")
		{
			dagger.SetActive(true);
		}
		else if (typ == "fir e")
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
				pu.Hurt(10);
				aaaa.Play();
				return;     //cause damage
			}
		}
		if (c.tag == "Fright")
		{
			bool attacking = c.GetComponent<Bitey>().gOOOO;
			if (attacking)
			{
				print("ouch scary");
				pu.Hurt(5);
				aaaa.Play();
				return;     //cause bleed
			}
		}
	}
}
