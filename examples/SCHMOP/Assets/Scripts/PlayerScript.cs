using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private float xPos;
    public float paddleSpeed = .05f;
    public float leftWall, rightWall;
    public GameObject bullet;

    public KeyCode leftKey, rightKey, spaceKey;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(leftKey))
        {
            if (xPos > leftWall)
            {
                xPos -= paddleSpeed;
            }
        }

        if (Input.GetKey(rightKey))
        {
            if (xPos < rightWall)
            {
                xPos += paddleSpeed;
            }
        }

        if (Input.GetKeyDown(spaceKey))
        {
            Instantiate(bullet, new Vector3(this.transform.position.x, this.transform.position.y, 0f), Quaternion.identity);
        }

        transform.localPosition = new Vector3(xPos, transform.position.y, 0);
    }
}

