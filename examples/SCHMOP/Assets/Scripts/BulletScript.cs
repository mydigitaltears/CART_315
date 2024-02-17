using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private float yPos;
    // Start is called before the first frame update
    void Start()
    {
        yPos = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        yPos += 1;
        transform.localPosition = new Vector3(transform.position.x, yPos, 0);
        if (yPos > 6)
        {
            Destroy(this.gameObject);
        }
    }
}