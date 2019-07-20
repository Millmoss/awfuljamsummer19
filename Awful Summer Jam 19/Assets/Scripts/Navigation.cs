using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Navigation : MonoBehaviour
{
	public float unit = 1f;
	public int size = 250;
	public float castHeight = 3;
	public GameObject goodMesh;
	public GameObject badMesh;
	public LayerMask castLayer;
	private Vector3[,] positions;
	private bool[,] walkable;

	void OnEnable()
	{
		positions = new Vector3[size, size];
		walkable = new bool[size, size];

		for (int x = -size / 2; x < size / 2; x++)
		{
			for (int z = -size / 2; z < size / 2; z++)
			{
				Vector3 pos = transform.position + new Vector3(x, castHeight, z);
				RaycastHit h;
				if (Physics.Raycast(pos, Vector3.down, out h, castHeight + 0.01f, castLayer.value))
				{
					positions[x + size / 2, z + size / 2] = h.point;
					walkable[x + size / 2, z + size / 2] = true;
				}
				else
				{
					positions[x + size / 2, z + size / 2] = Vector3.zero;
					walkable[x + size / 2, z + size / 2] = false;
				}
			}
		}
	}

	public List<Vector3> GetPath(Vector3 goal)		// TODO : consider a much simpler solution (travelling salesman
	{
		return null;
	}
}
