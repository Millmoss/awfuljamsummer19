using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard_2dImage : MonoBehaviour
{
    public Camera cam;
    private void Update()
    {
        transform.LookAt(cam.transform);
    }
}
