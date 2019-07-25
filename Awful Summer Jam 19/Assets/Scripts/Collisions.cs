using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collisions : MonoBehaviour
{
	public float speed = 3.4f;
	public float accel = 0.8f;
	public float slerpVal = 0.1f;
	public LayerMask collisionMask;

	private Vector3 moveDirection = Vector3.zero;
	private bool decelerate = false;
	private Collider[] playerColliders;
	private Rigidbody playerBody;
	private float currentSpeed = 0;

	void Start()
	{
		playerColliders = GetComponents<Collider>();
		playerBody = GetComponent<Rigidbody>();
	}

	void FixedUpdate()
	{
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
			else
			{
				decelerate = true;
			}
		}

		if (!decelerate)
			playerBody.velocity = Vector3.Lerp(playerBody.velocity, moveDirection * speed, accel * Time.deltaTime * 60f);
		else
			playerBody.velocity = Vector3.Lerp(playerBody.velocity, Vector3.zero, accel * Time.deltaTime * 60f / 5f);

		currentSpeed = 0;

		if (moveDirection != Vector3.zero)
			playerBody.rotation = Quaternion.Slerp(playerBody.rotation, Quaternion.LookRotation(moveDirection, Vector3.up), slerpVal * Time.deltaTime * 60f);
	}

	void ToggleColliders(bool toggle)
	{
		for (int i = 0; i < playerColliders.Length; i++)
		{
			playerColliders[i].enabled = toggle;
		}
	}

	public void SetDirection(Vector3 dir)
	{
		moveDirection = dir;
	}

	public float GetSpeed()
	{
		if (!decelerate)
			return playerBody.velocity.magnitude;

		return currentSpeed;
	}
}
