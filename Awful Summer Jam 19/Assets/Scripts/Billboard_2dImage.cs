using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard_2dImage : MonoBehaviour
{
    public Camera cam;
    public bool flip = false;
    private void Update()
    {
            transform.LookAt(cam.transform);
        if (flip)
            transform.eulerAngles = transform.eulerAngles + new Vector3(180, 0, 180);
    }
}
