using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private Vector3 camPos;
    // Start is called before the first frame update
    void Start()
    {
        camPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0f ) // forward
        {
            camPos.y += Input.mouseScrollDelta.y;
            transform.position = camPos;
        }
    }
}
